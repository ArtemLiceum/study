//using System;

//// Объявление делегата
//delegate void MyDelegate(double value);

//class Class1
//{
//    // Статический метод (метод_1)
//    public static void Method1(double value)
//    {
//        Console.WriteLine($"Class1.Method1 (статический): {value} + 5 = {value + 5}");
//    }

//    // Экземплярный метод (метод_2)
//    public void Method2(double value)
//    {
//        Console.WriteLine($"Class1.Method2 (экземплярный): {value} * 2 = {value * 2}");
//    }
//}

//class Class2
//{
//    // Метод в Class2 для демонстрации
//    public void Method3(double value)
//    {
//        Console.WriteLine($"Class2.Method3: {value} / 2 = {value / 2}");
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        // Создание экземпляров классов
//        Class1 obj1 = new Class1();
//        Class2 obj2 = new Class2();

//        // Создание делегата и присвоение статического метода
//        MyDelegate del = Class1.Method1;

//        // Добавление экземплярного метода (мультикаст)
//        del += obj1.Method2;

//        // Добавление метода из другого класса
//        del += obj2.Method3;

//        // Вызов делегата (многоадресная передача)
//        Console.WriteLine("=== Вызов методов через делегат ===");
//        double parameter = 10.0;
//        del(parameter);

//        // Удаление одного метода из делегата
//        del -= obj1.Method2;

//        Console.WriteLine("\n=== Вызов методов после удаления одного из них ===");
//        del(parameter);
//    }
//}

// ------------------------------ ex2 --------------------------------------------------------

using System;

// Шаг 1: Объявление делегата
delegate void MyEventHandler(double value);

// Класс с событием
class Class1
{
    // Шаг 2: Объявление события на основе делегата
    public event MyEventHandler MyEvent;

    // Метод для вызова события
    public void TriggerEvent(double value)
    {
        Console.WriteLine("\nСобытие вызывается из Class1:");
        MyEvent?.Invoke(value); // Вызов события, если есть подписчики
    }

    // Статический метод (метод_1)
    public static void Method1(double value)
    {
        Console.WriteLine($"Class1.Method1 (статический): {value} + 5 = {value + 5}");
    }

    // Экземплярный метод (метод_2)
    public void Method2(double value)
    {
        Console.WriteLine($"Class1.Method2 (экземплярный): {value} * 2 = {value * 2}");
    }
}

// Второй класс
class Class2
{
    // Метод для обработки события
    public void Method3(double value)
    {
        Console.WriteLine($"Class2.Method3: {value} / 2 = {value / 2}");
    }
}

// Тестирующий класс
class Program
{
    static void Main()
    {
        // Шаг 3: Создание экземпляров классов
        Class1 obj1 = new Class1();
        Class2 obj2 = new Class2();

        // Шаг 4: Подписка на событие
        obj1.MyEvent += Class1.Method1;  // Статический метод
        obj1.MyEvent += obj1.Method2;    // Экземплярный метод
        obj1.MyEvent += obj2.Method3;    // Метод из другого класса

        // Шаг 5: Вызов события
        Console.WriteLine("=== Вызов цепочки методов через событие ===");
        obj1.TriggerEvent(10.0);

        // Шаг 6: Удаление метода из подписки
        obj1.MyEvent -= obj1.Method2;

        Console.WriteLine("\n=== Вызов после удаления одного обработчика ===");
        obj1.TriggerEvent(10.0);
    }
}
