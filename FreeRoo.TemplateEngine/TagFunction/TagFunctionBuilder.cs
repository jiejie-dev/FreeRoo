using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class TagFunctionBuilder
    {
        private Func<ITagFunctionFactory> factoryThunk;
        public static TagFunctionBuilder Current { get; private set; }
        static TagFunctionBuilder()
        {
            Current = new TagFunctionBuilder();
        }
        public void SetTagFunctionFactory(ITagFunctionFactory tagFunctionFactory)
        {
            factoryThunk = () => tagFunctionFactory;
        }
        public ITagFunctionFactory GetTagFunctionFactory()
        {
            return factoryThunk();
        }
    }
}
