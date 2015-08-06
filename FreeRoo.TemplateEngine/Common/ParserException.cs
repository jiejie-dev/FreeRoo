using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class ParserException:Exception
    {
        public ParserException(string msg, Token token)
            : base(msg + "\tLine:" + token.Line + "\tCol:" + token.Col)
        {

        }
        public ParserException(string msg)
            :base(msg)
        {

        }
    }
}
