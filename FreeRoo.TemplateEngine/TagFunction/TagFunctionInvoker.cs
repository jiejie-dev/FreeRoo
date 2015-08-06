using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FreeRoo.TemplateEngine
{
    public class TagFunctionInvoker
    {
        public object Invoke(string tagFunctionName, Dictionary<string, object> tempArgs)
        {
            Type tp = TagFunctionBuilder.Current.GetTagFunctionFactory().CreateTagFunctionPackage(tagFunctionName).GetType();
            
            MethodInfo method = null;
            
            foreach(var item in tp.GetMethods())
            {
                if (item.Name.ToLower() == tagFunctionName.ToLower())
                    method = item;

            }
            
            var o = Activator.CreateInstance(tp);
            ArgCreator argCreator = new ArgCreator();
            List<object> args = new List<object>();
            foreach (var para in method.GetParameters())
            {
                args.Add(argCreator.Create(para.Name, para.ParameterType, tempArgs));
            }
            return method.Invoke(o, args.ToArray());

            throw new Exception("未找到此标签函数！");
        }
    }
}
