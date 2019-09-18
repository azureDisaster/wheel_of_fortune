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

            Console.WriteLine("Game Over! Press any key to exit Wheel of Fortune.");


        }

        public void Play(int player)
        {
            // call Puzzle.Display();
            Console.WriteLine("Hey player {0}! Make a guess.", player);
            // input validation 
            string guess = Console.ReadLine(); // string to char : 'h'

            Console.WriteLine("You guessed: {0}!", guess); 
            int numberOfCorrectLetters = puzzle.Guess(guess); // number correct (1)
            Console.WriteLine(numberOfCorrectLetters); //testing
            bool isSolved = puzzle.IsSolved();
            Console.WriteLine(isSolved);
            // bool prevGuessCorrect = numberOfCorrectLetters >= 1 ? true : false; // T||F

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
                } else // try ti guess a single char
                {   // need to convert the string to char
                    numberOfCorrectLetters = puzzle.Guess(guess); // checking if guess is correct (which updates the puzzle.isSolved)
                    isSolved = puzzle.IsSolved();
                    
                    // check for if the guess was correct && check if puzzle is solved
                    // if the guess was correct, update numcorrectletters, update puzzle and keep looping
                    // if the puzzle was solved, update numCorrectLetters, update isSolved (breaks loop)
                    // if inccorect, numcorrectletters = 0

                   
                }
                Console.WriteLine("You guessed: {0}", guess);

                numberOfCorrectLetters = puzzle.Guess(guess);
            }

            Console.WriteLine("Your guess was wrong... Let's move on to the next player.");

        }
        public void Quit()
        {
            return;
        }
    }
}