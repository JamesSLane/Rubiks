using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static RubikCube.Global.Enums;

namespace RubikCube.Objects
{
    public class Cube
    {
        private Side[] _Sides = new Side[6];
        public Cube() 
        {
            //Setup Cube
            int index = 0;
            var ColourAsArray = Enum.GetNames(typeof(Global.Enums.Colour));
            foreach (Global.Enums.Side face in (Global.Enums.Side[])Enum.GetValues(typeof(Global.Enums.Side)))
            {
                Side newSide = new Side(face, (Global.Enums.Colour)Enum.ToObject(typeof(Global.Enums.Colour), 
                    Enum.Parse(typeof(Global.Enums.Colour), ColourAsArray[index])));

                _Sides[index] = newSide;
                index++;
            }
        }

        public void PrintNetToConsole()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\nRubiks cube simulator\n\n");

            //Top
            _Sides[(int)Global.Enums.Side.Top].PrintSide();

            //Left , Front, Right, Back
            for (int x = 0; x <= 2; x++)
            {
                Console.Write($"\n");
                _Sides[(int)Global.Enums.Side.Left].PrintSideSingleRow(x);
                _Sides[(int)Global.Enums.Side.Front].PrintSideSingleRow(x);
                _Sides[(int)Global.Enums.Side.Right].PrintSideSingleRow(x);
                _Sides[(int)Global.Enums.Side.Back].PrintSideSingleRow(x);
            }

            //Bottom
            _Sides[(int)Global.Enums.Side.Bottom].PrintSide();
        }

        public void PerformRotate(Global.Enums.Side side, Direction direction)
        {
            //Roate whole side by 90 degs
            _Sides[(int)side].RotateSide(direction);

            //Next deal with adjacent rows on ajoinging faces
            Global.Enums.Colour[] rowToCopy = new Global.Enums.Colour[3];
            Global.Enums.Colour[] nextRow = new Global.Enums.Colour[3];
            if (direction == Direction.CW)
            {
                rowToCopy = GetAdjacentRowToCopy(side, 0);
                for (int i = 0; i < 3; i++)
                {
                    //Store the next row before we overwrite it
                    nextRow = GetAdjacentRowToCopy(side, i + 1);
                    CopyOverAdjacentRow(side, i + 1, rowToCopy, InvertIndicesRequired(side, direction, i + 1, i));
                    rowToCopy = nextRow;
                }
                //Copy over last row
                rowToCopy = nextRow;
                CopyOverAdjacentRow(side, 0, rowToCopy, InvertIndicesRequired(side, direction, 0, 3));
            }
            else
            {
                rowToCopy = GetAdjacentRowToCopy(side, 3);
                for (int i = 3; i > 0; i--)
                {
                    //Store the next row before we overwrite it
                    nextRow = GetAdjacentRowToCopy(side, i - 1);
                    CopyOverAdjacentRow(side, i - 1, rowToCopy, InvertIndicesRequired(side, direction, i - 1, i));
                    rowToCopy = nextRow;
                }
                //Copy over last row
                rowToCopy = nextRow;
                CopyOverAdjacentRow(side, 3, rowToCopy, InvertIndicesRequired(side, direction, 0, 3));
            }
        }

        private bool InvertIndicesRequired(Global.Enums.Side side, Direction direction, int nextIndex, int currentIndex)
        {
            //Some rotations on the adjacent sides will need the row order inverted check if this is needed
            bool invertNeeded = false;

            Global.Enums.Side nextSide = _Sides[(int)side].AdjacentSides[nextIndex].Side;
            Global.Enums.Side currentSide = _Sides[(int)side].AdjacentSides[currentIndex].Side;

            if (((nextSide == Global.Enums.Side.Left || nextSide == Global.Enums.Side.Back) && currentSide == Global.Enums.Side.Top) ||
                ((nextSide == Global.Enums.Side.Right || nextSide == Global.Enums.Side.Back)&& currentSide == Global.Enums.Side.Bottom) ||
                ((nextSide == Global.Enums.Side.Bottom || nextSide == Global.Enums.Side.Top) && currentSide == Global.Enums.Side.Back) ||
                (nextSide == Global.Enums.Side.Top && currentSide == Global.Enums.Side.Left) ||
                (nextSide == Global.Enums.Side.Bottom && currentSide == Global.Enums.Side.Right))
            {
                invertNeeded = true;
            }

            return invertNeeded;
        }

        private Global.Enums.Colour[] GetAdjacentRowToCopy(Global.Enums.Side side, int adjacentIndex)
        {
            Global.Enums.Colour[] rowStore = new Global.Enums.Colour[3];

            Global.Enums.Side sideTopCopy = _Sides[(int)side].AdjacentSides[adjacentIndex].Side;
            if (_Sides[(int)side].AdjacentSides[adjacentIndex].AdjacentXRow != null)
            {
                int x = (int)_Sides[(int)side].AdjacentSides[adjacentIndex].AdjacentXRow;
                for (int count = 0; count <= 2; count++)
                {
                    rowStore[count] = _Sides[(int)sideTopCopy].Blocks[x, count];
                }
            }
            else
            {
                int y = (int)_Sides[(int)side].AdjacentSides[adjacentIndex].AdjacentYRow;
                for (int count = 0; count <= 2; count++)
                {
                    rowStore[count] = _Sides[(int)sideTopCopy].Blocks[count, y];
                }
            }

            return rowStore;
        }

        private void CopyOverAdjacentRow(Global.Enums.Side side, int adjacentIndex, Global.Enums.Colour[] rowToCopy, bool invert)
        {
            if(invert)
            {
                Array.Reverse(rowToCopy);
            }

            Global.Enums.Side sideToCopyTo = _Sides[(int)side].AdjacentSides[adjacentIndex].Side;

            if (_Sides[(int)side].AdjacentSides[adjacentIndex].AdjacentXRow != null)
            {
                int xcopyto = (int)_Sides[(int)side].AdjacentSides[adjacentIndex].AdjacentXRow;
                for (int count = 0; count <= 2; count++)
                {
                    _Sides[(int)sideToCopyTo].Blocks[xcopyto, count] = rowToCopy[count];
                }
            }
            else
            {
                int ycopyto = (int)_Sides[(int)side].AdjacentSides[adjacentIndex].AdjacentYRow;
                for (int count = 0; count <= 2; count++)
                {
                    _Sides[(int)sideToCopyTo].Blocks[count, ycopyto] = rowToCopy[count];
                }
            }
        }
    }
}
