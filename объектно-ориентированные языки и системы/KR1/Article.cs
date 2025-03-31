using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1
{
    public class Article : IComparable<Article>, IComparer<Article>
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        // Конструктор с параметрами
        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        // Конструктор по умолчанию
        public Article() : this(new Person(), "Untitled", 0.0) { }

        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Author: {Author}, Title: {Title}, Rating: {Rating}";
        }

        // Реализация IComparable<Article> для сравнения по названию статьи
        public int CompareTo(Article other)
        {
            return Title.CompareTo(other.Title);
        }

        // Реализация IComparer<Article> для сравнения по фамилии автора
        public int Compare(Article x, Article y)
        {
            return x.Author.LastName.CompareTo(y.Author.LastName);
        }
    }

    // Вспомогательный класс для сравнения по рейтингу
    public class ArticleRatingComparer : IComparer<Article>
    {
        public int Compare(Article x, Article y)
        {
            return x.Rating.CompareTo(y.Rating);
        }
    }

}
