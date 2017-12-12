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
            queue.Enqueue("0");
            visited.Add("0");

            int numGroups = 1;
            while (visited.Count < graph.Keys.Count)
            {
                while(queue.Count > 0)
                {
                    string curr = queue.Dequeue();

                    foreach(string child in graph[curr])
                    {
                        if (!visited.Contains(child))
                        {
                            Console.WriteLine("Enqueuing child {0}", child);
                            visited.Add(child);
                            queue.Enqueue(child);
                        }
                    }
                }

                foreach(var key in graph.Keys)
                {
                    if (!visited.Contains(key))
                    {
                        numGroups++;
                        Console.WriteLine("Enqueuing unseen {0}", key);
                        visited.Add(key);
                        queue.Enqueue(key);
                        break;
                    }
                }
            }

            //Console.WriteLine("Total {0} in group 0", visited.Count);
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
