using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace WOFClassLib
{
    public class Player
    {

        private int totalMoney = 0;
        private int roundMoney = 0;

        public string Name { get; set; }
        public int TotalMoney
        {
            get { return totalMoney; }
        }

        public int RoundMoney {
            get { return roundMoney;  }
        }

        public Player(string name = "Player")
        {
            Name = name;
            totalMoney = 0;
            roundMoney = 0;
        }

        public int GuessLetter(char guess, Puzzle puzzle, int spinAmount=0)
        {
            int numLetters = puzzle.Guess(guess);
            roundMoney += numLetters * spinAmount;
            return numLetters;
        }

        public bool SolvePuzzle(string guess, Puzzle puzzle)
        {
            bool isSolved = puzzle.Solve(guess);
            if (isSolved)
            {
                totalMoney += RoundMoney;
            }
            return isSolved;
        }

        public void NewRound()
        {
            roundMoney = 0;
        }

        public void NewGame()
        {
            totalMoney = 0;
            roundMoney = 0;
        }

    }
}
