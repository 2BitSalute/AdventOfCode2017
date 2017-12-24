namespace _24
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public static Dictionary <int, int> Bridges = new Dictionary <int, int>();

        public static int Recur(
            bool[] used,
            int[,] input,
            Dictionary<int, List<int>> connectorToIndex,
            int[] values,
            int prevConnectorValue,
            int total,
            int length,
            int bridgeLength)
        {
            int maxTotal = total;

            // Look at components that match the previous connector
            foreach(var component in connectorToIndex[prevConnectorValue])
            {
                if (used[component])
                {
                    continue;
                }

                // This component is not used, and it fits; let's try it:
                total += values[component];
                used[component] = true;

                if (!Bridges.ContainsKey(bridgeLength))
                {
                    Bridges.Add(bridgeLength, 0);
                }

                Bridges[bridgeLength] = Math.Max(Bridges[bridgeLength], total);

                int connector;
                if (input[component, 0] == prevConnectorValue)
                {
                    connector = 1;
                }
                else
                {
                    connector = 0;
                }

                var currTotal = Recur(used, input, connectorToIndex, values, input[component, connector], total, length, bridgeLength + 1);

                maxTotal = Math.Max(currTotal, maxTotal);

                used[component] = false;
                total -= values[component];
            }

            return maxTotal;
        }

        public static void Main(string[] args)
        {
            var inputStrings = File.ReadAllLines("input.txt");
            int length = inputStrings.Length;

            int[,] input = new int[length, 2];
            int[] values = new int[length];

            Dictionary<int, List<int>> connectorToIndex = new Dictionary<int, List<int>>();
            for(int i = 0; i < length; i++)
            {
                var tokens = inputStrings[i].Split('/', StringSplitOptions.RemoveEmptyEntries);
                input[i, 0] = int.Parse(tokens[0]);
                input[i, 1] = int.Parse(tokens[1]);

                connectorToIndex.Add(input[i, 0], i);
                connectorToIndex.Add(input[i, 1], i);

                values[i] = input[i, 0] + input[i, 1];
            }

            bool[] used = new bool[length];

            int maxTotal = Recur(
                used,
                input,
                connectorToIndex,
                values,
                prevConnectorValue: 0,
                total: 0,
                length: length,
                bridgeLength: 0);

            int longest = 0;
            int strongestLongest = 0;
            foreach(var bridge in Bridges)
            {
                if (longest < bridge.Key)
                {
                    longest = bridge.Key;
                    strongestLongest = bridge.Value;
                }
            }

            Console.WriteLine("Max total {0}, strongest longest {1}", maxTotal, strongestLongest);
        }

        public static void Add(this Dictionary<int, List<int>> valueToIndex, int value, int index)
        {
            if (!valueToIndex.ContainsKey(value))
            {
                valueToIndex.Add(value, new List<int>());
            }

            valueToIndex[value].Add(index);
        }
    }
}