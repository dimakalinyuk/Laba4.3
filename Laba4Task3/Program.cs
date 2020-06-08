using System;
using System.IO;

namespace Laba4Task3
{
    class Program
    {
        public static Bank[] b = new Bank[1000000];
        public static bool[] delete = new bool[1000000];


        static void Main(string[] args)
        {

            Input.Key();



        }

    }

    class Bank
    {
        private string name;
        private string surname;
        private string date;
        private int suma;


        public string Name
        {
            get { return name; }
            set { name = value; }

        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public int Suma
        {
            get { return suma; }
            set
            {
                if (value > 0) suma = value;
                else throw new FormatException();
            }
        }



        public Bank(string song, string singer, string album, int year)
        {
            this.name = song;
            this.surname = singer;
            this.date = album;
            this.suma = year;

        }
    }
    class Output
    {
        public static void Write(Bank[] m)
        {
            Console.WriteLine("{0,-30} {1, -20} {2, -30} {3, -15}", "Імя", "Прізвище", "Дата", "Сума вкладу");

            for (int i = 0; i < m.Length; ++i)
            {
                if (m[i] != null)
                {
                    Console.WriteLine("{0,-30} {1, -20} {2, -30} {3, -15}", Program.b[i].Name, Program.b[i].Surname, Program.b[i].Date, Program.b[i].Suma);
                }
            }
        }

        public static void Write1(Bank[] m, bool[] write)
        {
            Console.WriteLine("{0,-30} {1, -20} {2, -30} {3, -15}", "Імя", "Прізвище", "Дата", "Сума вкладу");

            for (int i = 0; i < m.Length; ++i)
            {
                if ((write[i]) && (!Program.delete[i]))
                {
                    Console.WriteLine("{0,-30} {1, -20} {2, -30} {3, -15}", Program.b[i].Name, Program.b[i].Surname, Program.b[i].Date, Program.b[i].Suma);
                }
            }
        }
    }

    class Input
    {


        public static void Key()
        {
            Work.Parse(Read(), false);

            Console.WriteLine("Додавання записiв: +");
            Console.WriteLine("Редагування записiв: E");
            Console.WriteLine("Знищення записiв: -");
            Console.WriteLine("Виведення записiв: Enter");
            Console.WriteLine("Пошук записiв: F");
            Console.WriteLine("Сортуванн записiв: S");
            Console.WriteLine("Вихiд: Esc");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.OemPlus:
                    Console.WriteLine();
                    Work.Add();
                    break;

                case ConsoleKey.E:
                    Console.WriteLine();
                    Work.Edit();
                    break;

                case ConsoleKey.OemMinus:
                    Console.WriteLine();
                    Work.Remove();
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Output.Write(Program.b);
                    Key();
                    break;

                case ConsoleKey.F:
                    Console.WriteLine();
                    Work.Find();
                    break;

                case ConsoleKey.S:
                    Console.WriteLine();
                    Work.Sort();
                    break;

                case ConsoleKey.Escape:
                    return;
            }
        }
        public static string[] Read()
        {
            StreamReader fromFile = new StreamReader("text.txt");

            return fromFile.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }
    }
    class Work
    {
        public static void Add()
        {
            Console.WriteLine("Введiть данi");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Parse(elements, true);

            Input.Key();
        }

        public static void Remove()
        {
            Console.Write("Прізвище: ");

            string singer = Console.ReadLine();

            bool[] write = new bool[Program.b.Length];

            for (int i = 0; i < Program.b.Length; ++i)
            {
                if (Program.b[i] != null)
                {
                    if (Program.b[i].Surname == singer)
                    {
                        Console.WriteLine("{0,-30} {1, -30} {2, -30} {3, -15}", Program.b[i].Name, Program.b[i].Surname, Program.b[i].Date, Program.b[i].Suma);

                        Console.WriteLine("Видалити? (Y/N)");

                        var key = Console.ReadKey().Key;

                        if (key == ConsoleKey.Y)
                        {
                            Program.delete[i] = true;
                        }
                        else
                        {
                            Program.delete[i] = false;
                        }
                    }
                }
            }
        }

        public static void Edit()
        {
            Console.Write("Прізвище: ");

            string singer = Console.ReadLine();

            bool[] write = new bool[Program.b.Length];

            for (int i = 0; i < Program.b.Length; ++i)
            {
                if (Program.b[i] != null)
                {
                    if (Program.b[i].Surname == singer)
                    {
                        Console.WriteLine("{0,-30} {1, -20} {2, -30} {3, -15}", Program.b[i].Name, Program.b[i].Surname, Program.b[i].Date, Program.b[i].Suma);

                        Console.WriteLine("Введiть нову iнформацiю");

                        string str = Console.ReadLine();

                        string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        Program.b[i] = new Bank(elements[0], elements[1], elements[2], int.Parse(elements[3]));
                    }
                }
            }
        }

        public static void Find()
        {
            Console.Write("Дата: ");

            string singer = Console.ReadLine();

            bool[] write = new bool[Program.b.Length];

            for (int i = 0; i < Program.b.Length - 1; ++i)
            {
                if (Program.b[i] != null)
                {
                    if (Program.b[i].Date == singer)
                    {
                        write[i] = true;

                    }
                    else
                    {
                        write[i] = false;

                    }
                }

            }

            Output.Write1(Program.b, write);

            Input.Key();
        }

        public static void Sort()
        {
            int index = 0;

            while (Program.b[index + 1] != null)
            {
                for (int i = 0; i < Program.b.Length - 1; ++i)
                {
                    if (Program.b[i + 1] != null)
                    {
                        if (Program.b[i].Suma > Program.b[i + 1].Suma)
                        {
                            string tempStr;
                            int tempInt;

                            tempInt = Program.b[i].Suma;
                            Program.b[i].Suma = Program.b[i + 1].Suma;
                            Program.b[i + 1].Suma = tempInt;


                            tempStr = Program.b[i].Date;
                            Program.b[i].Date = Program.b[i + 1].Date;
                            Program.b[i + 1].Date = tempStr;

                            tempStr = Program.b[i].Surname;
                            Program.b[i].Surname = Program.b[i + 1].Surname;
                            Program.b[i + 1].Surname = tempStr;

                            tempStr = Program.b[i].Name;
                            Program.b[i].Name = Program.b[i + 1].Name;
                            Program.b[i + 1].Name = tempStr;


                        }
                    }
                }

                ++index;
            }

            Output.Write(Program.b);

            Input.Key();
        }


        private static bool needToReOrder(Bank musicCollection1, Bank musicCollection2)
        {
            throw new NotImplementedException();
        }

        private static void Save(Bank m)
        {
            StreamWriter save = new StreamWriter("text.txt", true);

            save.WriteLine(m.Name);
            save.WriteLine(m.Surname);
            save.WriteLine(m.Date);
            save.WriteLine(m.Suma);


            save.Close();
        }

        public static void Parse(string[] elements, bool save)
        {
            int counter = 0;

            while (Program.b[counter] != null)
            {
                ++counter;
            }

            for (int i = 0; i < elements.Length; i += 4)
            {
                Program.b[counter + i / 4] = new Bank(elements[i], elements[i + 1], elements[i + 2], int.Parse(elements[i + 3]));

                if (save)
                {
                    Save(Program.b[counter + i / 4]);
                }
            }
        }
        public static string[] Read()
        {
            StreamReader fromFile = new StreamReader("text.txt");

            return fromFile.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }




    }
}
