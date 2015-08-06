using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class Field : Expression
    {
        public string ObjectName { get; private set; }
        public string FieldName { get; private set; }
        public Field(int line, int col, string objectName, string fieldName)
            :base(line,col)
        {
            this.ObjectName = objectName;
            this.FieldName = fieldName;
        }
    }
}
