using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Pieces
{
    /// <summary>
    /// The class for the piece that looks like an "O".
    /// </summary>
    public class PieceO : Piece
    {
        /// <summary>
        /// The 4 orientations of the piece depending on rotation.
        /// </summary>
        private readonly Cell[][] cells = new Cell[][]
        {
            //We don't need to established all 4 orientations for this block because rotating the
            //square piece will have it in the same spot anyway, so 1 spot will suffice.
            new Cell[] { new(0,0), new(0,1), new(1,0), new(1,1) }
        };

        /// <summary>
        /// Inherits the previously established list of cells from the piece class.
        /// </summary>
        protected override Cell[][] Cells => cells;

        /// <summary>
        /// Spawns the piece in the correct spot and orientation.
        /// </summary>
        protected override Cell StartingPoint => new Cell(0, 4);

        /// <summary>
        /// The piece number specific to that piece.
        /// </summary>
        public override int PieceNum => 4;
    }
}
