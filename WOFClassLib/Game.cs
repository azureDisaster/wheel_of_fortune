using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WOFClassLib
{
    public class Game
    {
        public  List<Player> players = new List<Player>();

        public Puzzle puzzle = new Puzzle("hello"); //hardcoded atm
        public static bool playing = true;
       
        //public int numberOfMatches = 0;
        public int totalPlayers;
        public void Start()
        {
            
            Console.WriteLine("Welcome to Wheel of Fortune!! How many players would you like to begin with?");
            totalPlayers = Int32.Parse(Console.ReadLine());

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

        public void Play(Player player)
        {
           
            Console.WriteLine("Hey {0}! Make a guess.", player.Name);
            string guess = Console.ReadLine(); 
            int numberOfCorrectLetters = 0; 

            try
            {
              Console.WriteLine("You guessed: {0}!", guess);
              numberOfCorrectLetters = player.GuessLetter(guess, puzzle);
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Please enter a single letter.");
                guess = Console.ReadLine();
                numberOfCorrectLetters = player.GuessLetter(guess, puzzle); 
            }

                bool isSolved = puzzle.IsSolved(); // false
                Console.WriteLine(isSolved);
                Console.WriteLine(puzzle.GetPuzzleDisplay());
           


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
                        Quit();
                    }
                    numberOfCorrectLetters = 0;
                } else
                {  
                    numberOfCorrectLetters = player.GuessLetter(guess, puzzle); 
                    isSolved = puzzle.IsSolved();
                   
                }
                Console.WriteLine("You guessed: {0}", guess);
            }

            if(isSolved)
            {
                Console.WriteLine("Congrats!");
                playing = false;
                

            } else
            {
            Console.WriteLine("Your guess was wrong... Let's move on to the next player.");
            }

        }
        public void Quit()
        {
            Console.WriteLine("The game is over! Press any key to exit out.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}