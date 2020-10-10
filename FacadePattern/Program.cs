using System;
using System.Collections.Generic;
using System.Linq;

namespace FacadePattern
{
    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => 0)
                .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    public class MagicSquareGenerator
    {
        public List<List<int>> Generate(int size)
        {
            List<List<int>> matrix = new List<List<int>>();
            var generator = new Generator();
            var splitter = new Splitter();
            var verifier = new Verifier();

            for (int i = 0; i < size; i++)
            {
                matrix.Add(generator.Generate(size));
            }

            var poweredSize = Math.Pow(size, 2);

            for (int i = 1, row = 0, column = size / 2; i <= poweredSize; i++)
            {
                if (row < 0 && column < size && column > 0)
                {
                    row = size - 1;
                }
                else if (row >= 0 && row < size && column >= size)
                {
                    column = 0;
                }
                else if (row <= size && column >= size // outside both a row and a column
                         || matrix[row][column] > 0) // the cell is already filled
                {
                    row += 2;
                    column--;
                }
                matrix[row][column] = i;
                row--;
                column++;
            }

            var splitMatrix = splitter.Split(matrix);
            if (verifier.Verify(splitMatrix))
            {
                return splitMatrix;
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var magicSquareGenerator = new MagicSquareGenerator();
            var magicSquare = magicSquareGenerator.Generate(3);
            Console.WriteLine(magicSquare);
            Console.ReadKey();
        }
    }
}
