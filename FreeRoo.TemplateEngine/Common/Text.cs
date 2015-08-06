using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class Text : Element
    {
        public string Data { get; private set; }
        public Text(int line, int col, string data)
            : base(line, col)
        {
            this.Data = data;
        }
    }
}
