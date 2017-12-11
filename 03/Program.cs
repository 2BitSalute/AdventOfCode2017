namespace Foo
{
  using System;
  
  public class Program
  {
    public static void Main()
    {
      int input = 361527;
      //int input = 1; // 0
      //int input = 12; // 3
      //int input = 26;
      //int input = 23; // 2
      //int input = 1024; // 31
      /*
      Data from square 1 is carried 0 steps, since it's at the access port.
      Data from square 12 is carried 3 steps, such as: down, left, left.
      Data from square 23 is carried only 2 steps: up twice.
      Data from square 1024 must be carried 31 steps.
      */
      
      int dimension = (int) Math.Ceiling(Math.Sqrt(input)) + 1;
      
      
      int centerX = (dimension - 1) / 2;
      int centerY = dimension / 2;

      Console.WriteLine("{0}x{0}, center at [{1},{2}]", dimension, centerX, centerY);
      
      // build the grid
      int[,] grid = new int[dimension, dimension];
      
      grid[centerY,centerX] = 1;
      
      //Print(grid, dimension);
      Console.WriteLine();
      
      int x = centerX;
      int y = centerY;
      
      int size = 2;

      int xDirection = 1;
      int yDirection = -1;
      
      bool valueFound = false;
      int value = 0;
      
      while(size <= dimension)
      {
        for(int j = 0; j < size; j++)
        {
          value = GetValue(grid, dimension, y, x);
          
          if (value > input)
          {
            valueFound = true;
            break;
          }
          
          grid[y,x] = value;
          
          //Print(grid, dimension);
          //Console.WriteLine("value {0}", value);
          
          x += xDirection;
        }

        if (valueFound)
        {
          break;
        }

        x -= xDirection;

        for(int j = 0; j < size; j++)
        {
          value = GetValue(grid, dimension, y, x);
          
          if (value > input)
          {
            valueFound = true;
            break;
          }
          
          grid[y,x] = value;
          
          //Print(grid, dimension);
          //Console.WriteLine("value {0}", value);
          
          y += yDirection;
        }

        y -= yDirection;
        size++;

        xDirection *= -1;
        yDirection *= -1;


        //Print(grid, dimension);
        //Console.WriteLine("coordinates are {0},{1}, {2}", y, x, grid[y,x]);
      }
      
      //Print(grid, dimension);

      
      Console.WriteLine("Value is {0}", value);
    }
    
    public static int GetValue(int[,] grid, int dimension, int y, int x)
    {
      if (grid[y,x] != 0)
      {
        return grid[y,x];
      }
      
      int sum = 0;
      
      int xMin = Math.Max(0, x - 1);
      int xMax = Math.Min(dimension, x + 1);
      int yMin = Math.Max(0, y - 1);
      int yMax = Math.Min(dimension, y + 1);
      
      //Console.WriteLine("x {0}-{1}, y {2}-{3}", xMin, xMax, yMin, yMax);
      
      for(int i = Math.Max(0, x - 1); i <= Math.Min(dimension - 1, x + 1); i++)
      {
        for(int j = Math.Max(0, y - 1); j <= Math.Min(dimension - 1, y + 1); j++)
        {
          //if (i != x && y != j)
          //{
            sum += grid[j,i];
          //}
        }
      }
      
      return sum;
    }
    
    public static void Print(int[,] grid, int dimension)
    {
      for(int x = 0; x < dimension; x++)
      {
        for(int y = 0; y < dimension; y++)
        {
          Console.Write(grid[x,y] + " ");
        }
        
        Console.WriteLine();
      }
    }
    
  }
  
}