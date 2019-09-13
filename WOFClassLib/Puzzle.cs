using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOFClassLib
{
    public class Puzzle
    {
        const char UNKNOWN_CHAR = '-';

        char[] displayChars;
        char[] solutionChars;
        bool solved;

        public bool Solved
        {
            get
            {
                return solved;
            }
        }
        public string Display
        {
            get
            {
                return new string(displayChars);
            }
        }
        public string Solution
        {
            set
            {
                solutionChars = value.ToUpper().ToCharArray();
                displayChars = solutionChars.Select(c=>(c!=' ')? UNKNOWN_CHAR : ' ').ToArray();
                solved = false;
            }
        }

        public Puzzle(string solution)
        {
            this.Solution = solution;
        }


        public int Guess(char guess)
        {
            int count = 0;

            for (int i=0; i< displayChars.Length; i++)
            {
                if (displayChars[i] == UNKNOWN_CHAR && solutionChars[i] == guess)
                {
                    count++;
                    displayChars[i] = guess;
                }
            }
            solved = displayChars.Where(c => c == UNKNOWN_CHAR).Count() == 0;
            return count;
        }

        public bool Solve(string guess)
        {
            string solution = new string(solutionChars);
            if (guess.ToUpper() == solution)
            {
                displayChars = solutionChars.ToArray();
                solved = true;
            }
            return solved;
        }

    }
}
