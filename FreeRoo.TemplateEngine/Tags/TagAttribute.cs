using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class TagAttribute
    {
        public string Name { get; private set; }
        public Expression Expression { get; private set; }
        public TagAttribute(string name, Expression exp)
        {
            this.Name = name;
            this.Expression = exp;
        }
    }
}
