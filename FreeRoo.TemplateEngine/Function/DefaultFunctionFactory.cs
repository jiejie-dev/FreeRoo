using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class DefaultFunctionFactory : IFunctionFactory
    {
        protected List<Type> FunctionPackage { get; set; }

        public DefaultFunctionFactory ()
		{
			this.FunctionPackage = new List<Type> ();
			Assembly assembly = Assembly.GetExecutingAssembly ();
			foreach (Type type in assembly.GetTypes().Where(type => typeof(IFunctionPackage).IsAssignableFrom(type))) {
				FunctionPackage.Add (type);
			}
		}
	

        public virtual IFunctionPackage CreateFunctionPackage(string functionName)
        {
            foreach(Type type in FunctionPackage)
            {
                foreach(var method in type.GetMethods())
                {
                    if (method.Name == functionName)
                        return (IFunctionPackage)Activator.CreateInstance(type);
                }
            }
            throw new Exception("没有找到FunctionName为：" + functionName + "的包");
        }
    }
}
