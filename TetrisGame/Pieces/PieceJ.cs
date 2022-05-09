using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Pieces
{
    /// <summary>
    /// The class for the piece that looks like a "J"
    /// </summary>
    public class PieceJ : Piece
    {
        /// <summary>
        /// The 4 orientations of the piece depending on rotation.
        /// </summary>
        protected override Cell[][] Cells => new Cell[][] {
            new Cell[] {new(0, 0), new(1, 0), new(1, 1), new(1, 2)},
            new Cell[] {new(0, 1), new(0, 2), new(1, 1), new(2, 1)},
            new Cell[] {new(1, 0), new(1, 1), new(1, 2), new(2, 2)},
            new Cell[] {new(0, 1), new(1, 1), new(2, 1), new(2, 0)}
        };

        /// <summary>
        /// Spawns the piece in the correct spot and orientation.
        /// </summary>
        protected override Cell StartingPoint => new(0, 3);

        /// <summary>
        /// The piece number specific to that piece.
        /// </summary>
        public override int PieceNum => 2;
    }
}
