using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Calculate(string expression)
        {
            // todo
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
