using RubikCube.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikCube.Objects
{
    public class AdjacentSide
    {
        private Global.Enums.Side _side;
        public Enums.Side Side { get => _side; set => _side = value; }

        private int? _adjacentXRow;
        public int? AdjacentXRow { get => _adjacentXRow; set => _adjacentXRow = value; }

        private int? _adjacentYRow;
        public int? AdjacentYRow { get => _adjacentYRow; set => _adjacentYRow = value; }

        public AdjacentSide(Global.Enums.Side side, int? adjacentXRow, int? adjacentYRow)
        {
            Side = side;
            AdjacentXRow = adjacentXRow;
            AdjacentYRow = adjacentYRow;
        }
    }
}
