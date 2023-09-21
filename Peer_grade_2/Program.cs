using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using EBookLib;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using System.Text.Unicode;

namespace Peer_grade_2
{
    public class Program
    {
        // Обратчик события Print
        static public void PrintHandler(PrintEdition print)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("PRINTED!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(print);
        }
        // Обработчик события TakeBooks
        public static void TakeHandler(char chr)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"ATTENTION! Books starts with {chr} were taken!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Создание случайной строки
        public static string GetRandomString()
        {
            Random random = new Random();
            int length = random.Next(1, 11);
            string str = "";
            str += (char)(random.Next(0, 26) + 65); 
            for (int i = 1; i < length; i++)
            {
                str += (char)(random.Next(0, 26) + 97);
            }
            return str;
        }

        // Создать случайный экземпляр класса MyLibrary
        public static void GenerateLibrary(MyLibrary<PrintEdition> myLibrary, int n)
        {
            Random random = new Random();
            int numberOfPrintEdition = random.Next(1, 11);
            for (int i = 0; i < n; i++)
            {
                int typeOfEdition = random.Next(0, 2);
                bool procced = true;
                while (procced)
                {
                    try
                    {
                        if (typeOfEdition == 0)
                        {
                            myLibrary.Add(new Magazine(GetRandomString(), random.Next(-10, 101), random.Next(-10, 101)));
                        }
                        else
                        {
                            myLibrary.Add(new Book(GetRandomString(), random.Next(-10, 101), GetRandomString()));
                        }
                        procced = false;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        // Спросить пользователя, хочет ли он выполнить программу еще раз
        public static void AskToContinue(ref bool procced)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Сформироввать myLibrary еще раз? y/n");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            while (input != "y" && input != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неккоректный ввод");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Сформироввать myLibrary еще раз? y/n");
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine();
            }
            if (input == "y")
            {
                procced = true;
                Console.Clear();
            }
            else
            {
                procced = false;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Создание экземпляра класса MyLibrary из текста, полученного из файла
        public static void GenerateLibraryFromText(ref MyLibrary<PrintEdition> myLibrary, string text)
        {
            text = text.Replace(";", "");
            text = text.Replace(".", "");
            string[] strings = Array.ConvertAll(text.Split('\n'), e => e);
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Length > 0)
                {
                    try
                    {
                        string[] str = Array.ConvertAll(strings[i].Split(' ', '='), e => e);
                        if (str[4] == "period")
                        {
                            myLibrary.Add(new Magazine(str[1], int.Parse(str[3]), int.Parse(str[5])));
                        }
                        else
                        {
                            myLibrary.Add(new Book(str[1], int.Parse(str[3]), str[5]));
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                
            }
        }

        static void Main(string[] args)
        {
            bool procced = true;
            string path = @"..\..\..\..\myLibrary.txt";
            while (procced)
            {
                try
                {
                    string textFromFile = "";
                    Console.InputEncoding = System.Text.Encoding.UTF8;
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    IPrinting.onPrint = PrintHandler;
                    MyLibrary<PrintEdition>.onTake = TakeHandler;
                    MyLibrary<PrintEdition> myLibrary = new();
                    MyLibrary<PrintEdition> myLibraryFromFile = new();

                    // Запрос у пользователя числа печатных изданий
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Введите количество печатных изданий в библиотеке:");
                    Console.ForegroundColor = ConsoleColor.White;
                    string N = Console.ReadLine();
                    while (!int.TryParse(N, out int number))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неккоректный ввод, введите целое число:");
                        Console.ForegroundColor = ConsoleColor.White;
                        N = Console.ReadLine();
                    }
                    // Создание библиотеки
                    GenerateLibrary(myLibrary, int.Parse(N));

                    // Вывзов метода Print() для всех книг (Book)
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вызываем Print() для всех книг (Book)");
                    Console.ForegroundColor = ConsoleColor.White;
                    myLibrary.CallPrintForBooks();

                    // Вывод содержания myLibrary
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Выводим содержимое myLibrary на экран:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(myLibrary);

                    // Удалить книги, начинающиеся на случайную букву
                    Random random = new Random();
                    myLibrary.TakeBooks((char)(random.Next(0, 26) + 65));
                    Console.WriteLine(myLibrary);

                    // Запись в файл информации о myLibrary
                    File.WriteAllText(path, myLibrary.ConvertMyLibraryToText(), Encoding.Unicode);

                    // Чтение данных из файла и создание по ним нового myLibrary
                    textFromFile = File.ReadAllText(path, Encoding.Unicode);
                    GenerateLibraryFromText(ref myLibraryFromFile, textFromFile);

                    // Вывод myLibrary на экран
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("myLibrary, полученный из файла");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(myLibraryFromFile);

                    // Вывод среднего колчества страниц в книгах и журналах
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if (!Double.IsNaN(myLibraryFromFile.averageNumberOfPagesInBooks))
                    {
                        Console.WriteLine($"Среднее количество страниц в книгах: {myLibraryFromFile.averageNumberOfPagesInBooks.ToString("F2")}.");
                    }
                    else
                    {
                        Console.WriteLine($"Среднее количество страниц в книгах: не удалось посчитать.");
                    }
                    if (!Double.IsNaN(myLibraryFromFile.averageNumberOfPagesInMagazines))
                    {
                        Console.WriteLine($"Среднее количесвто страниц в журналах: {myLibraryFromFile.averageNumberOfPagesInMagazines.ToString("F2")}.");
                    }
                    else
                    {
                        Console.WriteLine($"Среднее количесвто страниц в журналах: не удалось посчитать.");
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                    // Спросить пользователя, хочет ли он выполнить программу еще раз
                    AskToContinue(ref procced);
                }
                catch
                {
                    // Вывести извещение об ошибке и спросить, хочет ли пользователь выполнить программу занаво
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Произошла неизвестная ошибка");
                    Console.ForegroundColor = ConsoleColor.White;
                    AskToContinue(ref procced);
                }
            }

        }
    }
}