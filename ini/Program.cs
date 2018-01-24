using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ini
{
  class Program
  {
    public static void Main(string[] args)
    {
        string filename = @".\sample.ini";
      /*sample.ini:
       * [foods]
       * pizza=margherita
       * bread=full grain
       * 
       * [drinks]
       * cola=zero
       * 7up=diet
       */


      Console.WriteLine("\r\n" + "Categories:");                    
      //should say "food\r\ndrinks"
      string[] categories = Handler.GetCategories(filename);
      foreach (string category in categories)
        Console.WriteLine(">" + category + "<");

      Console.WriteLine("\r\n" + "Keys (In Category \"foods\"):");  
      //should say "pizza\r\nbread"
      string[] keys = Handler.GetKeys(filename, "foods");
      foreach (string key in keys)
        Console.WriteLine(">" + key + "<");

      Console.WriteLine("\r\n" + "Value (In Category \"foods\", Key \"pizza\" - default if not found \"vegan\"):");  
      //should say "margherita"
      string value = Handler.GetValue(filename, "foods", "pizza", "vegan");
      Console.WriteLine(">" + value + "<");

      //-------------------------------------------------------------------
      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
    }
  }
}