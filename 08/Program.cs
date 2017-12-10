namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            //var input = File.ReadAllLines("smallInput.txt");

            char[] separators = new char[] { ' ' };

            Dictionary<string, int> registers = new Dictionary<string, int>();

            int greatestEver = int.MinValue;

            foreach(var line in input)
            {
                var tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string register = tokens[0];
                string op = tokens[1];
                int value = int.Parse(tokens[2]);
                string conditionRegister = tokens[4];
                string condition = tokens[5];
                int conditionValue = int.Parse(tokens[6]);

                if (!registers.ContainsKey(register))
                {
                    registers.Add(register, 0);
                }

                if (!registers.ContainsKey(conditionRegister))
                {
                    registers.Add(conditionRegister, 0);
                }

                bool doOp = false;
                int currValue = registers[conditionRegister];
                switch(condition)
                {
                    case "==":
                        doOp = currValue == conditionValue;
                        break;
                    case "!=":
                        doOp = currValue != conditionValue;
                        break;
                    case ">=":
                        doOp = currValue >= conditionValue;
                        break;
                    case "<=":
                        doOp = currValue <= conditionValue;
                        break;
                    case ">":
                        doOp = currValue > conditionValue;
                        break;
                    case "<":
                        doOp = currValue < conditionValue;
                        break;
                    default:
                        throw new Exception(condition);
                }

                Console.WriteLine(
                    "register {0} op {1} value {2}, condition {3} {4} {5}",
                    register,
                    op,
                    value,
                    conditionRegister,
                    condition,
                    conditionValue);

                if (doOp)
                {
                    if (op == "inc")
                    {
                        registers[register] += value;
                        Console.WriteLine("Incremented {0} by {1}, new value is {2}", register, value, registers[register]);
                    }
                    else if (op == "dec")
                    {
                        registers[register] -= value;
                        Console.WriteLine("Decremented {0} by {1}, new value is {2}", register, value, registers[register]);
                    }
                    else
                    {
                        throw new Exception(op);
                    }

                    if (greatestEver < registers[register])
                    {
                        greatestEver = registers[register];
                    }
                }
                else
                {
                    Console.WriteLine("Noop");
                }
            }
            
            int largestValue = int.MinValue;
            foreach(var value in registers.Values)
            {
                if (value > largestValue)
                {
                    largestValue = value;
                }
            }

            Console.WriteLine("Largest value is {0}", largestValue);
            Console.WriteLine("Largest ever is {0}", greatestEver);
        }
    }
}
