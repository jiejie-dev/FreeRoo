using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class StringExpression:CellectionExpression
    {
        public string Value { get; private set; }
        public StringExpression(int line,int col,string value)
            :base(line,col)
        {
            this.Value = value;
        }
    }
}
