using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HangMan_Quadrat
{
    class Program
    {
        #region Variables
        static string wordToGuess = "";
        static int errorCount = 0;

        static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static bool isRunning = true;

        static string triedLetters = "";
        static bool[] progress;
        static bool victory = false;

        static ConsoleColor endColor = ConsoleColor.Red;
        static string endMessage = "You're Looser!";
        #endregion

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            #region Greetings
            Console.WriteLine("Grüß dich.\n");
            //wordToGuess = GetInput(@"^[a-zA-Z-]{1,}$", "Bitte gebe dein zu erratenes Wort ein: ", "Eingabe fehlerhaft").ToUpper();
            wordToGuess = GetInput(3, "Bitte gebe dein zu erratenes Wort ein: ", "Eingabe fehlerhaft").ToUpper();
            #endregion

            progress = new bool[wordToGuess.Length];

            Console.Clear();
            DrawWord();

            while (isRunning)
            {
                RunGame();
            }

            #region draw end message
            Console.ForegroundColor = endColor;
            Console.WriteLine(endMessage);
            #endregion

            // last line in code...
            Console.ReadLine();
        }

        static void DrawWord()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (progress[i])
                {
                    Console.Write(wordToGuess[i] + " ");
                    continue;
                }
                Console.Write("_ ");
            }

            #region Draw Error Count
            string errorText = errorCount.ToString() + " Fehler!";
            // Curser Position an Rand rechts - Länge des zu schreibenden Textes
            Console.CursorLeft = Console.BufferWidth - Math.Max(errorText.ToString().Length, 1);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(errorText);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            #endregion

            Console.WriteLine("\n");

            if (!progress.Contains(false))
            {
                victory = true;
                endColor = ConsoleColor.Green;
                endMessage = "Gewonnen!\n";
            }
        }

        static void RunGame()
        {
            #region Check ob Spiel ist vorbei => vicoty || lose
            if (victory || errorCount >= 8)
            {
                Console.Clear();

                isRunning = false;
                if (!victory)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSpiel vorbei! Das Wort war " + wordToGuess + ".");

                    #region ASCII art
                    endMessage += "\n\n" + @" ___________.._______
| .__________))______|
| | / /      ||
| |/ /       ||
| | /        ||.-''.
| |/         |/  _  \
| |          ||  `/,|
| |          (\\`_.'
| |         .-`--'.
| |        /Y . . Y\
| |       // |   | \\
| |      //  | . |  \\
| |     ')   |   |   (`
| |          ||'||
| |          || ||
| |          || ||
| |          || ||
| |         / | | \
„„„„„„„„„„|_`-' `-' |„„„ |
| „ | „„„„„\ \       '„ | „ |
 | |        \ \        | |
: :          \ \       : :  
. .           `'       . .";
                    #endregion
                }

                return;
            }
            #endregion

            /** ask for Letter
            * ^: Anfang des String;
            * []: Gruppierung einzelner Zeichen;
            * {1,}: Anzahl (min, max) => min = 1, max = unbegrenzt;
            * $: Ende des String;
            */
            //string inputLetter = GetInput("^[a-zA-Z]{1,}$", "Eingabe eines neuen Buchstabens: ", "Die Eingabe darf nur ein Buchstabe sein!");
            string inputLetter = GetInput(1, "Eingabe eines neuen Buchstabens: ", "Die Eingabe darf nur Buchstaben enthalten!", "Die Eingabe ist zu kurz!");

            #region Prüfung für Eingaben länger als ein Zeichen
            if (inputLetter.Length > 1)
            {
                if (!HandleWord(inputLetter))
                {
                    errorCount += 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("Das Wort '" + inputLetter + "' ist falsch!");
                    DrawWord();
                    return;
                }

                victory = true;
                endMessage = "\nDas Wort '" + wordToGuess + "' wurde erraten! Glückwunsch!";
                endColor = ConsoleColor.Green;
                return;
            }
            #endregion

            #region Check Zeichen wurde bereits geraten
            if (triedLetters.Contains(inputLetter))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("'" + inputLetter + "' wurde bereits geraten");
                errorCount++;
                DrawWord();
                return;
            }

            triedLetters += inputLetter;
            #endregion

            Console.Clear();

            #region Check ob Zeichen ist in Wort
            if (wordToGuess.Contains(inputLetter))
            {
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i].ToString() == inputLetter)
                    {
                        progress[i] = true;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(inputLetter + " ist nicht enthalten!");
                errorCount++;
            }
            #endregion

            DrawWord();
        }

        private static bool HandleWord(string input)
        {
            if (input == wordToGuess)
            {
                return true;
            }

            return false;
        }

        #region Input Block
        static public string GetInput(string pattern, string requestMessage = "enter input ", string errorMessage = "invalid input")
        {
            // ähnlich zu Random rng = new Random(DateTime.Now.ToString().GetHash());
            // int test = rng.Next(0, 11);
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
            return input.ToUpper();
        }

        static public string GetInput(int minLength, string requestMessage = "enter input ", string errorMessage = "invalid input", string tooShortMessage = "input is too short")
        {
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(requestMessage);

                Console.ForegroundColor = ConsoleColor.Magenta;
                input = Console.ReadLine().ToUpper();
                validInput = true;

                //input zu kurz
                if (input.Length < minLength)
                {
                    validInput = false;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(tooShortMessage);

                    continue;
                }

                //Zeichen nicht im Alphabet
                foreach (char letter in input)
                {
                    if (!alphabet.Contains(letter))
                    {
                        validInput = false;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errorMessage);
                        break;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            return input;
        }
        #endregion
    }
}
