using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace iniRun
{
  class Program
  {
    static void Main(string[] args)
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

      //should say "food\r\ndrinks"
      string[] categories = IniHandler.GetCategories(filename);
      foreach (string category in categories)
        Console.WriteLine(category);


      //should say "pizza\r\nbread"
      string[] keys = IniHandler.GetKeys(filename, "foods");
      foreach (string key in keys)
        Console.WriteLine(key);

      //should say "margherita"
      string value = IniHandler.GetValue(filename, "foods", "pizza", "vegan");
      Console.WriteLine(value);


    }
  }
}
