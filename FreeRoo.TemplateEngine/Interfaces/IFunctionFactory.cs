using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public interface IFunctionFactory
    {
        IFunctionPackage CreateFunctionPackage(string functionName);
    }
}
