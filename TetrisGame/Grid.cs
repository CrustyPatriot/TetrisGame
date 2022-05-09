using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    /// <summary>
    /// The class that contains the grid for the tetris game.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// The game grid for all the moves to take place on.
        /// </summary>
        private readonly int[,] gameGrid;

        /// <summary>
        /// The rows of the game grid.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// The columns of the game grid.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Gets the correct row and column for the specific grid square to index into the game.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The correct index of the grid.</returns>
        public int this[int row, int column]
        {
            get => gameGrid[row, column];
            set => gameGrid[row, column] = value;
        }

        /// <summary>
        /// Empties all the rows that are filled.
        /// </summary>
        /// <returns>Returns the number of rows that were emptied.</returns>
        public int EmptyFilledRows()
        {
            int rowsEmptied = 0;

            for (int rows = Rows - 1; rows >= 0; rows--)
            {
                if (CheckFullRow(rows))
                {
                    EmptyRow(rows);
                    rowsEmptied++;
                }
                else if (rowsEmptied > 0) ShiftRowDown(rows, rowsEmptied);
            }
            return rowsEmptied;
        }

        /// <summary>
        /// Shifts the given row down by a certain number.
        /// </summary>
        /// <param name="row">The given row.</param>
        /// <param name="rowNumber">The certain number to shift down.</param>
        private void ShiftRowDown(int row, int rowNumber)
        {
            for (int c = 0; c < Columns; c++)
            {
                gameGrid[row + rowNumber, c] = gameGrid[row, c];
                gameGrid[row, c] = 0;
            }
        }

        /// <summary>
        /// Empty's out the row.
        /// </summary>
        /// <param name="row">The given row.</param>
        private void EmptyRow(int row)
        {
            for (int c = 0; c < Columns; c++)
            {
                gameGrid[row, c] = 0;
            }
        }

        /// <summary>
        /// Checks to see if the given row is empty.
        /// </summary>
        /// <param name="row">The given row.</param>
        /// <returns>Returns true if the given row is empty, else returns false.</returns>
        public bool CheckEmptyRow(int row)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (gameGrid[row, c] != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks to see if the given row is full.
        /// </summary>
        /// <param name="row">The given row.</param>
        /// <returns>Returns true if it is full, else returns false.</returns>
        public bool CheckFullRow(int row)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (gameGrid[row, c] == 0) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the specific section of the grid is empty or not.
        /// </summary>
        /// <param name="row">The given row.</param>
        /// <param name="column">The given column.</param>
        /// <returns>Returns true if it is empty, else returns false.</returns>
        public bool CheckEmptyGrid(int row, int column)
        {
            return CheckInsideGrid(row, column) && gameGrid[row, column] == 0;
        }

        /// <summary>
        /// Checks to see if the given row and column is inside the game grid.
        /// </summary>
        /// <param name="rows">The given rows.</param>
        /// <param name="columns">The given columns.</param>
        /// <returns>Returns true if it is inside, else returns false.</returns>
        public bool CheckInsideGrid(int rows, int columns)
        {
            return rows >= 0 && rows < Rows && columns >= 0 && columns < Columns;
        }

        /// <summary>
        /// The constructor for the grid.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            gameGrid = new int[rows, columns];
        }
    }
}
