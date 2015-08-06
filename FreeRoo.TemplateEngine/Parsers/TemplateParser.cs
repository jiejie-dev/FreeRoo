using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class TemplateParser
    {
        TemplateLexer lexer;
        Token currentToken;
        List<Element> elements;
        public TemplateParser(TemplateLexer lexer)
        {
            this.lexer = lexer;
            this.elements = new List<Element>();
        }
        public Token Continue()
        {
            Token oldToken = currentToken;
            currentToken = lexer.Next();
            return oldToken;
        }
        public Token Continue(Token.TokenKind kind)
        {
            Token oldToken = currentToken;
            currentToken = lexer.Next();
            if (oldToken.Kind != kind)
                throw new ParserException("并不是 期望的Token！",currentToken);

            return oldToken;
        }
        public List<Element> Parse()
        {
            this.elements.Clear();
            Continue();
            
            while (true)
            {
                Element ele = ReadElement();
                if (ele != null)
                    this.elements.Add(ele);
                else
                {
                    break;
                }
            }
            return this.elements;
        }
        Element ReadElement()
        {
            switch(currentToken.Kind)
            {
                case Token.TokenKind.EOF:
                    return null;
                case Token.TokenKind.TextData:
                    Text text= new Text(currentToken.Line, currentToken.Col, currentToken.Text);
                    Continue();
                    return text;
                case Token.TokenKind.ExpStart:
                    return ReadExpression();
                case Token.TokenKind.TagStart:
                    return ReadTag();
                case Token.TokenKind.TagClose:
                    return ReadTagClose();
                default:
                    throw new ParserException("无效的Token",currentToken);
            }
            
        }
        TagClose ReadTagClose()
        {
            Continue(Token.TokenKind.TagClose);
            Token id = Continue(Token.TokenKind.ID);
            Token tagName = null;
            if (currentToken.Kind == Token.TokenKind.Colon)
            {
                Continue(Token.TokenKind.Colon);
                tagName = Continue(Token.TokenKind.ID);
            }

            Continue(Token.TokenKind.TagEnd);
            TagClose tagClose;
            if (tagName != null)
                tagClose = new TagClose(id.Line, id.Col, id.Text + ":" + tagName.Text);
            else
                tagClose = new TagClose(id.Line, id.Col, id.Text);

            return tagClose;
        }
        /// <summary>
        /// 读取表达式
        /// </summary>
        /// <returns></returns>
        Expression ReadExpression()
        {
            Continue(Token.TokenKind.ExpStart);
            Token name = Continue(Token.TokenKind.ID);

            if (currentToken.Kind == Token.TokenKind.LParen)
            {
                Expression[] args = ReadArguments();
                Continue();
                return new Function(name.Line, name.Col, name.Text, args);
            }
            else if (currentToken.Kind == Token.TokenKind.ExpEnd)
            {
                Continue();
                return new Variable(name.Line, name.Col, name.Text);  //有问题
            }
            else if (currentToken.Kind == Token.TokenKind.Dot)
            {
                Continue();
                Token field = Continue(Token.TokenKind.ID);
                Continue();
                return new Field(name.Line, name.Col, name.Text, field.Text);
            }
            else
                throw new ParserException("无效的表达式！", currentToken);
        }
        /// <summary>
        /// 读取Tag
        /// </summary>
        /// <returns></returns>
        Tag ReadTag()
        {
            Continue(Token.TokenKind.TagStart);
            Token tagID = Continue(Token.TokenKind.ID);
            Tag tag;
            if (currentToken.Kind == Token.TokenKind.Colon)
            {
                Continue(Token.TokenKind.Colon);
                Token token = Continue(Token.TokenKind.ID);
                tag = new TagFunction(tagID.Line, tagID.Col, tagID.Text, token.Text);
            }
            else
                tag = new Tag(tagID.Line, tagID.Col, tagID.Text);
            tag.Attributes = ReadAttributes();

            ////test
            //if (currentToken.Kind == Token.TokenKind.TextData)
            //{
            //    Token template = Continue(Token.TokenKind.TextData);
            //    tag.InnerElements.Add(new StringExpression(currentToken.Line, currentToken.Col, template.Text));
            //}

            return tag;
        }
        /// <summary>
        /// 读取Tag和TagFunction的属性
        /// </summary>
        /// <returns></returns>
        TagAttribute[] ReadAttributes()
        {
            List<TagAttribute> tagAttrs = new List<TagAttribute>();
            while (true)
            {
                if (currentToken.Kind == Token.TokenKind.ID)
                    tagAttrs.Add(ReadAttribute());
                else if (currentToken.Kind == Token.TokenKind.TagEnd)
                {
                    Continue();
                    break;
                }
                else
                    throw new ParserException("无效的Token", currentToken);
            }
            return tagAttrs.ToArray();
        }
        /// <summary>
        /// 读取Tag和TagFunction的属性
        /// </summary>
        /// <returns></returns>
        TagAttribute ReadAttribute()
        {
            Token name = Continue(Token.TokenKind.ID);
            Continue(Token.TokenKind.TagEquals);

            Expression exp = null;
            if (currentToken.Kind == Token.TokenKind.StringStart)
                exp = ReadString();
            else
                throw new ParserException("不是期望的Token",currentToken);
            return new TagAttribute(name.Text, exp);
        }
        /// <summary>
        /// 读取函数参数
        /// </summary>
        /// <returns></returns>
        Expression[] ReadArguments()
        {
            List<Expression> exps = new List<Expression>();
            Continue(Token.TokenKind.LParen);
            int index=0;
            while(true)
            {
                if (currentToken.Kind == Token.TokenKind.RParen)
                {
                    Continue();
                    break;
                }
                if (index > 0)
                    Continue(Token.TokenKind.Comma);
                if(currentToken.Kind==Token.TokenKind.Integer)
                {
                    Token arg = Continue(Token.TokenKind.Integer);
                    IntegerExpression integer = new IntegerExpression(arg.Line, arg.Col, int.Parse(arg.Text));
                    exps.Add(integer);
                }
                if(currentToken.Kind==Token.TokenKind.Double)
                {
                    Token arg = Continue(Token.TokenKind.Double);
                    DoubleExpression d = new DoubleExpression(arg.Line, arg.Col, double.Parse(arg.Text));
                    exps.Add(d);
                }
                if (currentToken.Kind == Token.TokenKind.ExpStart)
                    exps.Add(ReadExpression());
                if (currentToken.Kind == Token.TokenKind.StringStart)
                    exps.Add(ReadString());
                
                index++;
            }
            return exps.ToArray();
        }
        /// <summary>
        /// 读取双引号中间的元素
        /// </summary>
        /// <returns></returns>
        Expression ReadString()
        {
            Continue(Token.TokenKind.StringStart);
            CellectionExpression Exp = new CellectionExpression(currentToken.Line, currentToken.Col);
            while (true)
            {
                if (currentToken.Kind == Token.TokenKind.ExpStart)
                {
                    Exp.Add(ReadExpression());
                }
                else if (currentToken.Kind == Token.TokenKind.StringText)
                {
                    Token value = Continue(Token.TokenKind.StringText);
                    StringExpression se = new StringExpression(currentToken.Line, currentToken.Col, value.Text);
                    Exp.Add(se);
                }
                else if (currentToken.Kind == Token.TokenKind.Integer)
                {
                    Token token = Continue(Token.TokenKind.Integer);
                    IntegerExpression integer = new IntegerExpression(token.Line, token.Col, int.Parse(token.Text));
                    Exp.Add(integer);
                }
                else if (currentToken.Kind == Token.TokenKind.Double)
                {
                    Token d = Continue(Token.TokenKind.Double);
                    DoubleExpression dexp = new DoubleExpression(d.Line, d.Col, double.Parse(d.Text));
                    Exp.Add(dexp);
                }
                else if (currentToken.Kind == Token.TokenKind.StringEnd)
                {
                    Continue();
                    break;
                }
                else if (currentToken.Kind == Token.TokenKind.EOF)
                {
                    throw new ParserException("不是期望的Token", currentToken);
                }
                else
                    throw new ParserException("不是期望的Token", currentToken);
            }
            if (Exp.Expressions.Count == 1)
                return Exp.Expressions[0];
            else
                return Exp;
        }
    }
}
