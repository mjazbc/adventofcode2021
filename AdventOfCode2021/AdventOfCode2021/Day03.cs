using aoc_core;
using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021
{
    internal class Day03  : AdventPuzzle
    {
        private readonly MatrixBuilder<double> M = Matrix<double>.Build;
        private readonly VectorBuilder<double> V = Vector<double>.Build;

        public override string SolveFirstPuzzle()
        {
            var matrix = M.DenseOfArray(Input.AsNumericMatrix());
            var i = V.Dense(matrix.RowCount, 1);

            var result = i * matrix;
            var bitResults = result / (matrix.RowCount / 2);
            int gamma = VectorToBinary(bitResults);
            int epsilon = gamma ^ VectorToBinary(V.Dense(matrix.ColumnCount, 1));

            return (gamma * epsilon).ToString();

        } 

        public override string SolveSecondPuzzle()
        {
            var m = Input.AsCustomTypeEnumerable(x => x.ToCharArray()).ToArray();
            int oxygen = FindRating(m, (x, y) => x >= y);

            m = Input.AsCustomTypeEnumerable(x => x.ToCharArray()).ToArray();
            int co2 = FindRating(m, (x, y) => x < y);


            return (oxygen * co2).ToString();
        }

        private static int FindRating(char[][] m, Func<int, double, bool> op)
        {
            for (int bit = 0; bit < m[0].Length; bit++)
            {
                var prominent = op(m.Count(x => x[bit] == '1'), m.Length / 2.0) ? '1' : '0';
                m = m.Where(x => x[bit] == prominent).ToArray();

                if (m.Length == 1)
                    break;
            }

            return Convert.ToInt32(string.Join("", m.Single()), 2);
        }

        private static int VectorToBinary(Vector<double> bitResults)
        {
            return Convert.ToInt32(string.Join("", bitResults.Select(x => ((int)x).ToString())), 2);
        }
    }
}