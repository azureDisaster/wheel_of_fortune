using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                Console.WriteLine("\n \n Welcome to Wheel of Fortune \n   sponsored by Azure Disaster LLC. \n\n How many players would you like to begin with? \n");
                string input = Console.ReadLine();
                valid = Int32.TryParse(input, out totalPlayers) && totalPlayers >= 1 ? Int32.TryParse(input, out totalPlayers) : false;
            } while (!valid);

            for (int i = 0; i < totalPlayers; i++)
            {
                Console.WriteLine("\nHey player {0} What's your name? \n", i+1);
                players.Add(new Player(Console.ReadLine())); // adds a player obj to list
                
            }
       
            Console.WriteLine("\nAlright, starting with {0} player(s)! \n", totalPlayers);
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
            Console.WriteLine("\n Hey {0}! Now it's your turn, make a guess. Remember, you can only guess a letter, no solving allowed!\n", player.Name);
            Console.WriteLine(puzzle.GetPuzzleDisplay());
            string guess = ""; 
            int numberOfCorrectLetters = 0; 

            bool validGuess = false;

            while (!validGuess) {
                Console.WriteLine("\nThis is your first guess, please enter a single letter. \n");
                guess = Console.ReadLine();
                Console.WriteLine("\nYou guessed: {0}! \n", guess);
                validGuess = Regex.IsMatch(guess, "^[a-zA-Z]") && guess.Length == 1;
            }

            numberOfCorrectLetters = player.GuessLetter(guess, puzzle);

            Console.WriteLine(puzzle.GetPuzzleDisplay());
            bool isSolved = puzzle.IsSolved(); // false
                

            while (numberOfCorrectLetters >= 1 && !isSolved)
            {

                Console.WriteLine("\nSince you guessed correctly, make another guess or attempt to solve! \n");
                // if the guess.length > 1 then assign as a string
                guess = Console.ReadLine();

                bool stringGuess = Regex.IsMatch(guess, "^[a-zA-Z]+"); // returns true if only contains letters
                                                                    
                while (!stringGuess)
                {
                    Console.WriteLine("\nPlease guess a letter or phrase.\n");
                    guess = Console.ReadLine();
                    Console.WriteLine("\nYou guessed: {0}! \n", guess);
                    stringGuess = Regex.IsMatch(guess, @"^[a-zA-Z]+$");
                }

                if (guess.Length > 1) // trying to guess the phrase
                {
                    isSolved = player.SolvePuzzle(guess, puzzle); // last modified
                    if (isSolved)
                    {
                        Console.WriteLine("YAYYYY! You solved it! \n");
                        Console.WriteLine(puzzle.GetPuzzleDisplay());
                        Quit();
                    }
                    numberOfCorrectLetters = 0;
                } else
                {  
                    numberOfCorrectLetters = player.GuessLetter(guess, puzzle); 
                    isSolved = puzzle.IsSolved();
                   

                }
                Console.WriteLine("You guessed: {0} \n", guess);
                Console.WriteLine(puzzle.GetPuzzleDisplay());
            }

            if(isSolved)
            {
                Console.WriteLine("\n Congrats! You solved it! \n");
                playing = false;
                

            } else
            {
                if(totalPlayers == 1)
                {
                 Console.WriteLine("Your guess was wrong. It's okay, you may try again. \n");
                }
                else
                {
                 Console.WriteLine("Your guess was wrong... Let's move on to the next player. \n");
                }
            }

        }
        /// <summary>
        /// If called, this method will exit the game once the user presses a key.
        /// </summary>
        public void Quit()
        {
            Console.WriteLine("The game is over! Press any key to exit...Byeeee~ \n");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}