using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Worker
    {
        public string Name = "None";
        public int Age = -1;
        public int Work_age = -1;

        // Конструктор без параметров  
        public Worker()
        {
            Console.WriteLine($"Конструктор без параметров вызван: Служащий '{Name}', возраст - '{Age}', стаж - {Work_age}.");
        }

        // Конструктор с параметрами  
        public Worker(string name, int age, int work_age)
        {
            Name = name;
            Age = age;
            Work_age = work_age;
            Console.WriteLine($"Конструктор с параметрами вызван: Служащий '{Name}', возраст - '{Age}', стаж - {Work_age}.");
        }

        // Конструктор копирования  
        public Worker(Worker existingWorker)
        {
            Name = existingWorker.Name;
            Age = existingWorker.Age;
            Work_age = existingWorker.Work_age;
            Console.WriteLine($"Конструктор копирования вызван: Служащий '{Name}' скопирован.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Служащий - {Name}, возраст - {Age}, стаж - {Work_age}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[3];

            // Создание объектов с использованием различных конструкторов  
            workers[0] = new Worker(); // Конструктор без параметров  
            workers[1] = new Worker("Иван", 25, 5); // Конструктор с параметрами  
            workers[2] = new Worker(workers[1]); // Конструктор копирования  

            Console.WriteLine("\nИнформация о служащих:");
            foreach (var worker in workers)
            {
                worker.DisplayInfo();
            }

            Console.WriteLine("\nВсе объекты были созданы и уничтожены.");
            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}
