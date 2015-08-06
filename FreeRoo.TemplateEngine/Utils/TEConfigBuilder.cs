
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class TEConfigBuilder
    {
        public static TEConfig Current { get; private set; }
        public static void SetConfig(TEConfig config)
        {
            Current = config;
        }
        public static TEConfig GetConfig()
        {
            return Current;
        }
    }
}
