using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune
{
    class Game
    {
        public List<int> players = new List<int>();

        public static bool playing = true;
        public int currentPlayer = 1;

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
            
            while(playing) // if the game is being played, loop thru the players
            {
               Game.Play(currentPlayer);
               currentPlayer = currentPlayer == totalPlayers ? 1 : currentPlayer += 1; 
            }

            Console.WriteLine("Game Over! Press any key to exit Wheel of Fortune.");


        }

        public static void Play(int player)
        {
            // call Puzzle.Display();
            Console.WriteLine("Hey player {0}! Make a guess.", player);
            string guess = Console.ReadLine();

            Console.WriteLine("You guessed: {0}", guess);

            bool prevGuessCorrect = true; // Depends on Puzzle.Guess() which returns bool depending on guess

            while (prevGuessCorrect)
            {
                Console.WriteLine("Since you guessed correctly, make another guess!");
                guess = Console.ReadLine();

                prevGuessCorrect = false;
                // calls puzzle method to see if the guess was correct 
                // if correct: 
                // will display the puzzle with newly filled in letters 
                // if the player won this round it will tell the player they won
                // will prompt player to guess again 
                // if incorrect:
                // will return so that the above code can move on to the next player
            }

            Console.WriteLine("Your guess was wrong... Let's move on to the next player.");

        }
    }
}
