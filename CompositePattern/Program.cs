using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        yield return Value;
      }
    }

    public class ManyValues : List<int>, IValueContainer
    {
      public List<int> Values { get; set; }

      IEnumerator<int> IEnumerable<int>.GetEnumerator()
      {
        foreach (var value in Values)
        {
          yield return value;
        }
      }
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

          var many = new ManyValues { Values = new List<int> {12, 7, 6}};
          Console.WriteLine($"Single sum: {many.Sum()}");
          Console.ReadKey();
        }
    }
}
