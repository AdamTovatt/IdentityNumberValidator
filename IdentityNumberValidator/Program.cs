using IdentityNumberValidator.Models;
using System;
using System.IO;

namespace IdentityNumberValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (TakeIntInput("1. Enter number manually\n2. Load list from specified path", 1, 2) == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter an identity number:\n");
                    HandleIdentityNumber(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Please enter the relative or full path to a file containing identity numbers separated by line breaks.\nOne identity number per line:\n");
                    string path = Console.ReadLine();
                    
                    if(!File.Exists(path))
                    {
                        Console.WriteLine("The specified file could not be found");
                    }
                    else
                    {
                        string[] numbers = null;

                        try
                        {
                            numbers = File.ReadAllLines(path);
                        }
                        catch
                        {
                            Console.WriteLine("Unknown error when reading file");
                        }

                        if(numbers != null)
                        {
                            Console.Clear();
                            Console.WriteLine("Identity numbers from the file:\n");
                            foreach(string number in numbers)
                            {
                                HandleIdentityNumber(number);
                            }
                        }
                    }
                }

                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void HandleIdentityNumber(string number)
        {
            IdentityNumber identityNumber = IdentityNumber.CreateFromString(number);
            ValidationResult validationResult = identityNumber.Validate();

            if (!validationResult.Valid)
                Console.WriteLine(validationResult.InvalidReason);
            else
                Console.WriteLine(string.Format("{0} is valid. It is a type of {1}", identityNumber.NumberData, identityNumber.GetType().Name)); ;
        }

        private static int TakeIntInput(string promt, int min, int max)
        {
            Console.WriteLine(promt);

            int result = 0;

            while (!int.TryParse(Console.ReadLine(), out result) || !(result <= max && result >= min))
                Console.WriteLine(string.Format("Please enter an integer between {0} and {1}", min, max));

            return result;
        }
    }
}
