using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1
{

    public delegate TKey KeySelector<TKey>(Magazine mg);

    public class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> magazines;
        private KeySelector<TKey> keySelector;

        // Конструктор
        public MagazineCollection(KeySelector<TKey> selector)
        {
            keySelector = selector;
            magazines = new Dictionary<TKey, Magazine>();
        }

        // Метод для добавления элементов по умолчанию
        public void AddDefaults()
        {
            Magazine magazine = new Magazine();
            TKey key = keySelector(magazine);
            magazines.Add(key, magazine);
        }

        // Метод для добавления журналов
        public void AddMagazines(params Magazine[] newMagazines)
        {
            foreach (var magazine in newMagazines)
            {
                TKey key = keySelector(magazine);
                magazines.Add(key, magazine);
            }
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return string.Join("\n", magazines.Values);
        }

        // Метод для краткого вывода информации
        public string ToShortString()
        {
            return string.Join("\n", magazines.Values.Select(m => m.ToShortString()));
        }

        // Свойство для максимального среднего рейтинга
        public double MaxAverageRating
        {
            get => magazines.Values.Any() ? magazines.Values.Max(m => m.AverageRating) : 0.0;
        }

        // Метод для фильтрации по периодичности
        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return magazines.Where(m => m.Value.Frequency == value);
        }

        // Свойство для группировки по периодичности
        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupByFrequency
        {
            get => magazines.GroupBy(m => m.Value.Frequency);
        }
    }
}
