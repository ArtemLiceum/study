using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1
{
    public class Edition : ICloneable
    {
        protected string name;
        protected DateTime releaseDate;
        protected int circulation;

        // Конструктор с параметрами
        public Edition(string name, DateTime releaseDate, int circulation)
        {
            this.name = name;
            this.releaseDate = releaseDate;
            this.circulation = circulation;
        }

        // Конструктор по умолчанию
        public Edition() : this("Unknown", DateTime.Now, 1000) { }

        // Свойства для доступа к полям
        public string Name
        {
            get => name;
            set => name = value;
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set => releaseDate = value;
        }

        public int Circulation
        {
            get => circulation;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Circulation must be non-negative.");
                circulation = value;
            }
        }

        // Реализация метода Clone из интерфейса ICloneable
        public virtual object Clone()
        {
            return new Edition(name, releaseDate, circulation);
        }

        // Переопределение метода Equals
        public override bool Equals(object obj)
        {
            if (obj is Edition other)
                return name == other.name && releaseDate == other.releaseDate && circulation == other.circulation;
            return false;
        }

        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(name, releaseDate, circulation);
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Name: {name}, Release Date: {releaseDate.ToShortDateString()}, Circulation: {circulation}";
        }
    }
}
