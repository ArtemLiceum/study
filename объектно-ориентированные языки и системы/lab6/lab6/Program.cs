using System;

interface IA
{
    void F0(out double параметр);
    int F1(double параметр);
}

interface IB
{
    void F0(out double параметр);
    void F1();
}

class Class1 : IA, IB
{
    private double поле;

    // Неявная реализация методов интерфейсов
    public void F0(out double параметр)
    {
        параметр = 15;  // Присваивание значения полю
        this.поле = параметр;
        Console.WriteLine("Class1: IA.IB F0 called. поле = " + поле);
    }

    public int F1(double параметр)
    {
        Console.WriteLine($"Class1: IA F1 called with параметр = {параметр}");
        return (int)параметр + 10;
    }

    public void F1()
    {
        Console.WriteLine("Class1: IB F1 called");
    }
}

class Class2 : IA, IB
{
    private double поле;

    // Неявная реализация
    public void F0(out double параметр)
    {
        параметр = 20;
        this.поле = параметр;
        Console.WriteLine("Class2: IA.IB F0 called. поле = " + поле);
    }

    public int F1(double параметр)
    {
        Console.WriteLine($"Class2: IA F1 called with параметр = {параметр}");
        return (int)параметр * 2;
    }

    public void F1()
    {
        Console.WriteLine("Class2: IB F1 called");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Неявная реализация интерфейсов ===");

        // Неявная реализация
        Class1 obj1 = new Class1();
        Class2 obj2 = new Class2();

        // Вызов методов с интерфейсными ссылками
        IA ia1 = obj1;
        IA ia2 = obj2;

        IB ib1 = obj1;
        IB ib2 = obj2;

        double w;

        // Вызов F0
        ia1.F0(out w);
        Console.WriteLine($"IA F0 result: {w}");

        ib1.F0(out w);
        Console.WriteLine($"IB F0 result: {w}");

        // Вызов F1 с приведением к интерфейсу
        Console.WriteLine("IA F1 result: " + ia1.F1(5.5));
        ib1.F1();

        Console.WriteLine("=== Явная реализация интерфейсов ===");

        // Явная реализация для методов с одинаковой сигнатурой
        Class1 objExplicit = new Class1();
        IA iaExplicit = objExplicit;
        IB ibExplicit = objExplicit;

        double result;

        // Явный вызов методов
        iaExplicit.F0(out result);
        Console.WriteLine("Явная IA F0: " + result);
        ibExplicit.F0(out result);
        Console.WriteLine("Явная IB F0: " + result);
    }
}

// ------------------------------------------------ ex2 -------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

class Student : IComparable<Student>
{
    public string FullName { get; set; }
    public int Course { get; set; }
    public string Group { get; set; }
    public DateTime Date { get; set; }
    public double RunResult { get; set; } // Результат забега в секундах

    public Student(string fullName, int course, string group, DateTime date, double runResult)
    {
        FullName = fullName;
        Course = course;
        Group = group;
        Date = date;
        RunResult = runResult;
    }

    // Реализация интерфейса IComparable для сравнения по RunResult
    public int CompareTo(Student other)
    {
        return this.RunResult.CompareTo(other.RunResult); // Сравнение по результату забега
    }

    public override string ToString()
    {
        return $"{FullName}, Курс: {Course}, Группа: {Group}, Дата: {Date.ToShortDateString()}, Результат: {RunResult}s";
    }
}

class StudentComparer : IComparer
{
    // Реализация интерфейса IComparer для сортировки по результату
    public int Compare(object x, object y)
    {
        Student s1 = x as Student;
        Student s2 = y as Student;

        if (s1 == null || s2 == null)
            throw new ArgumentException("Objects are not of type Student");

        return s1.RunResult.CompareTo(s2.RunResult);
    }
}

class Program
{
    static void Main()
    {
        // Создание коллекции студентов
        ArrayList students = new ArrayList
        {
            new Student("Иванов Иван", 2, "Группа А", new DateTime(2024, 5, 1), 12.5),
            new Student("Петров Петр", 1, "Группа Б", new DateTime(2024, 5, 2), 11.3),
            new Student("Сидоров Сидор", 3, "Группа В", new DateTime(2024, 5, 3), 13.1),
            new Student("Кузнецов Николай", 2, "Группа А", new DateTime(2024, 5, 4), 11.3),
            new Student("Смирнова Анна", 1, "Группа Б", new DateTime(2024, 5, 5), 10.9),
            new Student("Васильева Мария", 3, "Группа В", new DateTime(2024, 5, 6), 10.9),
            new Student("Захаров Алексей", 2, "Группа Г", new DateTime(2024, 5, 7), 12.0)
        };

        Console.WriteLine("--- Исходный список студентов ---");
        foreach (Student s in students)
            Console.WriteLine(s);

        // Сортировка по результату забега
        students.Sort(new StudentComparer());

        Console.WriteLine("\n--- Отсортированный список студентов по результатам забега ---");
        foreach (Student s in students)
            Console.WriteLine(s);

        // Определение трёх лучших результатов
        ArrayList winners = new ArrayList();
        double bestResult = ((Student)students[0]).RunResult;
        winners.Add(students[0]);

        // Добавление студентов с одинаковыми результатами в список победителей
        for (int i = 1; i < students.Count; i++)
        {
            Student currentStudent = (Student)students[i];
            if (winners.Count < 3 || currentStudent.RunResult == bestResult)
            {
                winners.Add(currentStudent);
                bestResult = currentStudent.RunResult;
            }
            else
            {
                break;
            }
        }

        Console.WriteLine("\n--- Список победителей ---");
        foreach (Student s in winners)
            Console.WriteLine(s);
    }
}
