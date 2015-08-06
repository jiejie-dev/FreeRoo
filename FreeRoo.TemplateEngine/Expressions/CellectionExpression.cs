using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    /// <summary>
    /// 通用表达式集合，用于包含多种表达式种类表示
    /// </summary>
    public class CellectionExpression : Expression
    {
        public List<Expression> Expressions { get; private set; }
        public CellectionExpression(int line, int col)
            : base(line, col)
        {
            this.Expressions = new List<Expression>();
        }
        public void Add(Expression exp)
        {
            this.Expressions.Add(exp);
        }
        public Expression this[int index]
        {
            get { return this.Expressions[index]; }
        }
    }
}
