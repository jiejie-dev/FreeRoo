using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    /// <summary>
    /// 各种表达式的基类，表达式包括函数，变量，常量等
    /// </summary>
    public class Expression : Element
    {
        public Expression(int line, int col)
            : base(line, col)
        {

        }
        public override string ToString()
        {
            return "Expression Line:" + Line + " Col" + Col;
        }
    }
}
