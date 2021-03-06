﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace InterpreterPattern
{
    /// <summary>
    /// You are asked to write an expression processor for simple numeric expressions with the following constraints:
    /// - Expressions use integral values (e.g., '13' ), single-letter variables defined in Variables, as well as + and - operators only
    /// - There is no need to support braces or any other operations
    /// - If a variable is not found in Variables (or if we encounter a variable with >1 letter, e.g. ab), the evaluator returns 0 (zero)
    /// - In case of any parsing failure, evaluator returns 0
    /// Example:
    /// Calculate("1+2+3")  should return 6
    /// Calculate("1+2+xy")  should return 0
    /// Calculate("10-2-x")  when x=3 is in  should return 5
    /// </summary>
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
                            bool isValueInVariables = false;
                            int varValue = 0;
                            try
                            {
                                isValueInVariables = Variables.TryGetValue(input[i], out varValue);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            if (char.IsLetter(input[i]) && isValueInVariables)
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
            var lastToken = tokens[tokens.Count - 1];
            if (lastToken.MyType == Token.Type.Unknown)
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
            None,
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
                    case Type.None:
                        if (Left != null)
                        {
                            return Left.Value;
                        }

                        return 0;
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

    class Program
    {
        static void Main(string[] args)
        {
            var expressionProcessor = new ExpressionProcessor();
            expressionProcessor.Variables.Add('x', 3);
            var result = expressionProcessor.Calculate("1+2+3");
            var result2 = expressionProcessor.Calculate("1+2+xy");
            var result3 = expressionProcessor.Calculate("10-2-x");
        }
    }

    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 5);

            Assert.That(ep.Calculate("1"), Is.EqualTo(1));

            Assert.That(ep.Calculate("1+2"), Is.EqualTo(3));

            Assert.That(ep.Calculate("1+x"), Is.EqualTo(6));

            Assert.That(ep.Calculate("1+xy"), Is.EqualTo(0));
        }
    }
}
