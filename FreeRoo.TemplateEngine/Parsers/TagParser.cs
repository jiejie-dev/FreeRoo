using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class TagParser
    {
        TemplateParser parser;
        public TagParser(TemplateParser parser)
        {
            this.parser = parser;
        }
        int pos;
        public List<Element> Parse()
        {
            List<Element> es = new List<Element>();
            es = this.parser.Parse();
            pos = 0;
            List<Element> result = new List<Element>();
            for (int i = 0; i < es.Count; i++)
            {
                var item = es[i];
                if (item is TagFunction)
                {
                    var tagF = item as TagFunction;
                    i++;
                    while (true)
                    {
                        if (es[i] is TagClose)
                        {
                            var temp = es[i] as TagClose;
                            if (tagF.Name + ":" + tagF.FunctionName == temp.Name)
                            {
                                break;
                            }
                        }
                        tagF.InnerElements.Add(es[i]);
                        i++;
                        if (i >= es.Count)
                            break;

                    }
                    result.Add(tagF);
                }
                else
                    result.Add(item);
            }

            return result;
        }
        void CollectTag()
        {

        }
    }
}
