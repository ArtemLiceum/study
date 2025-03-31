using System;

namespace var2
{
    public class MyString
    {
        protected const int SZ = 80; // Размер буфера
        protected char[] str;

        public MyString()
        {
            str = new char[SZ];
            str[0] = '\x0'; // Инициализация пустой строки
        }

        public MyString(string s)
        {
            str = new char[SZ];
            int len = Math.Min(s.Length, SZ - 1); // Ограничиваем длину
            for (int i = 0; i < len; i++)
            {
                str[i] = s[i];
            }
            str[len] = '\x0'; // Завершающий нулевой символ
        }

        public void Display()
        {
            string s = "";
            for (int i = 0; str[i] != '\x0'; i++)
            {
                s += str[i];
            }
            Console.WriteLine(s);
        }
    }

    public class Pstring : MyString
    {
        public Pstring(string s) : base()
        {
            int len = Math.Min(s.Length, SZ - 1); // Проверка на длину строки
            for (int i = 0; i < len; i++)
            {
                str[i] = s[i];
            }
            str[len] = '\x0'; // Завершающий нулевой символ
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Пример использования классов MyString и Pstring
            MyString shortStr = new MyString("Короткая строка");
            Pstring longStr = new Pstring("Эта строка имеет очень большую длину, и мы можем быть уверены, что она не поместится в отведённый буфер, что приведёт к непредсказуемым последствиям");

            Console.Write("Объект MyString: ");
            shortStr.Display();

            Console.Write("Объект Pstring: ");
            longStr.Display();
        }
    }
}
