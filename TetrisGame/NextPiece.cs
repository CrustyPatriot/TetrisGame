using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Pieces;

namespace TetrisGame
{
    /// <summary>
    /// Class that determines the next piece in the sequence.
    /// </summary>
    public class NextPiece
    {
        /// <summary>
        /// The next piece to be chosen.
        /// </summary>
        public Piece SecondPiece { get; private set; }

        /// <summary>
        /// A random field to choose random piece from the array of pieces.
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// An array with every block to choose the next random piece.
        /// </summary>
        private readonly Piece[] pieces = new Piece[]
        {
            new PieceI(),
            new PieceJ(),
            new PieceL(),
            new PieceO(),
            new PieceS(),
            new PieceT(),
            new PieceZ()
        };

        /// <summary>
        /// Updates the grid with the next piece.
        /// </summary>
        /// <returns>Returns the next piece in the grid.</returns>
        public Piece Update()
        {
            Piece piece = SecondPiece;

            do
            {
                SecondPiece = Random();
            }
            while (piece.PieceNum == SecondPiece.PieceNum);
            return piece;
        }

        /// <summary>
        /// Chooses the next piece at random.
        /// </summary>
        /// <returns>Returns the next random piece.</returns>
        private Piece Random()
        {
            return pieces[random.Next(pieces.Length)];
        }

        /// <summary>
        /// The constructor for setting the next piece in the sequence for the game.
        /// </summary>
        public NextPiece()
        {
            SecondPiece = Random();
        }
    }
}
