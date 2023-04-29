// !!! No using other than System are authorized !!!

using System;

namespace Case1_HangedMan.Game
{
    public static class Game
    {
        // This is a private global variable accessible from everywhere within this file
        // You can give as an argument of Loader.GetWord() the path to the word_bank.txt
        // By default if no argument is given the path will be at the root of the TP1 folder
        public static string WordToGuess = Loader.GetWord();

        /*
         * TODO implement this function
         * This function gets the user input from the console and return a char
         * The input must be a single letter (upper or lowercase)
         * The output must be a single lowercase letter (you can use Char.IsLetter() or your own functions)
         * In the case of an error, you must print an error message on the stderr and return 0 using an explicit cast
         */
        public static char GetInput()
        {
            Console.Write("Input a letter: ");
            var input = Console.ReadLine();
            if (input != null && input.Length == 1 && char.IsLetter(input[0]))
                return char.ToLower(input[0]);

            Console.Error.WriteLine("Invalid Argument");
            return (char) 0;
        }

        /*
         * TODO implement this function
         * This function clears the console and prints the 2 arguments
         * You should loop through the array guessedWord and print each char.
         */
        public static void DisplayWord(char[] guessedWord, string usedLetters)
        {
            Console.Clear();
            Console.Write("Your guess: ");

            var index = 0;
            while (index < guessedWord.Length)
            {
                Console.Write(guessedWord[index]);
                index++;
            }

            Console.WriteLine(", Used letters: " + usedLetters);
        }

        /*
         * TODO implement this function
         * This function displays the hangman according to the gameState and the current errorCount (usedLetters.Length)
         * The hangman ascii is found in Loader.Ascii, it is an array with the different states to display
         *      case gameState Loader.GameState.RUNNING: print the hangman according to the number of wrong letters
         *      case gameState Loader.GameState.LOST: print the the hangman at index Loader.DEFEAT in Loader.Ascii
         *      case gameState Loader.GameState.WON: print the the hangman at index Loader.VICTORY in Loader.Ascii
         */
        public static void DisplayHangman(string usedLetters, Loader.GameState gameState)
        {
            /*
             * This is a ternary operation you can read it as such
             * A ? B : C;
             * A ('if true' / '?') [then] B ('else' / ':') C;
             */

            Console.WriteLine(gameState == Loader.GameState.RUNNING
                ? Loader.Ascii[usedLetters.Length]
                : Loader.Ascii[gameState == Loader.GameState.WON ? Loader.Victory : Loader.Defeat]);
        }

        /*
         * TODO implement this function
         * This function returns a bool whether the letter is contained in guessedWord
         */
        public static bool ContainsLetter(char[] guessedWord, char letter)
        {
            var index = 0;
            while (index < guessedWord.Length)
            {
                if (guessedWord[index] == letter)
                    return true;
                index++;
            }


            return false;
        }

        /*
         * TODO implement this function
         * This function returns the string of usedLetters updated
         * If the letter was already guess or if the letter was already used, print an error on stderr and return
         * usedLetters unchanged
         * If the letter is in the the WordToGuess, update the guessedWord array and leaves usedLetters unchanged
         * else add it to the usedLetters string
         * To check if the letter is contained in usedLetter you can use String.Contains() function 
         * To check if the letter is contained in guessedWord you should use ContainLetter
         * (you may also use 'new string()' but ContainsLetter will be expected and tested)
         * At the end call DisplayWord and DisplayHangman and return usedLetters
         */
        public static string ValidateLetter(char[] guessedWord, string usedLetters, char letter,
            Loader.GameState gameState)
        {
            if (ContainsLetter(guessedWord, letter))
            {
                Console.Error.WriteLine("This letter has already been guessed!");
                return usedLetters;
            }

            if (usedLetters.Contains(letter))
            {
                Console.Error.WriteLine("You already used that letter...");
                return usedLetters;
            }

            var index = 0;
            var validLetter = false;
            while (index < WordToGuess.Length)
            {
                if (WordToGuess[index] == letter)
                {
                    guessedWord[index] = letter;
                    validLetter = true;
                }

                index++;
            }

            usedLetters += !validLetter ? letter.ToString() : "";
            DisplayWord(guessedWord, usedLetters);
            DisplayHangman(usedLetters, gameState);
            return usedLetters;
        }

        /*
         * TODO implement this function
         * This function returns the state of the game
         * If the number of letters in usedLetters is greater or equal to the number of attempts then the user lost
         * If the WordToGuess and guessedWord are equal then the user won
         * Otherwise, keep running the game
         */
        public static Loader.GameState GameStatus(char[] guessedWord, string usedLetters, int attempts)
        {
            if (usedLetters.Length >= attempts)
                return Loader.GameState.LOST;

            var index = 0;
            while (index < WordToGuess.Length)
            {
                if (WordToGuess[index] != guessedWord[index])
                    return Loader.GameState.RUNNING;
                index++;
            }

            return Loader.GameState.WON;
        }

        /*
         * TODO implement this function
         * This function calls DisplayWord, DisplayHangman and displays a victory or defeat message
         * In case of a defeat, print the WordToGuess
         */
        public static void EndScreen(char[] guessedWord, string usedLetters, Loader.GameState gameState)
        {
            DisplayWord(guessedWord, usedLetters);
            DisplayHangman(usedLetters, gameState);

            Console.WriteLine(gameState == Loader.GameState.WON
                ? "You won!"
                : "You lost :( The answer was " + WordToGuess);
        }

        /*
         * TODO implement this function
         * This function must initialise the variables used in the game:
         *      The gameState must be initialized to Loader.GameState.RUNNING
         *      The number of attempts is defined in Loader.Attempts
         *      The errorCount (usedLetters) must be initialized to an empty string
         *      Moreover, guessedWord can be initialised using Loader.GetEmptyDuplicate(WordToGuess);
         * The game must run as follows:
         *      Print the word and the hangman. While the game has to run, ask for an input.
         *      If the output is invalid, loop again else and update the variables.
         *      Upon exiting the loop, print the correct end message according to the gameState
         */
        public static void LaunchGame()
        {
            var gameState = Loader.GameState.RUNNING;
            var attempts = Loader.Attempts;

            var guessedWord = Loader.GetEmptyDuplicate(WordToGuess);
            var usedLetters = "";

            DisplayWord(guessedWord, usedLetters);
            DisplayHangman(usedLetters, gameState);
            while (gameState == Loader.GameState.RUNNING)
            {
                var input = GetInput();
                if (input == 0)
                    continue;

                usedLetters = ValidateLetter(guessedWord, usedLetters, input, gameState);
                gameState = GameStatus(guessedWord, usedLetters, attempts);
            }

            EndScreen(guessedWord, usedLetters, gameState);
        }
    }
}