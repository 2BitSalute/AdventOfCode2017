using System;
using System.IO;
using System.Collections.Generic;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileInput = File.ReadAllText("input.txt");

            string[] inputs = new string[]
            {
                //"ne,ne,ne", // is 3 steps away.
                //"ne,ne", // 2
                //"ne,ne,sw,sw", // is 0 steps away (back where you started).
                //"ne,ne,s,s", // is 2 steps away (se,se).
                //"se,sw,se,sw,sw", // is 3 steps away (s,s,sw).
                fileInput
            };

            int x;
            int y;

            foreach(var input in inputs)
            {
                int steps = FindLocation(input, out x, out y);

                Console.WriteLine("Final Location is {0},{1}", x, y);

                //int steps = GoHome(x, y);

                Console.WriteLine("Furthest away is {0} steps away", steps);
            }

        }

        public static string[] directions = new string[] { "n", "s", "ne", "nw", "sw", "se" };

        public static char[] separators = new char[] { ' ', ',' };

        static int FindLocation(string input, out int x, out int y)
        {
            x = 0;
            y = 0;

            var directions = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int farthestAway = 0;

            foreach(var direction in directions)
            {
                x += GetXOffset(direction);
                y += GetYOffset(direction);


                int steps = GoHome(x, y);

                if (steps > farthestAway)
                {
                    farthestAway = steps;
                }
            }

            Console.WriteLine("Final location is {0} steps away", GoHome(x, y));

            return farthestAway;
        }

        public static int GoHome(int x, int y)
        {
            int xSteps = Math.Abs(x / 3);

            if (y < 0)
            {
                y += xSteps * 2;
            }
            else
            {
                y -= xSteps * 2;
            }

            return xSteps + Math.Abs(y / 4);
        }

        public static int GetYOffset(string dir)
        {
            if (dir == "n")
            {
                return -4;
            }

            if (dir == "s")
            {
                return 4;
            }

            if (dir == "sw" || dir == "se")
            {
                return 2;
            }

            if (dir == "ne" || dir == "nw")
            {
                return -2;
            }

            return 0;
        }

        public static int GetXOffset(string dir)
        {
            if (dir == "sw" || dir == "nw")
            {
                return 3;
            }

            if (dir == "ne" || dir == "se")
            {
                return -3;
            }

            return 0;
        }
    }
}
