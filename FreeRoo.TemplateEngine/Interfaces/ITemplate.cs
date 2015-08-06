using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FreeRoo.TemplateEngine
{
    public interface ITemplate
    {
        /// <summary>
        /// 处理模版并打印页面内容
        /// </summary>
        void Process(TextWriter writer);
        /// <summary>
        /// 处理模版并返回相应页面内容
        /// </summary>
        /// <returns></returns>
        string Process();
        /// <summary>
        /// 设置变量和它对应的值
        /// </summary>
        void SetValue(string name,object value);
    }
}
