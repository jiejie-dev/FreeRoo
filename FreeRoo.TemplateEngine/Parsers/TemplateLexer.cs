using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FreeRoo.TemplateEngine
{
    public class TemplateLexer
    {
        int savePos;
        int saveLine;
        int saveCol;

        int currentPos;
        int currentLine;
        int currentCol;

        string data;
        const char EOF =(char)0;

        enum LexerMode
        {
            Text,
            String,
            Expression,
            Tag
        }
        LexerMode currentMode;
        Stack<LexerMode> Mode;
        
        public TemplateLexer(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            data = reader.ReadToEnd();

            Reset();
        }

        public TemplateLexer(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            this.data = data;

            Reset();
        }
        void Reset()
        {
            this.Mode = new Stack<LexerMode>();

            currentPos = 0;
            currentLine = 1;
            currentCol = 1;

            currentMode = LexerMode.Text;
            Mode.Push(currentMode);
        }
        char NextChar(int count)
        {
            if (currentPos + count >= data.Length)
                return EOF;
            else
                return data[currentPos + count];
        }
        void StartRead()
        {
            savePos = currentPos;
            saveLine = currentLine;
            saveCol = currentCol;
        }
        char Continue()
        {
            char ch = data[currentPos];
            currentPos++;
            currentCol++;
            return ch;
        }
        char Continue(int count)
        {
            if (count <= 0)
                throw new ParserException("continue count<=0!");
            char ch = ' ';
            while (count > 0)
            {
                ch = Continue();
                count--;
            }
            return ch;
        }
        public Token Next()
        {
            switch(currentMode)
            {
                case LexerMode.Text: return NextText();

                case LexerMode.Tag: return NextTag();

                case LexerMode.Expression: return NextExpression();

                case LexerMode.String: return NextString();

                default: throw new ParserException("未知的LexerMode!");

            }
        }
        Token NextText()
        {
            StartRead();
        StartTextRead:
            char ch = NextChar(0);
            switch (ch)
            {
                case EOF:
                    if (savePos == currentPos)
                        return CreateToken(Token.TokenKind.EOF);
                    else 
                        break;
                case '\n':
                case '\r':
                    ReadWhitespace();	// handle newlines specially so that line number count is kept
                    goto StartTextRead;
                case '<':
                    if (NextChar(1) == '#')
                    {
                        if (savePos == currentPos)
                        {
                            Continue(2);
                            
                            EnterMode(LexerMode.Tag);
                            return CreateToken(Token.TokenKind.TagStart);
                        }
                        else
                            break;
                    }
                    else if(NextChar(1)=='/'&&NextChar(2)=='#')
                    {
                        if (savePos == currentPos)
                        {
                            Continue(3);
                            EnterMode(LexerMode.Tag);
                            return CreateToken(Token.TokenKind.TagClose);
                        }
                        else
                            break;
                    }
                    Continue();
                    goto StartTextRead;
                case '#':
                    if (NextChar(1) == '{')
                    {
                        if (savePos == currentPos)
                        {
                            Continue(2);
                            EnterMode(LexerMode.Expression);
                            return CreateToken(Token.TokenKind.ExpStart);
                        }
                        else
                            break;
                    }
                    Continue();
                    goto StartTextRead;
                default:
                    Continue();
                    goto StartTextRead;

            }
            return CreateToken(Token.TokenKind.TextData);
        }
        Token NextString()
        {
            StartRead();
        StartStringRead:
            char ch = NextChar(0);
            switch(ch)
            {
                case EOF:
                    return CreateToken(Token.TokenKind.EOF);
                case '\r':
                case '\n':
                    ReadWhitespace();
                    goto StartStringRead;
                case '"':
                    if (NextChar(1) == '"')
                    {
                        Continue();
                        goto StartStringRead;
                    }
                    else if (savePos == currentPos)
                    {
                        Continue();
                        LeaveMode();
                        return CreateToken(Token.TokenKind.StringEnd);
                    }
                    else
                        break;
                case '#':
                    if(NextChar(1)=='{')
                    {
                        if (savePos == currentPos)
                        {
                            Continue(2);
                            EnterMode(LexerMode.Expression);
                            return CreateToken(Token.TokenKind.ExpStart);
                        }
                        else
                            break;
                    }
                    Continue();
                    goto StartStringRead;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ReadNumber();

                default:
                    Continue();
                    goto StartStringRead;
            }
            return CreateToken(Token.TokenKind.StringText);
        }
        Token NextTag()
        {
            StartRead();
        StartTagRead:
            char ch = NextChar(0);
            switch (ch)
            {
                case EOF:
                    return CreateToken(Token.TokenKind.EOF);
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                    ReadWhitespace();	// ignore whitespace
                    StartRead();		// remark current position
                    goto StartTagRead;	// start again
                case '=':
                    Continue();
                    return CreateToken(Token.TokenKind.TagEquals);
                case '<':
                    if (NextChar(1) == '/' && NextChar(2) == '#')
                    {
                        Continue(2);
                        LeaveMode();
                        return CreateToken(Token.TokenKind.TagClose);
                    }
                    Continue();
                    goto StartTagRead;

                case '"':
                    Continue();
                    EnterMode(LexerMode.String);
                    return CreateToken(Token.TokenKind.StringStart);
                case '>':
                    Continue();
                    LeaveMode();
                    return CreateToken(Token.TokenKind.TagEnd);
                case ':':
                    Continue();
                    return CreateToken(Token.TokenKind.Colon);
                default:
                    if (Char.IsLetter(ch) || ch == '_')
                        return ReadID();
                    break;
            }
            throw new Exception("词法出错!Line:" + currentLine + "\tCol:" + currentCol);
        }
        Token NextExpression()
        {
            StartRead();
        StartFunctionRead:
            char ch = NextChar(0);
            switch (ch)
            {
                case EOF:
                    return CreateToken(Token.TokenKind.EOF);
                case '}':
                    Continue();
                    LeaveMode();
                    return CreateToken(Token.TokenKind.ExpEnd);
                case '"':
                    Continue();
                    EnterMode(LexerMode.String);
                    return CreateToken(Token.TokenKind.StringStart);
                case ',':
                    Continue();
                    return CreateToken(Token.TokenKind.Comma);
                case '.':
                    Continue();
                    return CreateToken(Token.TokenKind.Dot);
                case ':':
                    Continue();
                    return CreateToken(Token.TokenKind.Colon);
                case '(':
                    Continue();
                    return CreateToken(Token.TokenKind.LParen);
                case ')':
                    Continue();
                    return CreateToken(Token.TokenKind.RParen);

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ReadNumber();

                case '#':
                    if(NextChar(1)=='{')
                    {
                        if (savePos == currentPos)
                        {
                            Continue(2);
                            EnterMode(LexerMode.Expression);
                            return CreateToken(Token.TokenKind.ExpStart);
                        }
                        else
                            break;
                    }
                    Continue();
                    goto StartFunctionRead;
                default:
                    if (Char.IsLetterOrDigit(ch) || ch == '_')
                        return ReadID();
                    else
                        throw new ParserException("无效的函数组成！");

            }
            throw new ParserException("无效！");
        }
        
        Token CreateToken(Token.TokenKind tokenKind)
        {
            Token token = new Token(saveLine, saveCol, data.Substring(savePos, currentPos - savePos),tokenKind);
            return token;
        }
        Token CreateToken(Token.TokenKind tokenKind, string data)
        {
            Token token = new Token(saveLine, saveCol, data, tokenKind);
            return token;
        }
        void EnterMode(LexerMode mode)
        {
            Mode.Push(currentMode);
            currentMode = mode;
        }

        void LeaveMode()
        {
            currentMode = Mode.Pop();
        }
        Token ReadID()
        {
            StartRead();
            Continue();

            while (true)
            {
                char ch = NextChar(0);
                if (Char.IsLetterOrDigit(ch) || ch == '_')
                    Continue();
                else
                    break;
            }
            return CreateToken(Token.TokenKind.ID);
        }
        Token ReadNumber()
        {
            StartRead();
            Continue();
            
            bool hasDot = false;

            while(true)
            {
                char ch=NextChar(0);
                if(Char.IsNumber(ch))
                Continue();
                
                else if(ch=='.'&&!hasDot&&Char.IsNumber(NextChar(0)))
                {
                    hasDot=true;
                    Continue();
                }
                else
                    break;
            }
            return CreateToken(hasDot?Token.TokenKind.Double:Token.TokenKind.Integer);
        }
        void ReadWhitespace()
        {
            while (true)
            {
                char ch = NextChar(0);
                switch (ch)
                {
                    case ' ':
                    case '\t':
                        Continue();
                        break;
                    case '\n':
                        Continue();
                        NewLine();
                        break;

                    case '\r':
                        Continue();
                        if (NextChar(0) == '\n')
                            Continue();
                        NewLine();
                        break;
                    default:
                        return;
                }
            }
        }
        void NewLine()
        {
            currentCol=1;
            currentLine++;
        }
    }
}
