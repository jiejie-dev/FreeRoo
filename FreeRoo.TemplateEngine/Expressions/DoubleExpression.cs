using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class DoubleExpression:Expression
    {
        public double Value { get; private set; }
        public DoubleExpression(int line,int col,double value)
            :base(line,col)
        {

        }
    }
}
