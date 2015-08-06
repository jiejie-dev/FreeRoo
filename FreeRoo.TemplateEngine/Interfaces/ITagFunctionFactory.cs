using FreeRoo.TemplateEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public interface ITagFunctionFactory
    {
        ITagFunctionPackage CreateTagFunctionPackage(string tagFunctionName);
    }
}
