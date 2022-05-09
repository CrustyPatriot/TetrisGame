using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Pieces
{
    /// <summary>
    /// The individual game piece class to be inherited by each individual game piece.
    /// </summary>
    public abstract class Piece
    {
        /// <summary>
        /// A way to identify which piece is being used.
        /// </summary>
        public abstract int PieceNum { get; }

        /// <summary>
        /// Contains the positions of the blocks in the game grid.
        /// </summary>
        protected abstract Cell[][] Cells { get; }

        /// <summary>
        /// The place that the piece starts on the game grid.
        /// </summary>
        protected abstract Cell StartingPoint { get; }

        private Cell pieceLocation;
        private int pieceOrientation;

        /// <summary>
        /// Changes the piece's position to the default state.
        /// </summary>
        public void ChangeToDefault()
        {
            pieceOrientation = 0;
            pieceLocation.Row = StartingPoint.Row;
            pieceLocation.Column = StartingPoint.Column;
        }

        /// <summary>
        /// Shifts the piece in relation to number of rows and columns.
        /// </summary>
        /// <param name="row">The number of rows.</param>
        /// <param name="column">The number of columns.</param>
        public void Shift(int rows, int columns)
        {
            pieceLocation.Row += rows;
            pieceLocation.Column += columns;
        }

        /// <summary>
        /// Rotates the piece to the left.
        /// </summary>
        public void RotateLeft()
        {
            if (pieceOrientation == 0)
            {
                pieceOrientation = Cells.Length - 1;
            }
            else
            {
                pieceOrientation--;
            }
        }

        /// <summary>
        /// Rotates the piece to the right.
        /// </summary>
        public void RotateRight()
        {
            pieceOrientation = (pieceOrientation + 1) % Cells.Length;
        }

        /// <summary>
        /// Loops through all cells and returns all locations for the cells around the piece.
        /// </summary>
        /// <returns>Returns all locations for the cells around the piece.</returns>
        public IEnumerable<Cell> GridLocations()
        {
            foreach (Cell p in Cells[pieceOrientation])
            {
                yield return new Cell(p.Row + pieceLocation.Row, p.Column + pieceLocation.Column);
            }
        }

        /// <summary>
        /// Constructor for the game pieces.
        /// </summary>
        public Piece()
        {
            pieceLocation = new Cell(StartingPoint.Row, StartingPoint.Column);
        }
    }
}
