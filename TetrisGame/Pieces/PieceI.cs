using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Pieces
{
    /// <summary>
    /// The class for the Piece that looks like an "I".
    /// </summary>
    public class PieceI : Piece
    {
        /// <summary>
        /// The 4 orientations of the piece depending on rotation.
        /// </summary>
        private readonly Cell[][] cells = new Cell[][]
        {
            new Cell[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new Cell[] { new(0,2), new(1,2), new(2,2), new(3,2) },
            new Cell[] { new(2,0), new(2,1), new(2,2), new(2,3) },
            new Cell[] { new(0,1), new(1,1), new(2,1), new(3,1) }
        };

        /// <summary>
        /// Inherits the previously established list of cells from the piece class.
        /// </summary>
        protected override Cell[][] Cells => cells;

        /// <summary>
        /// Spawns the piece at the correct spot and orientation.
        /// </summary>
        protected override Cell StartingPoint => new Cell(-1, 3);

        /// <summary>
        /// The piece number specific to that piece.
        /// </summary>
        public override int PieceNum => 1;
    }
}
