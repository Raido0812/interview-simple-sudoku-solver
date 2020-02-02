using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SimpleSudokuSolver
    {
        private static int[] FullSet = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public SudokuResult SolveByElimination(int?[,] sudokuBoard)
        {
            var result = new SudokuResult
            {
                SudokuBoard = sudokuBoard
            };

            while (result.NumberOfEmptyCells > 0 && result.NumberOfIterations < 1000)
            {
                // pick a cell
                for (var row = 0; row < result.SudokuBoard.GetLength(0); row++)
                {
                    for (var column = 0; column < result.SudokuBoard.GetLength(1); column++)
                    {
                        // cell already solved, move to next
                        if (result.SudokuBoard[row, column].HasValue)
                        {
                            continue;
                        }

                        // find all numbers that can't be in cell
                        var numbersEliminatedByColumn = GetAllValuesFromColumn(result.SudokuBoard, column);
                        var numbersEliminatedByRow = GetAllValuesFromRow(result.SudokuBoard, row);
                        var numbersEliminatedBySector = GetAllValuesFromSector(result.SudokuBoard, row, column);

                        // intersect numbers that can't be in other cells with numbers that can be in cell 
                        var validValues = FullSet
                            .Except(numbersEliminatedByColumn)
                            .Except(numbersEliminatedByRow)
                            .Except(numbersEliminatedBySector)
                            .ToList();
                        if (validValues.Count != 1)
                        {
                            // find all numbers that can't be in other cells in sector?
                            var numbersThatCantGoIntoOtherCellsInSector = GetAllInvalidValuesFromRestOfSector(result.SudokuBoard, row, column);
                            validValues = validValues.Intersect(numbersThatCantGoIntoOtherCellsInSector).ToList();
                        }

                        if (validValues.Count == 1)
                        {
                            result.SudokuBoard[row, column] = validValues.First();
                        }
                    }
                }

                result.NumberOfIterations++;
            }

            return result;
        }

        public int GetSector(int cellIndex)
        {
            return (int)Math.Floor(cellIndex / 3d);
        }

        public IList<int> GetAllValuesFromColumn(int?[,] sudokuBoard, int columnIndex)
        {
            var values = new List<int>();
            for (var row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (sudokuBoard[row, columnIndex].HasValue)
                {
                    values.Add(sudokuBoard[row, columnIndex].Value);
                }
            }

            return values;
        }
        public IList<int> GetAllValuesFromRow(int?[,] sudokuBoard, int rowIndex)
        {
            var values = new List<int>();
            for (var column = 0; column < sudokuBoard.GetLength(1); column++)
            {
                if (sudokuBoard[rowIndex, column].HasValue)
                {
                    values.Add(sudokuBoard[rowIndex, column].Value);
                }
            }

            return values;
        }

        public IList<int> GetAllValuesFromSector(int?[,] sudokuBoard, int rowIndex, int columnIndex)
        {
            var rowSector = GetSector(rowIndex);
            var columnSector = GetSector(columnIndex);
            var values = new List<int>();

            for (var row = rowSector * 3; row < rowSector * 3 + 3; row++)
            {
                for (var column = columnSector * 3; column < columnSector * 3 + 3; column++)
                {
                    if (sudokuBoard[row, column].HasValue)
                    {
                        values.Add(sudokuBoard[row, column].Value);
                    }
                }
            }

            return values;
        }

        public IList<int> GetAllInvalidValuesFromRestOfSector(int?[,] sudokuBoard, int rowIndex, int columnIndex)
        {
            var rowSector = GetSector(rowIndex);
            var columnSector = GetSector(columnIndex);
            var values = new List<List<int>>();

            for (var row = rowSector * 3; row < rowSector * 3 + 3; row++)
            {
                for (var column = columnSector * 3; column < columnSector * 3 + 3; column++)
                {
                    if ((row == rowIndex && column == columnIndex) || sudokuBoard[row, column].HasValue)
                    {
                        continue;
                    }

                    // find values that can't be in this cell

                    var numbersEliminatedByColumn = GetAllValuesFromColumn(sudokuBoard, column);
                    var numbersEliminatedByRow = GetAllValuesFromRow(sudokuBoard, row);
                    var numbersEliminatedBySector = GetAllValuesFromSector(sudokuBoard, row, column);
                    values.Add(
                        numbersEliminatedByColumn
                            .Union(numbersEliminatedByRow)
                            .Union(numbersEliminatedByRow)
                            .Union(numbersEliminatedBySector)
                            .ToList());
                }
            }

            // only values that can't be in any of empty cells is important
            var numberOfEmptyCells = values.Count;
            return values
                .SelectMany(cell => cell)
                .GroupBy(cellValue => cellValue)
                .Where(group => group.Count() == numberOfEmptyCells)
                .Select(group => group.Key)
                .ToList();
        }
    }
}
