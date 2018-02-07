using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //check if letter was guessed before

            //check if letter is in wordToGuess

            //check if game has ended -> vicoty / lose

            if (victory || lose) return false;
            return true;
        }
    }
}
