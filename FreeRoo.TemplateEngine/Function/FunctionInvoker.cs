
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class FunctionInvoker
    {
        public object Invoke(string functionName, object[] args)
        {
            Type tp = FunctionBuilder.Current.GetFunctionFactory().CreateFunctionPackage(functionName).GetType();
            MethodInfo method = tp.GetMethod(functionName);
            var o = Activator.CreateInstance(tp);
            var postedParams = new object[] { args };
            return method.Invoke(o, postedParams);

            throw new Exception("未找到此函数！");
        }
    }
}
