using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CompositePattern
{
    //Consider the code presented below.
    //The Sum() extension method adds up all the values in a list of IValueContainer elements it gets passed.
    //We can have a single value or a set of values.
    //Complete the implementation of the interfaces so that Sum() begins to work correctly.
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
      public int Value;
      public IEnumerator<int> GetEnumerator()
      {
          yield return Value;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
          return GetEnumerator();
      }
    }

    public class ManyValues : List<IValueContainer>
    {
        public List<int> Values;
    }

    public static class ExtensionMethods
    {
      public static int Sum(this List<IValueContainer> containers)
      {
        int result = 0;
        foreach (var c in containers)
        foreach (var i in c)
          result += i;
        return result;
      }
    }

    class Program
    {
        static void Main(string[] args)
        {
          var single = new SingleValue {Value = 3};
          Console.WriteLine($"Single sum: {single.Sum()}");

          var many = new ManyValues { Values = new List<int> {12, 6}};
          Console.WriteLine($"Single sum: {many.Sum()}");
          Console.ReadKey();
        }
    }
}
