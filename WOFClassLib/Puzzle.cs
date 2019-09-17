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
            puzzlePhrase = phrase.ToUpper();
            phraseLength = phrase.Length;
            display = InitializePuzzle(phrase.ToUpper());
        } 
        
        private char[] InitializePuzzle(string phrase)
        {
            string puzzle = "";

            for (int i = 0; i < phraseLength; i++)
            {
                char c = puzzlePhrase[i];

                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    throw new ArgumentException("The puzzle phrase should only contain characters that are letters or spaces.");
                }

                if (i != phraseLength - 1)
                {
                    char nextChar = puzzlePhrase[i + 1];
                    if (char.IsWhiteSpace(c) && char.IsWhiteSpace(nextChar))
                    {
                        throw new ArgumentException("The puzzle phrase should not contain consecutive spaces.");
                    }
                }

                if(char.IsWhiteSpace(c))
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
            if (!char.IsLetter(guess))
            {
                throw new ArgumentException("The guessed character must be a valid letter");
            }

            guess = char.ToUpper(guess);
            int numberOfMatches = 0;
            char[] currentDisplayArray = GetPuzzleDisplayAsArray();
            for(int i = 0; i < phraseLength; i++)
            {
                if(puzzlePhrase[i].Equals(guess))
                {
                    numberOfMatches++;
                    currentDisplayArray[i] = guess;
                    if(!display.Contains('-'))
                    {
                        solved = true;
                    }
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
    }
}
