// !!! Please do not edit this file !!!
// You are only allowed to change the ascii art in Ascii for the bonus

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Case1_HangedMan.Game
{
    public static class Loader
    {
        /*
         * Loads the file word_bank.txt and return a random word from the file
         * The path should be where the work_bank.txt is located in your directory
         * The function can be called with no arguments as its predefined value is at the root of the TP1 folder
         */
        public static string GetWord(string path = "../../../Game/word_bank.txt")
        {
            if (!File.Exists(path))
                throw new ArgumentException("Loader: couldn't load word bank at " + path);

            try
            {
                var index = new Random().Next(File.ReadLines(path).Count());
                return File.ReadLines(path).Skip(index).Take(1).First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /*
         * This function returns a char array initialized to '_' at size of the WordToGuess
         */
        public static char[] GetEmptyDuplicate(string word)
        {
            return new string('_', word.Length).ToCharArray();
        }

        /*
         * This enum logs the state of the game.
         */
        public enum GameState
        {
            RUNNING = 0,
            WON = 1,
            LOST = -1
        }

        /*
         * This is the array of all the possibles version of the hangman
         * The 2 last strings represent, in this order, the defeat and the victory version
         */
        public static readonly string[] Ascii =
        {
            @"
+---+
|   |
|
|
|
|
=========",
            @"
+---+
|   |
|   O
|
|
|
=========",
            @"
+---+
|   |
|   O
|   |
|
|
=========",
            @"
+---+
|   |
|  \O
|   |
|
|
=========",
            @"
+---+
|   |
|  \O/
|   |
|
|
=========",
            @"
+---+
|   |
|  \O/
|   |
|  /
|
=========",
            @"
+---+
|   |
|  \O/
|   |
|  / \
|
=========
I will survive!",
            @"
+---+
|   |
|   |o
|  /|\
|   ||
|
=========
...",
            @"
+---+
|   |
|
|   O
|  \|/
|  / \
=========
phewwww!"
        };

        // Global variables defining the positions of the defeat and victory versions in Ascii
        public static readonly int Defeat = Ascii.Length - 2;
        public static readonly int Victory = Ascii.Length - 1;

        // Global variable defining the number of attempts before the game ends
        public static readonly int Attempts = Ascii.Length - 2;
    }
}