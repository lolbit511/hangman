using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman2
{
    internal class Program
    {
        // title
        // hardcoded word : fever
        // shows chances (lives)
        // shows final word length
        // repeat-able
        // 
        // Learn:
        // file reading
        // randomizer
        // system clear function
        //
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Hangman");

            int lives = 15;
            string word = randWord(ref lives);
            string wordDupe = word;

            bool gameRunning = true;

            string guesses = "";
            string blanks = generateBlanks(word);

            int corrects = word.Length-1;
            
            Console.WriteLine("You have " + lives + " lives!");

            while (gameRunning)
            {
                //EmptySpaces(word, corrects);
                Console.WriteLine(blanks);
                Console.WriteLine(guesses);

                Console.WriteLine("Insert your chosen character: ");
                string input = Console.ReadLine();
                Guess(input, ref guesses, ref blanks, ref word, ref lives, ref corrects);

                Console.WriteLine("corrects: " + corrects);
                /*Console.Clear();
                Console.WriteLine("You have chosen: " + chosenList);

                corrects = Check(word, chosenList, corrects);
                Console.WriteLine("correct choices: " + corrects);*/

                Console.WriteLine("lives remaining: " + lives);
                if (lives < 1)
                {
                    Console.Clear();
                    Console.WriteLine("You lost!");
                    Console.WriteLine("The correct word was " + wordDupe + "!");
                    gameRunning = false;
                }
                if (corrects < 0)
                {
                    Console.Clear();
                    Console.WriteLine("You won!");
                    Console.WriteLine(wordDupe);
                    gameRunning = false;
                }

            }

        }

        static string randWord(ref int lives)
        {
            string path = @"D:\unity projects\Hangman2\randomlist.txt";

            string[] randomWord = File.ReadAllLines(path);

            Random rand = new Random();
            int r = rand.Next(0,randomWord.Length);

            return randomWord[r];

        }



        static string generateBlanks(string word)
        {
            string blanks = "";
            for (int i = 0; i < word.Length; i++)
            {
                blanks += '_';
            }
            return blanks;
        }

        static bool correctGuess(string input, string word)
        {
            bool result = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (input[0] == word[i])
                {
                    result = true;
                    break;
                }
            }
            
            return result;
        }

        static void updateBlanks(string input, ref string blanks, List<int> index)
        {
            for (int i = 0; i < index.Count; i++)
            {
                int temp = index[i];
                char[] temp2 = blanks.ToCharArray();
                temp2[temp] = input[0];
                blanks = new string(temp2);
            }
        }


        static void Guess(string input, ref string guesses, ref string blanks, ref string word, ref int lives, ref int corrects)
        {
            if (input != " " && input.Length == 1 && Char.IsLetter(input[0]) == true)
            {

                if (correctGuess(input, word))
                {

                    List<int> index = new List<int>();
                    for (int i = 0; i < word.Length; i++)
                    {

                        if (input[0] == word[i])
                        {
                            char[] temp = word.ToCharArray();
                            temp[i] = Convert.ToChar("-");
                            word = new string(temp);

                            Console.WriteLine("----------------word DEBUG: " + word);
                            corrects--;
                            index.Add(i);
                        }
                    }
                    
                    updateBlanks(input, ref blanks, index);
                }
                else
                {
                    guesses = guesses + input + ", ";
                    lives--;
                    
                }
            }
            else
            {

                Console.WriteLine("Invalid! Re-insert character: ");
                input = Console.ReadLine();
                Guess(input, ref guesses, ref blanks, ref word, ref lives, ref corrects);
            }

            Console.Clear();
            
            
        }




        static void EmptySpaces(string word, string corrects)
        {
            for (int j = 0; j < word.Length; j++)
            {
                for (int i = 0; i < corrects.Length; i++)
                {

                    if (Convert.ToString(corrects[i]) == "_")
                    {
                        Console.Write(corrects[i] + " ");
                        break;
                    }
                    else
                    {
                        Console.Write("_" + " ");
                        break;
                    }
                }
            }
            Console.WriteLine("");
        }






        static string Check(string word, string chosenList, string corrects) // chosenList[0] is most recent guess
        {

            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < chosenList.Length; j++)
                {
                    if (chosenList[j] == word[i]) // scan through each word, replace "_" with actual guess
                    {
                        //corrects[j] = chosenList[j];
                        Console.WriteLine("correct");
                        break;
                    }

                }

            }

            return corrects;
        }




        static void hangman(string word, string guessed, string correct, string written)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (correct.Length != 0)
                {
                    for (int j = 0; j < correct.Length; j++)
                    {
                        if (correct[j] == word[i] || written[0] != word[i])
                        {
                            Console.Write(correct[j] + " ");
                            written = written + correct[j];
                        }
                        else
                        {
                            Console.Write("_" + " ");
                            break;
                        }
                    }
                }
                else
                {
                    Console.Write("_" + " ");
                }
            }
            Console.WriteLine(" ");
            Console.WriteLine("Choose your letter:");

            string chosen = Console.ReadLine();
            if (chosen == " " || chosen.Length != 1)
            {
                //Console.Clear();
                Console.WriteLine("invaild input");

                Console.Write("Chosen letters: ");
                guessed = chosen + ", " + guessed;

                Console.WriteLine("----------------------------------------------------");
                hangman(word, guessed, correct, written);
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (chosen[0] == word[i])
                    {
                        Console.Write("correct: ");
                        correct = chosen + correct;
                        Console.WriteLine(correct);
                    }

                }


                Console.Clear();
                Console.Write("Chosen letters: ");
                guessed = chosen + ", " + guessed;
                Console.WriteLine(guessed);

                Console.WriteLine("----------------------------------------------------");
                hangman(word, guessed, correct, written);
            }

        }
    }
}
