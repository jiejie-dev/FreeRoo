
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class Template : ITemplate
    {
        Dictionary<string, Template> InnerTemplates;
        ValuePool Variables;
        public string Name { get; set; }
        public string Content { get; set; }
        public string Dir { get; set; }
        public Template()
        {
            this.Name = "Name";
            this.Content = "Content";
            this.Variables = new ValuePool();
            this.InnerTemplates = new Dictionary<string, Template>();
        }
        public Template(string name, string content)
        {
            this.Name = name;
            this.Content = content;
            this.Variables = new ValuePool();
            this.InnerTemplates = new Dictionary<string, Template>();
        }
        public static Template FromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Template t = new Template(fileInfo.Name, File.ReadAllText(filePath, Encoding.Default));
                t.Dir = fileInfo.DirectoryName;
                t.SetValue("Dir", t.Dir);
                return t;
            }
            else
                throw new Exception("文件未找到！：" + filePath);
        }
        public static Template FromElements(List<Element> elements)
        {
            Template t = new Template();
            t.Elements = elements;
            return t;
        }
        /// <summary>
        /// 执行内置函数
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        object EvalFunction(Function f)
        {
            object[] args = EvalArguments(f.Arguments);

            FunctionInvoker invoker = new FunctionInvoker();

            return invoker.Invoke(f.Name, args);
        }
        object[] EvalArguments(Expression[] exps)
        {
            object[] args = new object[exps.Length];
            for (int i = 0; i < exps.Length; i++)
                args[i] = EvalExpression(exps[i]);
            return args;

        }
        object EvalExpression(Expression exp)
        {
            if (exp is Function)
                return EvalFunction(exp as Function);
            if (exp is IntegerExpression)
                return (exp as IntegerExpression).Value;
            else if (exp is DoubleExpression)
                return (exp as DoubleExpression).Value;
            else if (exp is StringExpression)
                return (exp as StringExpression).Value;
            else if (exp is Field)
                return EvalField(exp as Field);
            else if (exp is Variable)
                return GetVariableValue((exp as Variable).Name);
            else
                return new Exception("未知的表达式类型");
        }
        object GetVariableValue(string name)
        {
            if (this.Variables.Contains(name))
                return this.Variables[name];
            else if (TEConfigBuilder.Current.Global.Contains(name))
                return TEConfigBuilder.Current.Global[name];
            else
                throw new Exception("未找到指定变量：" + name);
        }
        object EvalField(Field field)
        {
            object o = GetVariableValue(field.ObjectName);
            //object o = Variables[field.ObjectName];
            PropertyInfo p = o.GetType().GetProperty(field.FieldName);

            if (p != null)
                return p.GetValue(o, null);

            FieldInfo f = o.GetType().GetField(field.FieldName);
            if (f != null)
                return f.GetValue(o);
            else
                throw new ParserException("字段属性不存在！ObjectName:" + field.ObjectName + " ObjectField:" + field.FieldName);
        }

        public void Process(TextWriter writer)
        {
            writer.Write(Process());
        }
        StringWriter sw = new StringWriter();
        TemplateLexer lexer;
        TemplateParser parser;
        TagParser tagParser;
        public List<Element> Elements { get; set; }
        public string Process()
        {
            if (Elements == null)
            {
                lexer = new TemplateLexer(this.Content);
                parser = new TemplateParser(lexer);
                tagParser = new TagParser(parser);
                //TagParser tagParser=new TagParser(parser);
                //List<Element> elements = tagParser.Parse();

                Elements = tagParser.Parse();
            }
            
            for (int i = 0; i < Elements.Count; i++)
            {
                if (Elements[i] is Function)
                {
                    sw.Write(ProcessFunction(Elements[i] as Function));
                }
                else if (Elements[i] is TagFunction)
                {
                    var item = Elements[i] as TagFunction;
                    if (item.FunctionName == "Include")
                    {
                        string templateName = string.Empty;
                        Expression attrExp = item.GetAttribute("file");
                        templateName = EvalExpression(attrExp).ToString();
                        var temp = new Template(templateName, ProcessTagFunction(item));
                        for (int j = 0; j < Variables.Count; j++)
                        {
                            string key = Variables.GetKey(j);
                            string value = Variables.GetValue(j).ToString();
                            temp.SetValue(Variables.GetKey(j), Variables.GetValue(j));
                        }
                        sw.Write(temp.Process());
                    }
                    else
                        sw.Write(ProcessTagFunction(Elements[i] as TagFunction));
                }
                else if (Elements[i] is Text)
                {
                    sw.Write((Elements[i] as Text).Data);
                }
                else if (Elements[i] is Field)
                {
                    sw.Write(EvalField(Elements[i] as Field));
                }
                else if(Elements[i] is Variable)
                {
                    sw.Write(GetVariableValue((Elements[i] as Variable).Name));
                }
                else if (Elements[i] is TagClose)
                {

                }
            }
            return sw.GetStringBuilder().ToString();
        }

        string ProcessFunction(Function f)
        {
            return EvalFunction(f).ToString();
        }
        /// <summary>
        /// 执行内置标签函数
        /// </summary>
        /// <param name="tagF"></param>
        /// <returns></returns>
        object EvalTagFunction(TagFunction tagF)
        {
            Dictionary<string, object> temp = new Dictionary<string, object>();

            foreach (TagAttribute tagA in tagF.Attributes)
            {
                temp.Add(tagA.Name, EvalExpression(tagA.Expression));
            }
            if (tagF.InnerElements.Count > 0)
            {
                Template t = Template.FromElements(tagF.InnerElements);
                temp.Add("template", t);
            }
            TagFunctionInvoker invoker = new TagFunctionInvoker();
            return invoker.Invoke(tagF.FunctionName, temp);
        }
        string ProcessTagFunction(TagFunction tagF)
        {
            return EvalTagFunction(tagF).ToString();
        }

        public void SetValue(string name, object value)
        {
            this.Variables[name] = value;
        }

    }
}
