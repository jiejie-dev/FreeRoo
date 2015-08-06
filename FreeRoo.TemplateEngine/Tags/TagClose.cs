using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class TagClose:Element
    {
        public string Name{get;private set;}
        public TagClose(int line,int col,string name)
            :base(line,col)
        {
            this.Name = name;
        }
    }
}
