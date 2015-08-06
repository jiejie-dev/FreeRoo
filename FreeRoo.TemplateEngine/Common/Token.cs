using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeRoo.TemplateEngine
{
    public class Token
    {
        public enum TokenKind
        {
            EOF,
            Comment,
            // common tokens
            ID,				// (alpha)+

            // text specific tokens
            TextData,

            // tag tokens
            TagName,
            TagStart,		// <zb: 
            TagEnd,			// > 
            TagEndClose,	// />
            TagClose,		// </zb:
            TagEquals,		// =


            // expression
            ExpStart,		// $ at the beginning
            ExpEnd,			// $ at the end
            LParen,			// (
            RParen,			// )
            Dot,			// .
            Comma,			// ,
            Integer,		// integer number
            Double,			// double number
            LBracket,		// [
            RBracket,		// ]
            Colon,          //冒号

            // operators
            OpOr,           // "or" keyword
            OpAnd,          // "and" keyword
            OpIs,			// "is" keyword
            OpIsNot,		// "isnot" keyword
            OpLt,			// "lt" keyword
            OpGt,			// "gt" keyword
            OpLte,			// "lte" keyword
            OpGte,			// "gte" keyword

            // string tokens
            StringStart,	// "
            StringEnd,		// "
            StringText,		// text within the string

            //function
            FunctionName,
            FunctionStart,
            FunctionEnd
        }
        public int Line { get; private set; }
        public int Col { get; private set; }
        public string Text { get; private set; }
        public TokenKind Kind { get; private set; }
        public Token(int line, int col, string data,TokenKind tokenKind)
        {
            this.Line = line;
            this.Col = col;
            this.Text = data;
            this.Kind = tokenKind;
        }
    }
}
