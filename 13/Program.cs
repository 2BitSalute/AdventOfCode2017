using System;
using System.Collections.Generic;
using System.IO;

namespace _13
{
    class Program
    {
        const int Depth = 0;
        const int Scanner = 1;
        const int Direction = 2;

        public static void MoveScanners(List<int[]> path, int curr)
        {
            for (int j = 0; j < path.Count; j++)
            {
                if (path[j][Depth] == 0)
                {
                    continue;
                }

                if (path[j][Direction] == 0)
                {
                    path[j][Scanner]++;
                    if (path[j][Scanner] == path[j][Depth] - 1)
                    {
                        path[j][Direction] = 1;
                    }
                }
                else
                {
                    path[j][Scanner]--;
                    if (path[j][Scanner] == 0)
                    {
                        path[j][Direction] = 0;
                    }
                }
            }

            // Print(path, curr);
        }

        public static void Print(List<int[]> path, int curr)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write("{0} ", i);

                for (int j = 0; j < path[i][Depth]; j++)
                {
                    if (path[i][Scanner] == j)
                    {
                        Console.Write(i == curr && j == 0 ? "(S) " : "[S] ");
                    }
                    else
                    {
                        Console.Write(i == curr && j == 0 ? "( ) " : "[ ] ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            char[] separators = new char[] { ' ', ':' };
            List<int[]> path = new List<int[]>();
            foreach(var line in lines)
            {
                var tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int index = int.Parse(tokens[0]);
                int depth = int.Parse(tokens[1]);

                for(int i = path.Count; i <= index; i++)
                {
                    path.Add(new int[] { 0, 0, 0 });
                }

                path[index][Depth] = depth;
            }

            /*
            int delay = -1;
            bool hasDamage;
            
            do
            {
                delay++;
                for (int i = 0; i < path.Count; i++)
                {
                    path[i][Scanner] = 0;
                    path[i][Direction] = 0;
                }

                hasDamage = HasDamage(path, delay);

                Console.WriteLine("Damage at delay {0} is {1}", delay, hasDamage);


            } while(hasDamage);

*/

            int delay = CalculateDelay(path);
            Console.WriteLine("Delay by {0}", delay);
        }

        public static int CalculateDelay(List<int[]> path)
        {
            List<int> delays = new List<int>();

            while(true)
            {
                delays.Add(0);

                int start = Math.Max(0, delays.Count - path.Count);
                for(int i = start; i < delays.Count; i++)
                {
                    if (delays[i] == -1)
                    {
                        continue;
                    }

                    if (path[delays[i]][Depth] != 0 && path[delays[i]][Scanner] == 0)
                    {
                        delays[i] = -1;
                        continue;
                    }

                    delays[i]++;

                    if (delays[i] == path.Count)
                    {
                        return i;
                    }
                }

                MoveScanners(path, -1);
            }
        }

        public static bool HasDamage(List<int[]> path, int delay)
        {
            for(int i = 0; i < delay; i++)
            {
                MoveScanners(path, -1);
            }

            for (int i = 0; i < path.Count; i++)
            {
                if (path[i][Depth] != 0 && path[i][Scanner] == 0)
                {
                    return true;
                }

                MoveScanners(path, i);
            }

            return false;
        }

        public static int CalculateDamage(List<int[]> path, int delay)
        {
            int pictosecond = 0;

            for(int i = 0; i < delay; i++)
            {
                //Console.WriteLine("Pictosecond {0}", pictosecond);
                MoveScanners(path, -1);
            }

            int damage = 0;

            for (int i = 0; i < path.Count; i++)
            {
                if (path[i][Depth] != 0 && path[i][Scanner] == 0)
                {
                    //Console.WriteLine("BEFORE: Adding damage {0} at depth {1}, i {2}, Scanner at {3}", i * path[i][Depth], path[i][Depth], i, path[i][Scanner]);
                    damage += i * path[i][Depth];
                }

                MoveScanners(path, i);
            }

            return damage;
        }
    }
}
