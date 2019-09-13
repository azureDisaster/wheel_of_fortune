using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOFClassLib;

namespace WheelOfFortune
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new Puzzle("Blues Clues");

            var player = new Player();

            Console.WriteLine($"{puzzle.Display}, Solved={puzzle.Solved}");

            player.GuessLetter('D',puzzle);
            Console.WriteLine($"{puzzle.Display}, Solved={puzzle.Solved}");

            player.GuessLetter('L', puzzle);
            Console.WriteLine($"{puzzle.Display}, Solved={puzzle.Solved}");

            player.SolvePuzzle("Blues Clues", puzzle);
            Console.WriteLine($"{puzzle.Display}, Solved={puzzle.Solved}");

            Console.ReadKey();
        }
    }
}
