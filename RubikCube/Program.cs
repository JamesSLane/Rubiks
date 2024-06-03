using RubikCube.Global;
using RubikCube.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RubikCube
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool keepGoing = true, solvedState = true; ;
            Cube cube = new Cube();

            cube.PrintNetToConsole();

            while (keepGoing)
            {
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\n[Esc] Exit Application\n[Space] Reset Cube\n\n[Enter] Perfom assignment steps F/R'/U/B'/L/D'- Can only be run from a solved state\n\n[F] Front\n[R] Right\n[U] Up\n[B] Back\n[L] Left\n[D] Down");
                Console.WriteLine("\nHolding shift and one of the keys above performs the operation in a counter clockwise direction.\nWithout shift the operation is in a clockwise direction.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Enums.Direction direction = keyInfo.Modifiers== ConsoleModifiers.Shift? Enums.Direction.CCW : Enums.Direction.CW;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.F:
                        cube.PerformRotate(Global.Enums.Side.Front, direction);
                        break;
                    case ConsoleKey.R:
                        cube.PerformRotate(Global.Enums.Side.Right, direction);
                        break;
                    case ConsoleKey.U:
                        cube.PerformRotate(Global.Enums.Side.Top, direction);
                        break;
                    case ConsoleKey.B:
                        cube.PerformRotate(Global.Enums.Side.Back, direction);
                        break;
                    case ConsoleKey.L:
                        cube.PerformRotate(Global.Enums.Side.Left, direction);
                        break;
                    case ConsoleKey.D:
                        cube.PerformRotate(Global.Enums.Side.Bottom, direction);
                        break;
                    case ConsoleKey.Enter:
                        if (solvedState)
                        {
                            cube.PerformRotate(Global.Enums.Side.Front, Enums.Direction.CW);
                            cube.PrintNetToConsole();
                            Thread.Sleep(500);
                            cube.PerformRotate(Global.Enums.Side.Right, Enums.Direction.CCW);
                            cube.PrintNetToConsole();
                            Thread.Sleep(500);
                            cube.PerformRotate(Global.Enums.Side.Top, Enums.Direction.CW);
                            cube.PrintNetToConsole();
                            Thread.Sleep(500);
                            cube.PerformRotate(Global.Enums.Side.Back, Enums.Direction.CCW);
                            cube.PrintNetToConsole();
                            Thread.Sleep(500);
                            cube.PerformRotate(Global.Enums.Side.Left, Enums.Direction.CW);
                            cube.PrintNetToConsole();
                            Thread.Sleep(500);
                            cube.PerformRotate(Global.Enums.Side.Bottom, Enums.Direction.CCW);
                            Thread.Sleep(500);
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        cube = new Cube();
                        solvedState = true;
                        break;
                    case ConsoleKey.Escape:
                        keepGoing = false;
                        break;
                }

                if(keyInfo.Key==ConsoleKey.F ||
                    keyInfo.Key == ConsoleKey.R ||
                    keyInfo.Key == ConsoleKey.U ||
                    keyInfo.Key == ConsoleKey.B ||
                    keyInfo.Key == ConsoleKey.L ||
                    keyInfo.Key == ConsoleKey.D ||
                    keyInfo.Key == ConsoleKey.Enter)
                {
                    solvedState = false;
                }
                
                cube.PrintNetToConsole();
            }
        }
    }
}
