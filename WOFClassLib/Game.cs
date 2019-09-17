using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOFClassLib
{
    public class Game
    {
        public List<int> players = new List<int>();

        public Puzzle puzzle = new Puzzle("hello"); //hardcoded atm
        public static bool playing = true;
        public int currentPlayer = 1;
        //public int numberOfMatches = 0;
        public int totalPlayers;
        public void Start()
        {
            
            Console.WriteLine("Welcome to Wheel of Fortune!! How many players would you like to begin with?");
            totalPlayers = Int32.Parse(Console.ReadLine());

            for (int i = totalPlayers; i > 0; i--)
            {

                players.Add(i);
            }


            Console.WriteLine("Alright, starting with {0} player(s)!", totalPlayers);

            while (playing) // if the game is being played, loop thru the players
            {
                Play(currentPlayer);
                currentPlayer = currentPlayer == totalPlayers ? 1 : currentPlayer += 1;
            }

            Quit();


        }

        public void Play(int player)
        {
           
            Console.WriteLine("Hey player {0}! Make a guess.", player);
            string guess = Console.ReadLine();
            int numberOfCorrectLetters = 0; 

            try
            {
              Console.WriteLine("You guessed: {0}!", guess);
              numberOfCorrectLetters = puzzle.Guess(guess); 
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Please enter a single letter.");
                guess = Console.ReadLine();
                numberOfCorrectLetters = puzzle.Guess(guess);
            }

                bool isSolved = puzzle.IsSolved();
                
            

            while (numberOfCorrectLetters >= 1 && !isSolved)
            {
                Console.WriteLine("Since you guessed correctly, make another guess or attempt to solve!");
                // if the guess.length > 1 then assign as a string
                guess = Console.ReadLine();
                if(guess.Length > 1) // trying to guess the phrase
                {
                    isSolved = puzzle.Solve(guess); // returns if solved with phrase or not
                    
                    
                    if (isSolved)
                    {
                        Console.WriteLine("You solved it!");
                        Quit();
                    }
                } else
                {  
                    numberOfCorrectLetters = puzzle.Guess(guess); 
                    isSolved = puzzle.IsSolved();
                   
                }
                Console.WriteLine("You guessed: {0}", guess);

                numberOfCorrectLetters = puzzle.Guess(guess);
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