using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class ArgCreator
    {
        public object Create(string argName, Type argType, IDictionary<string,object> temp)
        {
            //是值类型或者是字符串类型
            if (argType.IsValueType || typeof(string) == argType)
            {
                object instance;
                if (GetValueTypeInstance(temp, argName, argType, out instance))
                {
                    return instance;
                }
                return Activator.CreateInstance(argType);
            }
            //传递模型参数
            
            return temp[argName];
        }
        bool GetValueTypeInstance(IDictionary<string,object> temp, string argName, Type argType, out object value)
        {
            foreach(var item in temp)
            {
                if(item.Key.ToString()==argName)
                {
                    value = Convert.ChangeType(item.Value, argType);
                    return true;
                }
            }
            value = null;
            return true;
        }
    }
}
