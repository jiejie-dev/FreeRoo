using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    /// <summary>
    /// 页面各元素的基类
    /// </summary>
    public class Element
    {
        public int Line { get; private set; }
        public int Col { get; private set; }
        public Element(int line,int col)
        {
            this.Line = line;
            this.Col = col;
        }
        public override string ToString()
        {
            return "Element Line:" + Line + " Col:" + Col;
        }
    }
}
