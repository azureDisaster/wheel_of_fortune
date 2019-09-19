using System;
using System.Collections.Generic;

namespace WOFClassLib
{
    /// <summary>
    /// This class uses the Player and Puzzle classes to perform the functions of running a game.
    /// </summary>
    public class Game
    {
        public  List<Player> players = new List<Player>();
        private Phrase phrase = new Phrase();
        public Puzzle puzzle;
        public static bool playing = true;
        public int totalPlayers;
       
        /// <summary>
        /// This method will prompt the user for details regarding the initialization of the game. 
        /// </summary>
        public void Start()
        {
            puzzle = new Puzzle(phrase.GetPhrase());
            bool valid = false;
            do
            {
                Console.WriteLine("Welcome to Wheel of Fortune!! How many players would you like to begin with?");
                string input = Console.ReadLine();
                valid = Int32.TryParse(input, out totalPlayers) && totalPlayers >= 1 ? Int32.TryParse(input, out totalPlayers) : false;
            } while (!valid);

            for (int i = 0; i < totalPlayers; i++)
            {
                Console.WriteLine("Hey player {0} add your name.", i+1);
                players.Add(new Player(Console.ReadLine())); // adds a player obj to list
                
            }
       
            Console.WriteLine("Alright, starting with {0} player(s)!", totalPlayers);
            int index = 0;
            Player currentPlayer = players[index];

            while (playing) // if the game is being played, loop thru the players
            {
                Play(currentPlayer); // call play on the current player object
                index = index + 1 == totalPlayers ? 0 : index += 1;
                currentPlayer = players[index];
                // currentPlayer = currentPlayer == totalPlayers ? 1 : currentPlayer += 1;
            }

            Quit();
        }

        /// <summary>
        /// This method will create a player for every player in the game. 
        /// </summary>
        /// <param name="player">A player object instantiated by the Player class.</param>
        public void Play(Player player)
        {
            Console.WriteLine("Hey {0}! Make a guess.", player.Name);
            Console.WriteLine(puzzle.GetPuzzleDisplay());
            string guess = Console.ReadLine(); 
            int numberOfCorrectLetters = 0; 

            try
            {
              Console.WriteLine("You guessed: {0}!", guess);
              //Console.WriteLine(puzzle.GetPuzzleDisplay());
              numberOfCorrectLetters = player.GuessLetter(guess, puzzle);
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Please enter a single letter.");
                guess = Console.ReadLine();
                numberOfCorrectLetters = player.GuessLetter(guess, puzzle);
            }
            Console.WriteLine(puzzle.GetPuzzleDisplay());
            bool isSolved = puzzle.IsSolved(); // false
                
           


            while (numberOfCorrectLetters >= 1 && !isSolved)
            {
                
                Console.WriteLine("Since you guessed correctly, make another guess or attempt to solve!");
                
                // if the guess.length > 1 then assign as a string
                guess = Console.ReadLine();
                if(guess.Length > 1) // trying to guess the phrase
                {
                    isSolved = player.SolvePuzzle(guess, puzzle); // last modified
                    if (isSolved)
                    {
                        Console.WriteLine("You solved it!");
                        Console.WriteLine(puzzle.GetPuzzleDisplay());
                        Quit();
                    }
                    numberOfCorrectLetters = 0;
                } else
                {  
                    numberOfCorrectLetters = player.GuessLetter(guess, puzzle); 
                    isSolved = puzzle.IsSolved();
                   

                }
                Console.WriteLine("You guessed: {0}", guess);
                Console.WriteLine(puzzle.GetPuzzleDisplay());
            }

            if(isSolved)
            {
                Console.WriteLine("Congrats!");
                playing = false;
                

            } else
            {
                if(totalPlayers == 1)
                {
                 Console.WriteLine("Your guess was wrong. It's okay, you may try again.");
                }
                else
                {
                 Console.WriteLine("Your guess was wrong... Let's move on to the next player.");
                }
            }

        }
        /// <summary>
        /// If called, this method will exit the game once the user presses a key.
        /// </summary>
        public void Quit()
        {
            Console.WriteLine("The game is over! Press any key to exit out.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}