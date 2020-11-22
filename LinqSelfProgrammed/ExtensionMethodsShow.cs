using System;
using System.Collections.Generic;

namespace LinqSelfProgrammed
{
  public static class ExtensionMethodsShow
  {
    private const int HEADER_LENGTH = 90;
    public static void Show<T>(this IEnumerable<T> list, string header)
    {
      //Console.WriteLine(new StackTrace().GetFrame(1).GetMethod().Name.PadLeft(80, '-'));
      header = " " + header;
      Console.WriteLine(header.PadLeft(HEADER_LENGTH, '-'));
      foreach (var s in list)
      {
        Console.WriteLine(s.ToString());
      }
      Console.WriteLine("".PadLeft(HEADER_LENGTH, '-'));
      Console.WriteLine();
    }

    public static void ShowSingle<T>(this T obj, string header)
    {
      //Console.WriteLine(new StackTrace().GetFrame(1).GetMethod().Name.PadLeft(80, '-'));
      header = " " + header;
      Console.WriteLine(header.PadLeft(HEADER_LENGTH, '-'));
      Console.WriteLine(obj.ToString());
      Console.WriteLine("".PadLeft(HEADER_LENGTH, '-'));
      Console.WriteLine();
    }
  }
}
