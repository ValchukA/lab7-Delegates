using System;
using System.IO;
using System.Text;

namespace DelegatesPart1
{
    class Program
    {
        static void Main()
        {
            Action<Func<char>, int, bool> print = PrintSequence;

            print += delegate(Func<char> charPicker, int count, bool upperCase)
            {
                if (charPicker != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine((upperCase == true) ? char.ToUpper(charPicker()) : charPicker());
                    }
                }
            };

            print += (Func<char> charPicker, int divider, bool printIfDivisible) =>
            {
                if (charPicker != null)
                {
                    char pickedChar = charPicker();

                    if (printIfDivisible == true && pickedChar % divider == 0)
                    {
                        Console.WriteLine(pickedChar);
                    }
                    else if (printIfDivisible == false && pickedChar % divider != 0)
                    {
                        Console.WriteLine(pickedChar);
                    }
                }
            };

            Random random = new Random();

            print?.Invoke(() => (char)random.Next(97, 123), 10, true);
        }

        static void PrintSequence(Func<char> charPicker, int length, bool printToFile)
        {
            if (charPicker != null)
            {
                StringBuilder str = new StringBuilder();

                char character = charPicker();

                for (int i = 0; i < length; i++)
                {
                    str.Append(character);

                    character = (char)(character + 1);
                }

                if (printToFile == false)
                {
                    Console.WriteLine(str);
                }
                else
                {
                    using var writer = File.CreateText("text.txt");

                    writer.WriteLine(str);
                }
            }
        }
    }
}
