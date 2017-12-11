using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    class Program
    {
        public static void DoWork(string input)
        {
            bool ignoreNext = false;
            bool insideGarbage = false;
            int groups = 0;

            int totalGroupCount = 0;
            int totalScore = 0;
            int totalGarbage = 0;

            for(int i = 0; i < input.Length; i++)
            {
                if (ignoreNext)
                {
                    ignoreNext = false;
                    continue;
                }

                if (insideGarbage)
                {
                    totalGarbage++;
                }

                switch(input[i])
                {
                    case '{':
                        if (!insideGarbage)
                        {
                            groups++;
                        }

                        break;
                    case '}':
                        if (!insideGarbage && groups > 0)
                        {
                            totalScore += groups;
                            groups--;
                            totalGroupCount++;
                        }

                        break;
                    case '!':
                        if (insideGarbage)
                        {
                            ignoreNext = true;
                            totalGarbage--;
                        }
                        break;
                    case '<':
                        // doesn't matter if already inside garbage
                        insideGarbage = true;

                        break;
                    case '>':
                        if (insideGarbage)
                        {
                            totalGarbage--;
                            insideGarbage = false;
                        }

                        break;
                }
            }

            Console.WriteLine("Input '{3}' has {2} garbage, {0} groups, {1} score", totalGroupCount, totalScore, totalGarbage, input);
        }

        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            //string input = "{{<a!>},{<a!>},{<a!>},{<ab>}}";

            string[] inputs = new string[]
            {
                "<>", // 0 characters.
                "<random characters>", // , 17 characters.
                "<<<<>", // , 3 characters.
                "<{!>}>", // , 2 characters.
                "<!!>", // , 0 characters.
                "<!!!>>", // , 0 characters.
                "<{o\"i!a,<{i<a>", // , 10 characters.
                input
            };

            foreach(var i in inputs)
            {
                DoWork(i);
            }
        }
    }
}
