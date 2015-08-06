using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class Variable : Expression
    {
        public string Name { get; private set; }
        public Variable(int line,int col,string name)
            :base(line,col)
        {
            this.Name = name;
        }
    }
}
