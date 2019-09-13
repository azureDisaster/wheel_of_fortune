using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WOFClassLib.Tests
{
    public class PlayerTests
    {
        [Theory]
        [InlineData("DOG", 'A', 0)]
        [InlineData("DOG", 'G', 1)]
        [InlineData("BLUES CLUES",'L', 2)]
        public void GuessLetter_ReturnValueTests(string puzzleString, char guessLetter, int expectedCount)
        {
            var puzzle = new Puzzle(puzzleString);
            var player = new Player();

            int actualCount = player.GuessLetter(guessLetter, puzzle);

            Assert.Equal(expectedCount, actualCount);

        }

        [Theory]
        [InlineData("DOG", 'A', 100, 0)]
        [InlineData("DOG", 'G', 100, 100)]
        [InlineData("BLUES CLUES", 'L', 100, 200)]
        public void GuessLetter_RoundMoneyTests(string puzzleString, char guessLetter, int spinAmount, int expected)
        {
            var puzzle = new Puzzle(puzzleString);
            var player = new Player();

            player.GuessLetter(guessLetter, puzzle, spinAmount);
            int actual = player.RoundMoney;

            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("DOG","CAT", false)]
        [InlineData("DOG", "DOG", true)]
        public void SolvePuzzle_ReturnValueTests(string puzzleString, string guess, bool expected)
        {
            var puzzle = new Puzzle(puzzleString);
            var player = new Player();

            bool actual = player.SolvePuzzle(guess, puzzle);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SolvePuzzle_PuzzleSolvedTotalShouldUpdate()
        {
            var puzzle = new Puzzle("DOG");
            var player = new Player();

            int expected = 100;
            player.GuessLetter('D', puzzle, expected);
            player.SolvePuzzle("DOG", puzzle);

            int actual = player.TotalMoney;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SolvePuzzle_PuzzleNotSolvedTotalShouldNotUpdate()
        {
            var puzzle = new Puzzle("DOG");
            var player = new Player();

            int expected = 0;
            player.GuessLetter('D', puzzle, 100);
            player.SolvePuzzle("CAT", puzzle);

            int actual = player.TotalMoney;

            Assert.Equal(expected, actual);
        }

    }
}
