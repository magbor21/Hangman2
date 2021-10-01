using System;
using System.Text;

namespace Hangman2
{
    class Program
    {
        static void Main(string[] args)
        {

            bool bigLoop = true; // for added replayability
            
            while(bigLoop)
            {            
                Random rng = new Random(); //rng = Random Number Generator

                string[] wordList = new string[] { "KASSETTBANDSPELARE", "VARMLUFTSBALLONG", "SKIFTNYCKEL", "HAWAII", "GARDEROB", "ALMANACKA", "FÖRSTORNINGSGLAS", "VATTENPOLO" };
                string theWord = wordList[rng.Next(wordList.Length)];
                StringBuilder wrongLetters = new StringBuilder();

                char[] correctLetters = new char[theWord.Length];      
                for (int i = 0; i < correctLetters.Length; i++)
                    correctLetters[i] = '_';
                       
            
                bool gameOn = true; //Current game is running

                while (gameOn)
                {
                    WriteTheInterface(correctLetters, wrongLetters.ToString()); //Writes the interface 
                    string guessString = Console.ReadLine().ToUpper();
                    bool guessedRight = false;

                    if (guessString.Length == 1) // One letter or maybe a number
                    {
                        char guessChar = guessString[0];  //convert to char

                        if (char.IsLetter(guessChar))
                        {
                            if (!GuessedBefore(wrongLetters.ToString(), new string(correctLetters), guessChar)) //Check if letter has been guessed before
                            {
                                for (int i = 0; i < theWord.Length; i++) // Check against the word and fill in the correct letters
                                {
                                    if (guessChar == theWord[i])
                                    {
                                        correctLetters[i] = guessChar;
                                        guessedRight = true;            // in case there are more of the same letter in the word
                                    }
                                }
                            }
                            else
                                continue; // Ask for a new guess if already guessed

                            if (!guessedRight)
                                wrongLetters.Append(guessChar);


                        }
                        else        //any one character not a letter exits
                            break;

                    }

                    if (guessString.Length > 1)  //wordguess
                    {
                        if (guessString == theWord)       // If correct
                            correctLetters = theWord.ToCharArray(); // fill in whole word
                        else
                        {
                            for (int i = 0; i < guessString.Length; i++)
                            {
                                if (!GuessedBefore(wrongLetters.ToString(), new string(correctLetters), guessString[i]))
                                {
                                    if (theWord.IndexOf(guessString[i]) == -1)
                                        wrongLetters.Append(guessString[i]);
                                }
                            }
                        }
                    }


                    if (theWord == (new string(correctLetters)))  // If player Wins
                    {
                        WriteTheInterface(correctLetters, wrongLetters.ToString());
                        Console.WriteLine(guessString);

                        Console.WriteLine("HURRA!");
                        Console.WriteLine("Du gissade det rätta ordet!");

                        gameOn = false;
                    }

                    if (wrongLetters.Length >= 10)          // If player loses
                    {
                        Console.WriteLine("GAME OVER!");
                        Console.WriteLine("Du lyckades inte gissa ordet.");
                        gameOn = false;

                    }
                }

                Console.Write("\nSpela igen? J/N: ");
                string igen = Console.ReadLine().ToUpper();
                bigLoop = (igen[0] == 'J');                     // Everything else than a J exits
            }
        }

        static bool GuessedBefore(string wrongGuesses,string rightGuesses, char guessChar)
        {
            Console.WriteLine(wrongGuesses);
            Console.WriteLine(rightGuesses);
            Console.WriteLine(guessChar); 


            if (wrongGuesses.Length > 0)
                if (wrongGuesses.IndexOf(guessChar) >= 0)     // in wrong guesses
                    return true;
            if(rightGuesses.Length > 0)
                if (rightGuesses.IndexOf(guessChar) >= 0)  // in correct guesses 
                    return true;

            //else    
            return false;
        }

        static void WriteTheInterface(char[] correctGuesses, string wrongGuesses)
        {
            Console.Clear();
            Console.WriteLine("HÄNGA GUBBE PÅ SVENSKA\n");
            Console.Write("Ordet: ");
            for (int i = 0; i < correctGuesses.Length; i++)
                Console.Write(correctGuesses[i] + " ");
            Console.WriteLine();
            Console.WriteLine("Felgissningar: " + wrongGuesses);
            Console.Write("Gissa > ");

        }
            
    }
}
