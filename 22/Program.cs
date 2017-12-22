using System;
using System.Collections.Generic;
using System.IO;

namespace _22
{
        public class Coordinate : IEquatable<Coordinate>
        {
            public long X {get;set;}
            public long Y {get;set;}

            public void Add(Coordinate other)
            {
                this.X += other.X;
                this.Y += other.Y;
            }

            public override string ToString()
            {
                return string.Format("X={0}, Y={1}", this.X, this.Y);
            }

            public bool Equals(Coordinate other)
            {
                return this.X == other.X && this.Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                var other = obj as Coordinate;
                return this.X == other.X && this.Y == other.Y;
            }

            public override int GetHashCode()
            {
                long result = this.X;
                result = (result * 397) ^ this.Y;
                return (int)result;
            }
        }

    public static class Program
    {
        const int S = 0;
        const int E = 1;
        const int N = 2;
        const int W = 3;

        const int Clean = 0;
        const int Weakened = 1;
        const int Infected = 2;
        const int Flagged = 3;

        private static Coordinate MoveSouth = new Coordinate { X = 0, Y = -1 };
        private static Coordinate MoveNorth = new Coordinate { X = 0, Y = 1 };
        private static Coordinate MoveEast = new Coordinate { X = 1, Y = 0 };
        private static Coordinate MoveWest = new Coordinate { X = -1, Y = 0 };

        const int Left = -1;
        const int Right = 1;

        static void Main(string[] args)
        {
            foreach(var fileName in new [] { "input.txt", "smallInput.txt" })
            {
                RunSolution(Part1, "Part 1", fileName);
                RunSolution(Part2, "Part 2 V1", fileName);
                RunSolution(Part2V2, "Part 2 V2", fileName);
            }
        }

        public static void RunSolution(Func<HashSet<Coordinate>, Coordinate, int> solve, string name, string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            Coordinate center = new Coordinate { X = lines[0].Length / 2, Y = lines.Length / 2 };

            var infected = GetInitialState(lines);

            var start = DateTime.Now;
            int answer = solve(infected, center);
            Console.WriteLine("{1} took {0} to find the answer {3} on {2}", DateTime.Now - start, name, fileName, answer);
        }

        public static HashSet<Coordinate> GetInitialState(string[] lines)
        {
            HashSet<Coordinate> infected = new HashSet<Coordinate>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        infected.Add(new Coordinate { X = j, Y = i});
                    }
                }
            }

            return infected;
        }

        public static int Part2V2(HashSet<Coordinate> infected, Coordinate curr)
        {
            Dictionary<Coordinate, int> states = new Dictionary<Coordinate, int>();
            foreach(var i in infected)
            {
                states.Add(i, Infected);
            }

            int dir = S;
            int infections = 0;
            for (int i = 0; i < 10000000; i++)
            {
                if (!states.ContainsKey(curr))
                {
                    states.Add(curr, Clean);
                }

                int state = states[curr];
                switch(state)
                {
                    case Clean:
                        dir = dir.Turn(Left);
                        break;
                    case Flagged:
                        dir = dir.Turn(Right);
                        dir = dir.Turn(Right);
                        break;
                    case Infected:
                        dir = dir.Turn(Right);
                        break;
                    case Weakened:
                        infections++;
                        break;
                }

                states[curr] = (state + 1) % 4;
                curr = curr.Move(dir);
            }

            return infections;
        }

        public static int Part2(HashSet<Coordinate> infected, Coordinate curr)
        {
            HashSet<Coordinate> weakened = new HashSet<Coordinate>();
            HashSet<Coordinate> flagged = new HashSet<Coordinate>();

            int dir = S;
            int infections = 0;
            for (int i = 0; i < 10000000; i++)
            {
                if (flagged.Contains(curr))
                {
                    flagged.Remove(curr);
                    dir = dir.Turn(Right);
                    dir = dir.Turn(Right);
                }
                else if (infected.Contains(curr))
                {
                    dir = dir.Turn(Right);
                    infected.Remove(curr);
                    flagged.Add(new Coordinate { X = curr.X, Y = curr.Y });
                }
                else if (weakened.Contains(curr))
                {
                    weakened.Remove(curr);
                    infected.Add(new Coordinate { X = curr.X, Y = curr.Y });
                    infections++;
                }
                else // Clean
                {
                    dir = dir.Turn(Left);
                    weakened.Add(new Coordinate { X = curr.X, Y = curr.Y });
                }

                curr = curr.Move(dir);
            }

            return infections;
        }

        public static int Part1(HashSet<Coordinate> infected, Coordinate curr)
        {
            int dir = S;
            int infections = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (infected.Contains(curr))
                {
                    dir = dir.Turn(Right);
                    infected.Remove(curr);
                }
                else
                {
                    dir = dir.Turn(Left);
                    infected.Add(new Coordinate { X = curr.X, Y = curr.Y });
                    infections++;
                }

                curr = curr.Move(dir);
            }

            return infections;
        }

        public static Coordinate Move(this Coordinate c, int dir)
        {
            Coordinate curr = new Coordinate { X = c.X, Y = c.Y };

            switch(dir)
            {
                case S:
                    curr.Add(MoveSouth);
                    break;
                case N:
                    curr.Add(MoveNorth);
                    break;
                case E:
                    curr.Add(MoveEast);
                    break;
                case W:
                    curr.Add(MoveWest);
                    break;
                default:
                    throw new Exception("Unexpected direction value " + dir);
            }

            return curr;
        }

        public static int Turn(this int currDir, int direction)
        {
            int newDir = (currDir + direction) % 4;

            if (newDir < 0)
            {
                return 4 + newDir;
            }

            return newDir;
        }
    }
}
