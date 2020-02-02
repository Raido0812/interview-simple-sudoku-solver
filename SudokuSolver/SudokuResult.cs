using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuResult
    {
        public int NumberOfIterations { get; set; }
        public int?[,] SudokuBoard { get; set; }
        public int NumberOfEmptyCells
        {
            get
            {
                var count = 0;
                for (var i = 0;i < SudokuBoard?.GetLength(0); i++)
                {
                    for (var j = 0; j < SudokuBoard?.GetLength(1); j++)
                    {
                        if (!SudokuBoard[i,j].HasValue)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < SudokuBoard?.GetLength(0); i++)
            {
                for (var j = 0; j < SudokuBoard?.GetLength(1); j++)
                {
                    if (SudokuBoard[i, j].HasValue)
                    {
                        stringBuilder.Append($"{SudokuBoard[i, j].Value}|");
                    }
                    else
                    {
                        stringBuilder.Append(" |");
                    }
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
