using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

namespace KR1
{
    public class TestCollections<TKey, TValue>
    {
        private List<TKey> keysList;
        private List<TValue> valuesList;
        private Dictionary<TKey, TValue> keyValueDictionary;
        private Dictionary<string, TValue> stringValueDictionary;
        private GenerateElement<TKey, TValue> generateElement;

        public TestCollections(int count, GenerateElement<TKey, TValue> generator)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.");

            generateElement = generator ?? throw new ArgumentNullException(nameof(generator));

            keysList = new List<TKey>();
            valuesList = new List<TValue>();
            keyValueDictionary = new Dictionary<TKey, TValue>();
            stringValueDictionary = new Dictionary<string, TValue>();

            for (int i = 0; i < count; i++)
            {
                var keyValuePair = generateElement(i);
                keysList.Add(keyValuePair.Key);
                valuesList.Add(keyValuePair.Value);
                keyValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
                stringValueDictionary.Add(keyValuePair.Key.ToString(), keyValuePair.Value);
            }
        }

        public void MeasureSearchTime()
        {
            if (keysList.Count == 0)
                return;

            // Элементы для поиска
            TKey firstKey = keysList[0];
            TKey middleKey = keysList[keysList.Count / 2];
            TKey lastKey = keysList[keysList.Count - 1];
            TKey nonExistentKey = generateElement(keysList.Count).Key; // Генерация несуществующего ключа

            // Поиск в List<TKey>
            MeasureTime(() => keysList.Contains(firstKey), "List<TKey> Contains (first)");
            MeasureTime(() => keysList.Contains(middleKey), "List<TKey> Contains (middle)");
            MeasureTime(() => keysList.Contains(lastKey), "List<TKey> Contains (last)");
            MeasureTime(() => keysList.Contains(nonExistentKey), "List<TKey> Contains (non-existent)");

            // Поиск в Dictionary<TKey, TValue> по ключу
            MeasureTime(() => keyValueDictionary.ContainsKey(firstKey), "Dictionary<TKey, TValue> ContainsKey (first)");
            MeasureTime(() => keyValueDictionary.ContainsKey(middleKey), "Dictionary<TKey, TValue> ContainsKey (middle)");
            MeasureTime(() => keyValueDictionary.ContainsKey(lastKey), "Dictionary<TKey, TValue> ContainsKey (last)");
            MeasureTime(() => keyValueDictionary.ContainsKey(nonExistentKey), "Dictionary<TKey, TValue> ContainsKey (non-existent)");

            // Поиск в Dictionary<string, TValue> по ключу
            MeasureTime(() => stringValueDictionary.ContainsKey(firstKey.ToString()), "Dictionary<string, TValue> ContainsKey (first)");
            MeasureTime(() => stringValueDictionary.ContainsKey(middleKey.ToString()), "Dictionary<string, TValue> ContainsKey (middle)");
            MeasureTime(() => stringValueDictionary.ContainsKey(lastKey.ToString()), "Dictionary<string, TValue> ContainsKey (last)");
            MeasureTime(() => stringValueDictionary.ContainsKey(nonExistentKey.ToString()), "Dictionary<string, TValue> ContainsKey (non-existent)");

            // Поиск в Dictionary<TKey, TValue> по значению
            TValue firstValue = valuesList[0];
            TValue middleValue = valuesList[valuesList.Count / 2];
            TValue lastValue = valuesList[valuesList.Count - 1];
            TValue nonExistentValue = generateElement(keysList.Count).Value; // Генерация несуществующего значения

            MeasureTime(() => keyValueDictionary.ContainsValue(firstValue), "Dictionary<TKey, TValue> ContainsValue (first)");
            MeasureTime(() => keyValueDictionary.ContainsValue(middleValue), "Dictionary<TKey, TValue> ContainsValue (middle)");
            MeasureTime(() => keyValueDictionary.ContainsValue(lastValue), "Dictionary<TKey, TValue> ContainsValue (last)");
            MeasureTime(() => keyValueDictionary.ContainsValue(nonExistentValue), "Dictionary<TKey, TValue> ContainsValue (non-existent)");
        }

        private void MeasureTime(Action action, string description)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            Console.WriteLine($"{description}: {stopwatch.ElapsedTicks} ticks");
        }
    }
}