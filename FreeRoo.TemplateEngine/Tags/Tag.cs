using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class Tag:Element
    {
        public string Name { get; private set; }
        public TagAttribute[] Attributes { get; set; }
        public List<Element> InnerElements { get; private set; }
        public Tag(int line,int col,string name)
            :base(line,col)
        {
            this.Name = name;
            this.InnerElements = new List<Element>();
        }
        public Expression GetAttribute(string name)
        {
            foreach(TagAttribute t in Attributes)
            {
                if (t.Name == name)
                    return t.Expression;
            }
            throw new ParserException("Tag不存在属性值：" + name);
        }
    }
}
