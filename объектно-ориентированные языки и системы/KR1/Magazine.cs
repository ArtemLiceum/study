using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1
{
    public class Magazine
    {
        private string name;
        private Frequency frequency;
        private DateTime releaseDate;
        private int circulation;
        private List<Article> articles;
        private List<Person> editors;

        // Конструктор с параметрами
        public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation)
        {
            this.name = name;
            this.frequency = frequency;
            this.releaseDate = releaseDate;
            this.circulation = circulation;
            articles = new List<Article>();
            editors = new List<Person>();
        }

        // Конструктор по умолчанию
        public Magazine() : this("Unknown", Frequency.Monthly, DateTime.Now, 1000) { }

        // Свойства для доступа к полям
        public string Name
        {
            get => name;
            set => name = value;
        }

        public Frequency Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set => releaseDate = value;
        }

        public int Circulation
        {
            get => circulation;
            set => circulation = value;
        }

        public List<Article> Articles
        {
            get => articles;
            set => articles = value;
        }

        public List<Person> Editors
        {
            get => editors;
            set => editors = value;
        }

        // Свойство для вычисления среднего рейтинга статей
        public double AverageRating
        {
            get => articles.Count > 0 ? articles.Average(a => a.Rating) : 0.0;
        }

        // Индексатор для проверки периодичности
        public bool this[Frequency freq]
        {
            get => frequency == freq;
        }

        // Метод для добавления статей
        public void AddArticles(params Article[] newArticles)
        {
            articles.AddRange(newArticles);
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Name: {name}, Frequency: {frequency}, Release Date: {releaseDate.ToShortDateString()}, " +
                   $"Circulation: {circulation}, Articles: {articles.Count}, Editors: {editors.Count}";
        }

        // Метод для краткого вывода информации
        public string ToShortString()
        {
            return $"Name: {name}, Frequency: {frequency}, Release Date: {releaseDate.ToShortDateString()}, " +
                   $"Circulation: {circulation}, Average Rating: {AverageRating}, Editors: {editors.Count}";
        }

        // Методы для сортировки статей
        public void SortArticlesByTitle()
        {
            articles.Sort();
        }

        public void SortArticlesByAuthor()
        {
            articles.Sort(new Article());
        }

        public void SortArticlesByRating()
        {
            articles.Sort(new ArticleRatingComparer());
        }
    }
}
