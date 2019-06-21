using System;
using System.Collections.Generic;
using System.Threading;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Model m = new Model();
            List<Cars> cars = new List<Cars>();
            var timer = new Timer((e) =>
            {
                m.TimerCallback();
            }, null, 0, 30000);
            while (true)
            {
                Console.Write("Podaj operacje (1 - lista, 2 - dodawanie, 3 – wyjscie): ");
                string menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        Console.WriteLine("Producent Model Pojemność");
                        foreach (Cars item in m.GetTests())
                        {
                            Console.WriteLine(item.Manufacturer + " " + item.Model + " " + item.Capacity);
                        }
                        break;

                    case "2":
                        Console.WriteLine(m.AddToBuffer());
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
