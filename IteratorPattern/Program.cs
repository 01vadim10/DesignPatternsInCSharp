﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace IteratorPattern
{
    class Program
    {
        public class Node<T>
        {
            public T Value;
            public Node<T> Left, Right;
            public Node<T> Parent;

            public Node(T value)
            {
                Value = value;
            }

            public Node(T value, Node<T> left, Node<T> right)
            {
                Value = value;
                Left = left;
                Right = right;

                left.Parent = right.Parent = this;
                // Parent = this;
            }

            public Node<T> GetEnumerator()
            {
                return new Node<T>(Parent.Value, Left, Right);
            }

            IEnumerable<T> Traverse(Node<T> current)
            {
                if (current != null)
                {
                    yield return current.Value;
                }

                if (current.Left != null)
                {
                    foreach (var left in Traverse(current.Left))
                        yield return left;
                }

                if (current.Right != null)
                {
                    foreach (var right in Traverse(current.Right))
                        yield return right;
                }
            }

            public IEnumerable<T> PreOrder
            {
                get
                {
                    foreach (var node in Traverse(this))
                    {
                        yield return node;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
        }

        [TestFixture]
        public class TestSuite
        {
            [Test]
            public void Test()
            {
                var node = new Node<char>('a',
                    new Node<char>('b',
                        new Node<char>('c'),
                        new Node<char>('d')),
                    new Node<char>('e'));
                Assert.That(new string(node.PreOrder.ToArray()), Is.EqualTo("abcde"));
            }
        }
    }
}
