using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterPattern.Coding
{
    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public List<Token> Lex(string input)
        {
            var result = new List<Token>();

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    default:
                        if (char.IsDigit(input[i]))
                        {
                            var sb = new StringBuilder(input[i].ToString());
                            if (i != input.Length - 1)
                            {
                                for (int j = i + 1; j < input.Length; ++j)
                                {
                                    if (char.IsDigit(input[j]))
                                    {
                                        sb.Append(input[j]);
                                        ++i;
                                    }
                                    else
                                    {
                                        result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                break;
                            }
                        }
                        else
                        {
                            if (char.IsLetter(input[i]) && Variables.TryGetValue(input[i], out var varValue))
                            {
                                result.Add(new Token(Token.Type.Integer, varValue.ToString()));
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Unknown, input[i].ToString()));
                                return result;
                            }
                        }
                        break;
                }
            }

            return result;
        }

        public int Calculate(string expression)
        {
            var tokens = Lex(expression);

            if (tokens.LastOrDefault()?.MyType == Token.Type.Unknown)
            {
                return 0;
            }

            var result = new BinaryOperation();
            bool haveLHS = false;

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];

                switch (token.MyType)
                {
                    case Token.Type.Integer:
                        var integer = new Integer(int.Parse(token.Text));
                        if (!haveLHS)
                        {
                            result.Left = integer;
                            haveLHS = true;
                        }
                        else
                        {
                            result.Right = integer;
                            if (i < tokens.Count - 1)
                            {
                                result.Left = new BinaryOperation
                                {
                                    Left = result.Left,
                                    Right = result.Right,
                                    OperationType = result.OperationType
                                };
                            }
                        }
                        break;
                    case Token.Type.Plus:
                        result.OperationType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.OperationType = BinaryOperation.Type.Subtraction;
                        break;
                }
            }

            return result.Value;
        }
    }

    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition,
            Subtraction
        }

        public Type OperationType;
        public IElement Left, Right;

        public int Value
        {
            get
            {
                switch (OperationType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Unknown
        }

        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
