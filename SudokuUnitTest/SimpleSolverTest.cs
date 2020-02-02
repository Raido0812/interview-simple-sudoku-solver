using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuUnitTest
{
    [TestFixture]
    public class SimpleSolverTest
    {
        [Test]
        public void TestSimpleSolver_TestCase1()
        {
            // arrange
            var testcase = GetTestCase1();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();
            var result = simpleSudokuSolver.SolveByElimination(testcase);

            // Assert
            Assert.IsNotNull(result.SudokuBoard);
            Assert.IsNotEmpty(result.ToString());
            Assert.AreEqual(0, result.NumberOfEmptyCells);
        }
        [Test]
        public void TestSimpleSolver_TestCase2()
        {
            // arrange
            var testcase = GetTestCase2();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();
            var result = simpleSudokuSolver.SolveByElimination(testcase);

            // Assert
            Assert.IsNotNull(result.SudokuBoard);
            Assert.IsNotEmpty(result.ToString());
            Assert.AreEqual(0, result.NumberOfEmptyCells);
        }

        [Test]
        public void TestSimpleSolver_RowValues()
        {
            // arrange
            var testcase = GetTestCase1();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();
            var result = simpleSudokuSolver.GetAllValuesFromRow(testcase, 2);

            // Assert
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(7));
        }

        [Test]
        public void TestSimpleSolver_ColumnValues()
        {
            // arrange
            var testcase = GetTestCase1();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();
            var result = simpleSudokuSolver.GetAllValuesFromColumn(testcase, 3);

            // Assert
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(4));
            Assert.IsTrue(result.Contains(5));
            Assert.IsTrue(result.Contains(8));
        }

        [Test]
        public void TestSimpleSolver_SectorValues()
        {
            // arrange
            var testcase = GetTestCase1();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();
            var result = simpleSudokuSolver.GetAllValuesFromSector(testcase, 2, 3);

            // Assert
            Assert.IsTrue(result.Contains(5));
            Assert.IsTrue(result.Contains(6));
            Assert.IsTrue(result.Contains(8));
        }

        [Test]
        public void TestSimpleSolver_DeterminingSector()
        {
            // arrange
            var testcase = GetTestCase1();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();

            // Assert
            Assert.AreEqual(0, simpleSudokuSolver.GetSector(0));
            Assert.AreEqual(0, simpleSudokuSolver.GetSector(1));
            Assert.AreEqual(0, simpleSudokuSolver.GetSector(2));
            Assert.AreEqual(1, simpleSudokuSolver.GetSector(3));
            Assert.AreEqual(1, simpleSudokuSolver.GetSector(4));
            Assert.AreEqual(1, simpleSudokuSolver.GetSector(5));
            Assert.AreEqual(2, simpleSudokuSolver.GetSector(6));
            Assert.AreEqual(2, simpleSudokuSolver.GetSector(7));
            Assert.AreEqual(2, simpleSudokuSolver.GetSector(8));
        }


        [Test]
        public void TestSimpleSolver_DeterminingInvalidValuesInRestOfSector()
        {
            // arrange
            var testcase = GetTestCase1();

            // Action
            var simpleSudokuSolver = new SimpleSudokuSolver();
            var result = simpleSudokuSolver.GetAllInvalidValuesFromRestOfSector(testcase, 2, 3);

            // Assert
            Assert.AreEqual(4,result.Count);
            Assert.IsTrue(result.Contains(5));
            Assert.IsTrue(result.Contains(6));
            Assert.IsTrue(result.Contains(8));
            Assert.IsTrue(result.Contains(9));
        }


        private int?[,] GetTestCase1()
        {
            return new int?[,] {
                { 4,    5,      null,   8,      null,   null,   9,      null,   null },
                { null, 9,      null,   null,   5,      6,      null,   null,   4    },
                { 1,    null,   null,   null,   null,   null,   null,   null,   7    },
                { 2,    6,      null,   5,      4,      null,   null,   9,      null },
                { null, null,   4,      1,      null,   2,      3,      null,   null },
                { null, 7,      null,   null,   6,      9,      null,   4,      8    },
                { 7,    null,   null,   null,   null,   null,   null,   null,   9    },
                { 8,    null,   null,   4,      9,      null,   null,   7,      null },
                { null, null,   9,      null,   null,   3,      null,   2,      5    },
            };
        }

        private int?[,] GetTestCase2()
        {
            return new int?[,] {
                { 3,    6,      null,   2,      null,   5,      null,   null,   null },
                { null, 1,      5,      4,      null,   3,      null,   8,      null },
                { null, null,   4,      9,      1,      null,   null,   null,   null },
                { 4,    5,      7,      null,   null,   null,   null,   9,      1    },
                { null, null,   2,      null,   null,   null,   3,      null,   null },
                { 8,    3,      null,   null,   null,   null,   7,      6,      4    },
                { null, null,   null,   null,   9,      4,      8,      null,   null },
                { null, 2,      null,   3,      null,   6,      1,      4,      null },
                { null, null,   null,   8,      null,   2,      null,   7,      9    },
            };
        }
    }
}
