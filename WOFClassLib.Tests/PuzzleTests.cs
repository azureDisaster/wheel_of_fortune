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

        [Theory]
        [InlineData("dog", new char[] {'d'}, "d--")]
        [InlineData("dog", new char[] { 'o' }, "-o-")]
        [InlineData("dog", new char[] { 'g', 'o' }, "-og")]
        [InlineData("dog", new char[] { 'g', 'g' }, "--g")]
        [InlineData("dogs and cats", new char[] { 'C', 'a', 'S', 'd' }, "d--s a-d ca-s")]
        [InlineData("leap", new char[] { 'x', 'p' }, "---p")]
        public void TestGuessUpdatesPuzzle(string phrase, char[] guesses, string expectedResultDisplay)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            foreach (char guess in guesses)
            {
                testPuzzle.Guess(guess);
            }
            Assert.Equal(expectedResultDisplay, testPuzzle.GetPuzzleDisplay());
        }

        [Theory]
        [InlineData("quest", 'z', 0)]
        [InlineData("wolf", 'w', 1)]
        [InlineData("little red riding hood", 'r', 2)]
        public void TestGuessReturnsCorrectNumberOfMatches(string phrase, char guess, int expectedNumMatches)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            int numberOfMatches = testPuzzle.Guess(guess);
            Assert.Equal(expectedNumMatches, numberOfMatches);
        }

        [Theory]
        [InlineData("Microsoft Azure", "microsoft azure", true)]
        [InlineData("dogs", "cats", false)]
        [InlineData("United States of America", "United States", false)]
        public void TestSolvePuzzle(string phrase, string guess, bool expectedResult)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            if(expectedResult == true)
            {
                Assert.True(testPuzzle.Solve(guess));
                Assert.True(testPuzzle.IsSolved());
            }
            else
            {
                Assert.False(testPuzzle.Solve(guess));
                Assert.False(testPuzzle.IsSolved());
            } 
        }

        //[Theory]
        //[InlineData("dogs   cats")]
        //[InlineData("back 2 back")]
        //[InlineData("yes/no")]
        //public void TestInvalidPuzzleThrowsException(string invalidPhrase)
        //{
        //    Assert.Throws<ArgumentException>(
        //        () => new Puzzle(invalidPhrase));
        //}

        //[Theory]
        //[InlineData('$')]
        //[InlineData('1')]
        //[InlineData(' ')]
        //public void TestInvalidGuessThrowsException(char invalidGuess)
        //{
        //    Puzzle testPuzzle = new Puzzle("test puzzle");
        //    Assert.Throws<ArgumentException>(
        //        () => testPuzzle.Guess(invalidGuess));
        //}

    }
}
