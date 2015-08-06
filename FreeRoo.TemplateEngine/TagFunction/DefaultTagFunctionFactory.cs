using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class DefaultTagFunctionFactory : ITagFunctionFactory
    {
        protected List<Type> TagFunctionPackage { get; set; }

        public DefaultTagFunctionFactory()
        {
            this.TagFunctionPackage = new List<Type>();
			Assembly assembly = Assembly.GetExecutingAssembly ();
                foreach (Type type in assembly.GetTypes().Where(type => typeof(ITagFunctionPackage).IsAssignableFrom(type)))
                {
                    TagFunctionPackage.Add(type);
                }
        }


        public virtual ITagFunctionPackage CreateTagFunctionPackage(string tagFunctionName)
        {
            foreach (Type type in TagFunctionPackage)
            {
                foreach (var method in type.GetMethods())
                {
                    if (method.Name.ToLower() == tagFunctionName.ToLower())
                        return (ITagFunctionPackage)Activator.CreateInstance(type);
                }
            }
            throw new Exception("没有找到FunctionName为：" + tagFunctionName + "的包");
        }
    }
}
