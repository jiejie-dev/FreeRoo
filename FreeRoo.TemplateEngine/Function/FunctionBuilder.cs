using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class FunctionBuilder
    {
        private Func<IFunctionFactory> factoryThunk;
        public static FunctionBuilder Current { get; private set; }
        static FunctionBuilder()
        {
            Current = new FunctionBuilder();
        }
        public void SetFunctionFactory(IFunctionFactory functionFactory)
        {
            factoryThunk = () => functionFactory;
        }
        public IFunctionFactory GetFunctionFactory()
        {
            return factoryThunk();
        }
    }
}
