using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Pieces;

namespace TetrisGame
{
    /// <summary>
    /// Handles the state and interactions between the pieces grid cells.
    /// </summary>
    public class StateInteraction
    {
        /// <summary>
        /// The score of the current game.
        /// </summary>
        public int GameScore { get; private set; }

        /// <summary>
        /// The next piece in the sequence of pieces.
        /// </summary>
        public NextPiece Next { get; }

        /// <summary>
        /// The current grid in the game.
        /// </summary>
        public Grid CurrentGrid { get; }

        /// <summary>
        /// Determines if the game is over or not.
        /// </summary>
        public bool GameOver { get; private set; }

        /// <summary>
        /// Private backing field for the "CurrentPiece".
        /// </summary>
        private Piece _currentPiece;

        /// <summary>
        /// Gets the current piece in the game grid.
        /// </summary>
        public Piece CurrentPiece
        {
            get => _currentPiece;
            private set
            {
                _currentPiece = value;
                _currentPiece.ChangeToDefault();

                for (int i = 0; i < 2; i++)
                {
                    _currentPiece.Shift(1, 0);

                    if (!CheckPosition())
                    {
                        _currentPiece.Shift(-1, 0);
                    }
                }
            }
        }

        /// <summary>
        /// The constructor for the "StateInteraction" class.
        /// </summary>
        public StateInteraction()
        {
            CurrentGrid = new Grid(22, 10);
            Next = new NextPiece();
            CurrentPiece = Next.Update();
        }

        /// <summary>
        /// Checks to see if the game is over.
        /// </summary>
        /// <returns>
        /// Returns true if the game is over, otherwise returns false.
        /// </returns>
        private bool CheckGameOver()
        {
            return !(CurrentGrid.CheckEmptyRow(0) && CurrentGrid.CheckEmptyRow(1));
        }

        /// <summary>
        /// Checks the piece's status and updates it's properties and score accordingly.
        /// </summary>
        private void MovePiece()
        {
            foreach (Cell c in CurrentPiece.GridLocations())
            {
                CurrentGrid[c.Row, c.Column] = CurrentPiece.PieceNum;
            }

            GameScore += CurrentGrid.EmptyFilledRows();

            if (CheckGameOver()) GameOver = true;
            else CurrentPiece = Next.Update();
        }

        /// <summary>
        /// Helper method to establish a "hard drop" function to drop the tile all the way to the bottom.
        /// </summary>
        /// <param name="c">The given cell.</param>
        /// <returns>
        /// The count of number of rows dropped.
        /// </returns>
        private int HelperHardDrop(Cell c)
        {
            int count = 0;
            while (CurrentGrid.CheckEmptyGrid(c.Row + count + 1, c.Column))
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Drops the piece to the bottom.
        /// </summary>
        /// <returns>
        /// The number of rows dropped.
        /// </returns>
        public int HardDropLength()
        {
            int rowsDropped = CurrentGrid.Rows;
            foreach (Cell c in CurrentPiece.GridLocations())
            {
                rowsDropped = System.Math.Min(rowsDropped, HelperHardDrop(c));
            }
            return rowsDropped;
        }

        /// <summary>
        /// Drops the piece to the bottom available row.
        /// </summary>
        public void HardDrop()
        {
            CurrentPiece.Shift(HardDropLength(), 0);
            MovePiece();
        }

        /// <summary>
        /// Checks to see if the piece can be rotated right, and does so if it can,
        /// otherwise it is rotated to the left.
        /// </summary>
        public void RotateRight()
        {
            CurrentPiece.RotateRight();
            if (!CheckPosition()) CurrentPiece.RotateLeft();
        }

        /// <summary>
        /// Checks to see if the piece can be rotated left, and does so if it can,
        /// otherwise it is rotated to the right.
        /// </summary>
        public void RotateLeft()
        {
            CurrentPiece.RotateLeft();

            if (!CheckPosition())
            {
                CurrentPiece.RotateRight();
            }
        }

        /// <summary>
        /// Checks to see if the piece can be shifted left, and does so if it can,
        /// otherwise it is rotated to the right.
        /// </summary>
        public void ShiftLeft()
        {
            CurrentPiece.Shift(0, -1);
            if (!CheckPosition()) CurrentPiece.Shift(0, 1);
        }

        /// <summary>
        /// Checks to see if the piece can be shifted right, and does so if it can,
        /// otherwise it is rotated to the left.
        /// </summary>
        public void ShiftRight()
        {
            CurrentPiece.Shift(0, 1);

            if (!CheckPosition())
            {
                CurrentPiece.Shift(0, -1);
            }
        }

        /// <summary>
        /// Checks to see if the piece can be shifted down, and does so if it can.
        /// </summary>
        public void ShiftDown()
        {
            CurrentPiece.Shift(1, 0);
            if (!CheckPosition())
            {
                CurrentPiece.Shift(-1, 0);
                MovePiece();
            }
        }

        /// <summary>
        /// Checks to see if the current piece is in a correct and legal position
        /// on the game grid.
        /// </summary>
        /// <returns>
        /// Returns false if not correct/legal, otherwise returns true.
        /// </returns>
        private bool CheckPosition()
        {
            foreach (Cell c in CurrentPiece.GridLocations())
            {
                if (!CurrentGrid.CheckEmptyGrid(c.Row, c.Column)) return false;
            }
            return true;
        }
    }
}
