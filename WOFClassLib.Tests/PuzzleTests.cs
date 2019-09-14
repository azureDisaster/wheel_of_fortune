using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WOFClassLib.Tests
{
    public class PuzzleTests
    {
        [Fact]
        public void TestGetDisplay()
        {
            Puzzle testPuzzle1 = new Puzzle("dog");
            string testDisplay1 = "---";
            string puzzleDisplay1 = testPuzzle1.GetPuzzleDisplay();
            Assert.Equal(testDisplay1, puzzleDisplay1);

            Puzzle testPuzzle2 = new Puzzle("dogs and cats");
            string testDisplay2 = "---- --- ----";
            string puzzleDisplay2 = testPuzzle2.GetPuzzleDisplay();
            Assert.Equal(testDisplay2, puzzleDisplay2);
        }
    }
}
