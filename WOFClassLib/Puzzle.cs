using System;
using System.Collections.Generic;
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
            puzzlePhrase = phrase;
            phraseLength = phrase.Length;
            display = initializePuzzle(phrase);
        } 
        
        private char[] initializePuzzle(string phrase)
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

        public bool isSolved()
        {
            return solved;
        }

    }
}
