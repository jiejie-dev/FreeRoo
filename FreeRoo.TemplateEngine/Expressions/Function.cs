using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    /// <summary>
    /// 代表内置函数或者插件函数
    /// </summary>
    public class Function : Expression
    {
        public string Name { get; private set; }
        public Expression[] Arguments { get; set; }
        public Function(int line,int col,string name,Expression[] args)
            :base(line,col)
        {
            this.Name = name;
            this.Arguments = args;
        }
    }
}
