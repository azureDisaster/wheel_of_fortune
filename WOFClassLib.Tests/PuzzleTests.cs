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

            Puzzle testPuzzle2 = new Puzzle("Dogs and Cats");
            string testDisplay2 = "---- --- ----";
            string puzzleDisplay2 = testPuzzle2.GetPuzzleDisplay();
            Assert.Equal(testDisplay2, puzzleDisplay2);
        }

        [Theory]
        [InlineData("dog", new char[] { 'd' }, "D--")]
        [InlineData("Dog", new char[] { 'o' }, "-O-")]
        [InlineData("DOG", new char[] { 'g', 'o' }, "-OG")]
        [InlineData("dog", new char[] { 'g', 'g' }, "--G")]
        [InlineData("Dogs and Cats", new char[] { 'C', 'a', 'S', 'd' }, "D--S A-D CA-S")]
        [InlineData("LEAP", new char[] { 'x', 'p' }, "---P")]
        public void TestGuessCharUpdatesPuzzle(string phrase, char[] guesses, string expectedResultDisplay)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            foreach (char guess in guesses)
            {
                testPuzzle.Guess(guess);
            }
            Assert.Equal(expectedResultDisplay, testPuzzle.GetPuzzleDisplay());
        }

        [Theory]
        [InlineData("QUEST", 'z', 0)]
        [InlineData("Wolf", 'w', 1)]
        [InlineData("Little Red Riding Hood", 'r', 2)]
        public void TestGuessCharReturnsCorrectNumberOfMatches(string phrase, char guess, int expectedNumMatches)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            int numberOfMatches = testPuzzle.Guess(guess);
            Assert.Equal(expectedNumMatches, numberOfMatches);
        }

        [Theory]
        [InlineData("Microsoft Azure", "MICROSOFT AZURE", true)]
        [InlineData("AN APPLE A DAY", "An Apple A Day", true)]
        [InlineData("banana", "banana", true)]
        [InlineData("dogs", "cats", false)]
        [InlineData("United States of America", "UNITED STATES", false)]
        [InlineData("tissue", "napkins", false)]
        public void TestSolvePuzzle(string phrase, string guess, bool expectedResult)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            if (expectedResult == true)
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

        [Theory]
        [InlineData('$')]
        [InlineData('1')]
        [InlineData(' ')]
        public void TestInvalidGuessThrowsException(char invalidGuess)
        {
            Puzzle testPuzzle = new Puzzle("test puzzle");
            Assert.Throws<ArgumentException>(
                () => testPuzzle.Guess(invalidGuess));
        }

        [Theory]
        [InlineData("dog", new char[] { 'd', 'o', 'g' }, true)]
        [InlineData("dog", new char[] { 'x', 'o' }, false)]
        public void TestPuzzleIsSolved(string phrase, char[] guesses, bool expectedResult)
        {
            Puzzle testPuzzle = new Puzzle(phrase);
            foreach(char guess in guesses)
            {
                testPuzzle.Guess(guess);
            }

            if (expectedResult == true)
            {
                Assert.True(testPuzzle.IsSolved());
            }
            else
            {
                Assert.False(testPuzzle.IsSolved());
            }
        }
    }
}
