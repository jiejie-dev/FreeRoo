using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    /// <summary>
    /// 整型表达式
    /// </summary>
    public class IntegerExpression:Expression
    {
        public int Value { get; private set; }
        public IntegerExpression(int line,int col,int value)
            :base(line,col)
        {
            this.Value = value;
        }
    }
}
