using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace WOFClassLib
{
    public class Puzzle
    {
        private string puzzlePhrase;
        private int phraseLength;
        private char[] display;
        private bool solved = false;

        public Puzzle(string phrase)
        {
            puzzlePhrase = phrase.ToLower();
            phraseLength = phrase.Length;
            display = InitializePuzzle(phrase.ToLower());
        } 
        
        private char[] InitializePuzzle(string phrase)
        {
            string puzzle = "";
            for(int i = 0; i < phraseLength; i++)
            {
                if(Char.IsWhiteSpace(phrase[i]))
                {
                    puzzle += " ";
                }
                else
                {
                    puzzle += "-";
                }
            }
            return puzzle.ToCharArray();
        }

        private char[] GetPuzzleDisplayAsArray()
        {
            return display;
        }

        public string GetPuzzleDisplay()
        {
            return new string(display);
        }

        public int Guess(char guess)
        {
            guess = Char.ToLower(guess);
            int numberOfMatches = 0;
            char[] currentDisplayArray = GetPuzzleDisplayAsArray();
            for(int i = 0; i < phraseLength; i++)
            {
                if(puzzlePhrase[i].Equals(guess))
                {
                    numberOfMatches++;
                    currentDisplayArray[i] = guess;
                }
            }
            return numberOfMatches;
        }

        public bool Solve(string guess)
        {
            if(guess.Equals(puzzlePhrase))
            {
                solved = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsSolved()
        {
            return solved;
        }

        public void IsValidPuzzle(string phrase)
        {
            for(int i = 0; i < phrase.Length; i++)
            {
                char c = phrase[i];
                
               
                if(!Char.IsLetter(c) && !Char.IsWhiteSpace(c))
                {
                    throw new ArgumentException("The puzzle phrase can only characters that are letters or spaces.");
                }

                if (i != phrase.Length - 1)
                {
                    char nextChar = phrase[i + 1];
                    if (Char.IsWhiteSpace(c) && char.IsWhiteSpace(nextChar))
                    {
                        throw new ArgumentException("The puzzle phrase should not contain consecutive spaces.");
                    }
                }
            }
        }

        public void IsValidGuess(char guess)
        {
            if (!Char.IsLetter(guess))
            {
                throw new ArgumentException("The guess character must be a valid letter");
            }
        }
    }
}
