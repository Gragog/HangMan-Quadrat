using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HangMan_Quadrat
{
    class Program
    {
        static string wordToGuess = "";
        static int errorCount = 0;

        static bool isRunning = true;

        static char[] alphabet;
        static char[] errorAlphabet;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            Console.WriteLine("Grüß dich.\nBitte gebe dein zu erratenes Wort ein.");
            wordToGuess = Console.ReadLine().ToUpper();
            Console.Clear();
            DrawUnderline();

            while (isRunning)
            {
                RunGame();
            }
            



            // last line in code...
            Console.ReadLine();
        }
        static void DrawUnderline()
        {
            foreach (char letter in wordToGuess)
            {
                Console.Write("_ ");
            }
            Console.Write("\n");
            
        }
        
        static bool RunGame()
        {
            //ask for Letter
            string inputLetter = Input.GetInput(@"^[a-zA-Z]{1}$", "Eingabe eines neuen Buchstabens: ", "Die Eingabe darf nur ein Buchstabe sein!");
            //check if letter was guessed before

            //check if letter is in wordToGuess

            //check if game has ended -> vicoty / lose

            //if (victory || lose) return false;
            return false;
        }

        #region Input
        public struct Input
        {
            static public string GetInput(string pattern, string requestMessage = "enter input ", string errorMessage = "invalid input")
            {
                Regex item = new Regex(pattern);
                bool validInput = false;
                string input = "";

                while (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(requestMessage);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    input = Console.ReadLine();

                    if (item.IsMatch(input))
                    {
                        validInput = true;
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                return input;
            }
        }
        #endregion
    }
}
