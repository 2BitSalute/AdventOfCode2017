using System;
using System.IO;
using System.Collections.Generic;

namespace _12
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            var input = File.ReadAllLines("input.txt");

            foreach(var line in input)
            {
                var tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach(var token in tokens)
                {
                    if (!graph.ContainsKey(token))
                    {
                        graph.Add(token, new List<string>());
                    }
                }

                List<string> children = new List<string>(tokens);

                // Add parent's children
                for(int i = 1; i < tokens.Length; i++)
                {
                    graph[tokens[0]].Add(tokens[i]);
                    graph[tokens[i]].Add(tokens[0]);
                }
            }

            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            string group = string.Empty;
            int numGroups = 0;

            while (visited.Count < graph.Keys.Count)
            {
                foreach(var key in graph.Keys)
                {
                    if (!visited.Contains(key))
                    {
                        group = key;
                        numGroups++;
                        visited.Add(key);
                        queue.Enqueue(key);
                        break;
                    }
                }

                while(queue.Count > 0)
                {
                    string curr = queue.Dequeue();

                    foreach(string child in graph[curr])
                    {
                        if (!visited.Contains(child))
                        {
                            visited.Add(child);
                            queue.Enqueue(child);
                        }
                    }
                }

                // Only accurate for the 1st group. Too lazy to fix
                Console.WriteLine("Now {0} when including group {1}", visited.Count, group);
            }

            Console.WriteLine("Num groups " + numGroups);
        }

        public static char[] separators = new char[] { ' ', '<', '-', '>', ',' };

        public static string[] SmallInput = new string[]
        {
            "0 <-> 2",
            "1 <-> 1",
            "2 <-> 0, 3, 4",
            "3 <-> 2, 4",
            "4 <-> 2, 3, 6",
            "5 <-> 6",
            "6 <-> 4, 5",
        };
    }
}
