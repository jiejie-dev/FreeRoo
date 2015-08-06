using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class TagFunction : Tag
    {
        public string FunctionName { get; private set; }
        public TagFunction(int line, int col, string name, string functionName)
            : base(line, col, name)
        {
            this.FunctionName = functionName;
        }
    }
}
