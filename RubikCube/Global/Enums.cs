using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikCube.Global
{
    public class Enums
    {
        public enum Direction
        {
            CCW, 
            CW
        }
        public enum Colour
        {
            Red =12,
            Green=2,
            Blue=9,
            White=15,
            Yellow=14,
            Orange=6
        }

        public enum Side
        {
            Front,
            Left,
            Back,
            Right,
            Bottom,
            Top
        }
    }
}
