using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 106, 104, 10, 5, 117, 174, 95, 61, 74, 145, 77, 95, 72, 59, 114, 95, 61, 116, 106, 66, 75, 85, 104, 62, 76, 87, 70, 17, 141, 39, 199, 91, 37, 139, 88, 84, 15, 166, 118, 54, 42, 123, 53, 183, 95, 101, 112, 26, 41, 135, 70, 48, 59, 69, 109, 93, 110, 153, 178, 117, 5 };

        // 1a. Select numbers greater than 80
        var querySyntax1a = from num in numbers where num > 80 select num;
        var methodSyntax1a = numbers.Where(num => num > 80);

        // 1b. Order numbers descending
        var querySyntax1b = from num in numbers orderby num descending select num;
        var methodSyntax1b = numbers.OrderByDescending(num => num);

        // 1c. Transform numbers into string "Have number #n"
        var querySyntax1c = from num in numbers select $"Have number {num}";
        var methodSyntax1c = numbers.Select(num => $"Have number {num}");

        // 1d. Count numbers smaller than 100 and greater than 70
        var querySyntax1d = from num in numbers where num > 70 && num < 100 select num;
        int count1d = querySyntax1d.Count();

        City[] cities = {
            new City("Toronto", 100200),
            new City("Hamilton", 80923),
            new City("Ancaster", 4039),
            new City("Brantford", 500890),
        };

        Person[] persons = {
            new Person("Cedric", "Coltrane", "Toronto", 157, null),
            new Person("Hank", "Spencer", "Peterborough", 158, "Sulfa, Penicillin"),
            new Person("Amy", "Leela", "Hamilton", 172, "Penicillin"),
            new Person("Tom", "Halliwell", "Hamilton", 179, "Codeine, Sulfa"),
            new Person("Jon", "Doggett", "Hamilton", 194, "Peanut Oil"),
        };

        // 2a. Select persons with particular height
        Func<int, IEnumerable<Person>> selectByHeight = height => persons.Where(p => p.Height == height);

        // 2b. Transform name into "J. Doe" format
        var querySyntax2b = from p in persons select $"{p.FirstName[0]}. {p.LastName}";
        var methodSyntax2b = persons.Select(p => $"{p.FirstName[0]}. {p.LastName}");

        // 2c. Select distinct allergies
        var allergies = persons.Where(p => p.Allergies != null)
                               .SelectMany(p => p.Allergies.Split(',').Select(a => a.Trim()))
                               .Distinct();

        // 2d. Select number of cities that start with 'H'
        int cityCount = cities.Count(c => c.Name.StartsWith("H"));

        // 2e. Join persons and cities, select persons from cities with population > 100,000
        var filteredPersons = from p in persons
                              join c in cities on p.City equals c.Name
                              where c.Population > 100000
                              select p;

        // 2f. Filter persons by predefined city list
        List<string> specificCities = new List<string> { "Toronto", "Hamilton", "Brantford" };
        var inCities = persons.Where(p => specificCities.Contains(p.City));
        var notInCities = persons.Where(p => !specificCities.Contains(p.City));

        // 3. Convert persons list to XML
        XElement personsXml = new XElement("Persons",
            from p in persons
            select new XElement("Person",
                new XElement("FirstName", p.FirstName),
                new XElement("LastName", p.LastName),
                new XElement("City", p.City),
                new XElement("Height", p.Height),
                new XElement("Allergies", p.Allergies ?? "None")
            )
        );
        Console.WriteLine(personsXml);
    }
}

public class City
{
    public string Name { get; set; }
    public int Population { get; set; }
    public City(string name, int population)
    {
        Name = name;
        Population = population;
    }
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public int Height { get; set; }
    public string Allergies { get; set; }

    public Person(string firstName, string lastName, string city, int height, string allergies)
    {
        FirstName = firstName;
        LastName = lastName;
        City = city;
        Height = height;
        Allergies = allergies;
    }
}
