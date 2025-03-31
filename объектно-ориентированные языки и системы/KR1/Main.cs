using System;
using KR1;

class Program
{
    static void Main()
    {
        // Создание объекта Magazine
        Magazine magazine = new Magazine("Science Journal", Frequency.Monthly, new DateTime(2023, 10, 1), 5000);
        magazine.AddArticles(
            new Article(new Person { LastName = "Smith" }, "Quantum Physics", 4.5),
            new Article(new Person { LastName = "Johnson" }, "AI in Medicine", 4.8)
        );

        // Сортировка статей
        magazine.SortArticlesByTitle();
        Console.WriteLine("Sorted by Title:\n" + magazine);

        magazine.SortArticlesByAuthor();
        Console.WriteLine("Sorted by Author:\n" + magazine);

        magazine.SortArticlesByRating();
        Console.WriteLine("Sorted by Rating:\n" + magazine);

        // Создание объекта MagazineCollection
        MagazineCollection<string> collection = new MagazineCollection<string>(mg => mg.Name);
        collection.AddDefaults();
        collection.AddMagazines(magazine);

        Console.WriteLine("Magazine Collection:\n" + collection);

        // Вычисление максимального среднего рейтинга
        Console.WriteLine("Max Average Rating: " + collection.MaxAverageRating);

        // Фильтрация по периодичности
        var monthlyMagazines = collection.FrequencyGroup(Frequency.Monthly);
        Console.WriteLine("Monthly Magazines:\n" + string.Join("\n", monthlyMagazines));

        // Группировка по периодичности
        var groupedMagazines = collection.GroupByFrequency;
        foreach (var group in groupedMagazines)
        {
            Console.WriteLine($"Frequency: {group.Key}");
            Console.WriteLine(string.Join("\n", group));
        }
    }
}
