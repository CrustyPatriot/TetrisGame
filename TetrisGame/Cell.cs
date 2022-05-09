using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    /// <summary>
    /// The specific cell in the game board.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The specific row of the cell.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The specific column of the cell.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Cell constructor
        /// </summary>
        /// <param name="r">The row</param>
        /// <param name="c">The column.</param>
        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
