//using System;
//using System.Collections.Generic;

//namespace HierarchyExample
//{
//    // Абстрактный класс
//    public abstract class Person
//    {
//        protected string name;
//        protected int age;

//        // Список объектов
//        private static List<Person> persons = new List<Person>();

//        public Person(string name, int age)
//        {
//            this.name = name;
//            this.age = age;
//        }

//        // Метод для добавления объекта в список
//        public void Add()
//        {
//            persons.Add(this);
//        }

//        // Абстрактный метод Show
//        public abstract void Show();

//        // Метод для просмотра списка
//        public static void ShowAll()
//        {
//            foreach (var person in persons)
//            {
//                person.Show();
//            }
//        }
//    }

//    // Класс Рабочий
//    public class Worker : Person
//    {
//        private string specialty;

//        public Worker(string name, int age, string specialty) : base(name, age)
//        {
//            this.specialty = specialty;
//        }

//        public override void Show()
//        {
//            Console.WriteLine($"Worker: {name}, Age: {age}, Specialty: {specialty}");
//        }
//    }

//    // Класс Инженер
//    public class Engineer : Person
//    {
//        private string field;

//        public Engineer(string name, int age, string field) : base(name, age)
//        {
//            this.field = field;
//        }

//        public override void Show()
//        {
//            Console.WriteLine($"Engineer: {name}, Age: {age}, Field: {field}");
//        }
//    }

//    // Класс Служащий
//    public class Employee : Person
//    {
//        private string department;

//        public Employee(string name, int age, string department) : base(name, age)
//        {
//            this.department = department;
//        }

//        public override void Show()
//        {
//            Console.WriteLine($"Employee: {name}, Age: {age}, Department: {department}");
//        }
//    }

//    // Тестирование
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Worker w1 = new Worker("Ivan", 35, "Welder");
//            Engineer e1 = new Engineer("Anna", 28, "Software");
//            Employee emp1 = new Employee("Maria", 30, "HR");

//            w1.Add();
//            e1.Add();
//            emp1.Add();

//            Console.WriteLine("List of Persons:");
//            Person.ShowAll();
//        }
//    }
//}

// ------------------------------------ Задание 2 ----------------------------------------

//using System;
//using System.Linq;

//namespace NormExample
//{
//    // Абстрактный класс с виртуальной функцией нормы
//    public abstract class Norm
//    {
//        public abstract double CalculateNorm();

//        public void DisplayNorm()
//        {
//            Console.WriteLine($"Norm: {CalculateNorm()}");
//        }
//    }

//    // Класс комплексных чисел
//    public class ComplexNumber : Norm
//    {
//        private double real;
//        private double imaginary;

//        public ComplexNumber(double real, double imaginary)
//        {
//            this.real = real;
//            this.imaginary = imaginary;
//        }

//        public override double CalculateNorm()
//        {
//            return real * real + imaginary * imaginary; // Модуль в квадрате
//        }

//        public override string ToString()
//        {
//            return $"{real} + {imaginary}i";
//        }
//    }

//    // Класс вектора из 10 элементов
//    public class Vector : Norm
//    {
//        private double[] elements;

//        public Vector(double[] elements)
//        {
//            if (elements.Length != 10)
//                throw new ArgumentException("Vector must have exactly 10 elements.");
//            this.elements = elements;
//        }

//        public override double CalculateNorm()
//        {
//            return Math.Sqrt(elements.Sum(e => Math.Abs(e))); // Корень из суммы элементов по модулю
//        }
//    }

//    // Класс матрицы 2x2
//    public class Matrix : Norm
//    {
//        private double[,] matrix;

//        public Matrix(double[,] matrix)
//        {
//            if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2)
//                throw new ArgumentException("Matrix must be 2x2.");
//            this.matrix = matrix;
//        }

//        public override double CalculateNorm()
//        {
//            return Math.Max(
//                Math.Max(Math.Abs(matrix[0, 0]), Math.Abs(matrix[0, 1])),
//                Math.Max(Math.Abs(matrix[1, 0]), Math.Abs(matrix[1, 1]))
//            );
//        }
//    }

//    // Тестирование
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            ComplexNumber complex = new ComplexNumber(3, 4);
//            Vector vector = new Vector(new double[] { 1, -2, 3, -4, 5, -6, 7, -8, 9, -10 });
//            Matrix matrix = new Matrix(new double[,] { { -3, 5 }, { 7, -2 } });

//            Console.WriteLine($"Complex Number: {complex}");
//            complex.DisplayNorm();

//            Console.WriteLine("\nVector Norm:");
//            vector.DisplayNorm();

//            Console.WriteLine("\nMatrix Norm:");
//            matrix.DisplayNorm();
//        }
//    }
//}

//------------------------------------- Задание 3 ----------------------------------------------------

using System;

namespace DataExample
{
    // Абстрактный класс "Данные"
    public abstract class Data
    {
        public abstract void Display(); // Метод для отображения данных
        public abstract void Save();    // Метод для сохранения данных
        public abstract void Process(); // Метод для обработки данных
    }

    // Данные типа "Сигнал"
    public class SignalData : Data
    {
        private double frequency; // Частота сигнала
        private double amplitude; // Амплитуда сигнала

        public SignalData(double frequency, double amplitude)
        {
            this.frequency = frequency;
            this.amplitude = amplitude;
        }

        public override void Display()
        {
            Console.WriteLine($"Данные сигнала: Частота = {frequency}, Амплитуда = {amplitude}");
        }

        public override void Save()
        {
            Console.WriteLine("Данные сигнала сохранены в файл.");
        }

        public override void Process()
        {
            Console.WriteLine($"Обработка данных сигнала: Амплитуда нормализована до {amplitude / 2}");
        }
    }

    // Данные типа "Результат обработки"
    public class ProcessedData : Data
    {
        private string algorithmName; // Название алгоритма
        private double result;        // Результат обработки

        public ProcessedData(string algorithmName, double result)
        {
            this.algorithmName = algorithmName;
            this.result = result;
        }

        public override void Display()
        {
            Console.WriteLine($"Данные обработки: Алгоритм = {algorithmName}, Результат = {result}");
        }

        public override void Save()
        {
            Console.WriteLine("Данные обработки сохранены в базу данных.");
        }

        public override void Process()
        {
            Console.WriteLine($"Дополнительная обработка: Результат увеличен до {result + 10}");
        }
    }

    // Вспомогательные данные
    public class AuxiliaryData : Data
    {
        private string description; // Описание данных

        public AuxiliaryData(string description)
        {
            this.description = description;
        }

        public override void Display()
        {
            Console.WriteLine($"Вспомогательные данные: {description}");
        }

        public override void Save()
        {
            Console.WriteLine("Вспомогательные данные сохранены во временное хранилище.");
        }

        public override void Process()
        {
            Console.WriteLine("Вспомогательные данные обработаны для отладки.");
        }
    }

    // Тестирование
    class Program
    {
        static void Main(string[] args)
        {
            Data signal = new SignalData(50.5, 10.2); // Создание объекта "Сигнал"
            Data processed = new ProcessedData("FFT", 128.4); // Создание объекта "Результат обработки"
            Data auxiliary = new AuxiliaryData("Временные параметры калибровки"); // Создание вспомогательных данных

            Console.WriteLine("Отображение всех данных:");
            signal.Display();
            processed.Display();
            auxiliary.Display();

            Console.WriteLine("\nОбработка всех данных:");
            signal.Process();
            processed.Process();
            auxiliary.Process();

            Console.WriteLine("\nСохранение всех данных:");
            signal.Save();
            processed.Save();
            auxiliary.Save();
        }
    }
}

