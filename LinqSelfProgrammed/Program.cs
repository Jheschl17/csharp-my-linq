using System;
using System.Collections.Generic;
using System.Globalization;
using MyLinqLib;

namespace LinqSelfProgrammed
{
 public static class Program
  {
    private static List<int> integers;
    private static List<double> doubles;
    private static List<string> strings;
    private static List<Person> persons;

    [STAThread]
    private static void Main()
    {
      ShowStartingLists();
      TestMyLinq();
      Console.Read();
    }

    static Program()
    {
      integers = new List<int> { 5, 76, 3, 93, 143, 5, 11, 67, 5 };
      doubles = new List<double> { 1.23, 68.256, 44.55, 96.127, 393.4567, 2.45, 4.1 };
      strings = new List<string> { "Hansi", "Pauli", "Heinzi", "Susi", "Pepi" };
      persons = new List<Person>
      {
        new Person {Firstname = "Hansi", Lastname = "Huber", Age = 66, Sallary = 1513},
        new Person {Firstname = "Heinzi", Lastname = "Prüller", Age = 77, Sallary = 3100},
        new Person {Firstname = "Susi", Lastname = "Maurer", Age = 55, Sallary = 2460},
        new Person {Firstname = "Gerti", Lastname = "Lehner", Age = 55, Sallary = 2941},
        new Person {Firstname = "Pauli", Lastname = "Huber", Age = 44, Sallary = 1471}
      };
    }

    private static void ShowStartingLists()
    {
      integers.Show("integers");
      doubles.Show("doubles");
      strings.Show("strings");
      persons.Show("persons");
    }

    private static void TestMyLinq()
    {
      TestFilters();
      TestProjection();
      TestSelects();
      TestAggregation();
      TestSorting();
      TestCombined();
    }

    private static void TestFilters()
    {
      var whereA = integers.Where(x => x > 50);
      whereA.Show("integers.Where(x => x > 50)");

      var whereB = strings.Where(x => x.Length == 4);
      whereB.Show("strings.Where(x => x.Length == 4)");

      persons.Where(x => x.Sallary > 2500).Show("persons.Where(x => x.Sallary > 2500)");

      doubles.Take(4).Show("doubles.Take(4)");
      persons.Skip(2).Show("persons.Skip(2)");
      integers.TakeWhile(x => x < 100).Show("integers.TakeWhile(x => x < 100");
      strings.SkipWhile(x => x[1] == 'a').Show("strings.SkipWhile(x => x[1] == 'a')");

      integers.Distinct().Show("integers.Distinct()");
    }

    private static void TestProjection()
    {
      doubles.Select(x => (int)x).Show("doubles.Select(x => (int)x)");
      strings.Select(x => x.ToUpper()).Show("strings.Select(x => x.ToUpper())");
      persons.Select(x => $"{x.Lastname} --> {x.Sallary}").Show("persons.Select(x => $\"{x.Lastname} --> {x.Sallary}\")");
      persons.Select(x => new { Name = $"{x.Firstname} {x.Lastname}" }).Show("persons.Select(x => new { Name = $\"{x.Firstname} {x.Lastname}\" })");
    }

    private static void TestSelects()
    {
      doubles.First().ShowSingle("doubles.First()");
      persons.First(x => x.Age < 60).ShowSingle("persons.First(x => x.Age < 60)");
      doubles.Last().ShowSingle("doubles.Last()");
      integers.FirstOrDefault(x => x > 1000).ShowSingle("integers.FirstOrDefault(x => x > 1000)");
    }

    private static void TestAggregation()
    {
      integers.Average().ShowSingle("integers.Average()");
      doubles.Average().ShowSingle("doubles.Average()");
      persons.Average(x => x.Sallary).ShowSingle("persons.Average(x => x.Sallary)");
      persons.Count(x => x.Age > 60).ShowSingle("persons.Count(x => x.Age > 60)");
      persons.Count(x => x.Lastname.Length > 5).ShowSingle("persons.Count(x => x.Lastname.Length > 5)");
    }

    private static void TestSorting()
    {
      integers.OrderBy(x => x).Show("integers.OrderBy(x => x)");
      doubles.OrderByDescending(x => x).Show("doubles.OrderByDescending(x => x)");
      persons.OrderByDescending(x => x.Sallary).Show("persons.OrderByDescending(x => x.Sallary)");
      persons.OrderByDescending(x => x.Sallary)
        .Select(x => x.Firstname)
        .Show("persons.OrderByDescending(x => x.Sallary).Select(x=>x.Firstname)");
      strings.Revert().Show("strings.Revert()");
    }

    private static void TestCombined()
    {
      persons
        .Where(x => x.Age < 70)
        .Skip(1)
        .Take(2)
        .Select(x => $"{x.Firstname} {x.Lastname}")
        .Show("persons.Where(x=>x.Age<70).Skip(1).Take(2).Select(x => $\"{x.Firstname} {x.Lastname}\")");

      CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
      doubles
        .Select(x => $"{x}".Split('.'))
        .Where(x => x[1].Length > 2)
        .Where(x => int.Parse(x[0]) % 2 == 0)
        .Select(x => string.Join(",", x))
        .Show("doubles with more than 2 decimals and even integer-part");
    }

  }
}
