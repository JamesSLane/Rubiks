using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RubikCube.Global.Enums;

namespace RubikCube.Objects
{
    public class Side
    {
        private Global.Enums.Colour[,] _blocks = new Global.Enums.Colour[3, 3];
        public Colour[,] Blocks { get => _blocks; }

        private AdjacentSides _adjacentSides = new AdjacentSides();
        public AdjacentSides AdjacentSides { get => _adjacentSides; }

        public Side(Global.Enums.Side face, Global.Enums.Colour colour) 
        {
            FillSideWithColour(colour);

            //Setup which sides are ajoining, and which array index on that face thouches the current face
            switch(face)
            {
                case Global.Enums.Side.Front:
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Left, null, 2));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Top, 2, null ));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Right, null, 0));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Bottom, 0, null));
                    break;
                case Global.Enums.Side.Left:
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Front, null, 0));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Bottom, null, 0));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Back, null, 2));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Top, null, 0));
                    break;
                case Global.Enums.Side.Right:
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Front, null, 2));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Top, null, 2));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Back, null, 0));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Bottom, null, 2));
                    break;
                case Global.Enums.Side.Back:
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Top, 0, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Left, null, 0));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Bottom, 2, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Right, null, 2));
                    break;
                case Global.Enums.Side.Bottom:
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Front, 2, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Right, 2, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Back, 2, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Left, 2, null));
                    break;
                case Global.Enums.Side.Top:
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Front, 0, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Left, 0, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Back, 0, null));
                    _adjacentSides.Add(new AdjacentSide(Global.Enums.Side.Right, 0, null));
                    break;
            }
        }

        private void FillSideWithColour(Global.Enums.Colour colour)
        {
            for (int x = 0; x < _blocks.GetLength(0); x++)
            {
                for (int y = 0; y < _blocks.GetLength(1); y++)
                {
                    _blocks[x, y] = colour;
                }
            }
        }

        public void PrintSide()
        {
            string TopBottomSpacing = "      ";
            for (int x = 0; x <= 2; x++)
            {
                Console.Write($"\n{TopBottomSpacing}");
                for (int y = 0; y <= 2; y++)
                {
                    Console.ForegroundColor = (ConsoleColor)_blocks[x, y];
                    Console.Write("██");
                }
            }
        }

        public void PrintSideSingleRow(int x)
        {
            for (int y = 0; y <= 2; y++)
            {
                Console.ForegroundColor = (ConsoleColor)_blocks[x, y];
                Console.Write("██");
            }
        }

        public void RotateSide(Direction direction)
        {
            Global.Enums.Colour[,] newArray = new Global.Enums.Colour[3, 3];

            if (direction == Direction.CW)
            {
                for (int x = 2; x >= 0; --x)
                {
                    for (int y = 0; y < 3; ++y)
                    {
                        newArray[y, 2 - x] = _blocks[x, y];
                    }
                }
            }
            else
            {
                for (int y = 2; y >= 0; --y)
                {
                    for (int x = 0; x < 3; ++x)
                    {
                        newArray[2-x, y] = _blocks[y, x];
                    }
                }
            }

            _blocks = newArray;
        }
    }
}
