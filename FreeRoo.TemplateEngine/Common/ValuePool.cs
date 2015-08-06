using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class ValuePool
    {
        Dictionary<string, object> variables;
        public ValuePool()
        {
            this.variables = new Dictionary<string, object>();
        }
        public object this[string name]
        {
            get
            {
                return variables[name];
            }
            set
            {
                variables[name] = value;
            }
        }
        public bool Contains(string name)
        {
            return variables.ContainsKey(name);
        }
        public string GetKey(int index)
        {
            var arr = variables.ToArray();

            return arr[index].Key;

        }
        public object GetValue(int index)
        {
            var arr = variables.ToArray();

            return arr[index].Value;
        }
        public int Count
        {
            get { return variables.Count; }
        }
    }
}
