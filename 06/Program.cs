
namespace Foo
{
  using System;
  using System.Collections.Generic;
  
  public class Program
  {
    public static void Main()
    {
      HashSet<string>seenConfigurations = new HashSet<string>();
      
      int[] banks = Input;//new int[] { 0, 2, 7, 0 };
      //int[] banks = new int[] { 0, 2, 7, 0 };

      string currentConfiguration = GetConfig(banks);
      
      bool foundFirst = false;
      
      int count = 0;
      string configuration = string.Empty;
      
      while(true)
      {
        if (!foundFirst)
        {
          if (seenConfigurations.Contains(currentConfiguration))
          {
            configuration = currentConfiguration;
            foundFirst = true;
          }
        }
        else
        {
          count++;

          if (configuration == currentConfiguration)
          {
            break;
          }
        }
        
        //Console.WriteLine(currentConfiguration);
        seenConfigurations.Add(currentConfiguration);
        
        int i = GetIndexOfFullest(banks);
        int amount = banks[i];
        banks[i] = 0;
        while(amount > 0)
        {
          i++;
          
          banks[i % banks.Length]++;
          amount--;
        }
        
        currentConfiguration = GetConfig(banks);
      }
      
      Console.WriteLine(count);
    }
    
    public static string GetConfig(int[] banks)
    {
      return string.Join(separator: " ", values: banks);
    }
    
    public static int GetIndexOfFullest(int[] banks)
    {
      int largest = -1;
      int index = -1;
      for(int i = 0; i < banks.Length; i++)
      {
        if (banks[i] > largest)
        {
          largest = banks[i];
          index = i;
        }
      }
      
      return index;
    }
    
    public static int[] Input = new int[]
    {
      10, 3, 15, 10, 5, 15, 5, 15, 9, 2, 5, 8, 5, 2, 3, 6
    };
  }
}