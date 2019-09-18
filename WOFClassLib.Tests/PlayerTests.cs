using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace WOFClassLib.Tests
{
    public class PlayerTests
    {
        [Theory]
        [InlineData("D","DOG", 1)]
        [InlineData("X", "DOG", 0)]
        [InlineData("U", "BLUES CLUES", 2)]
        public void GuessLetter_StringGuessTests(string guess, string puzzleString, int expected)
        {
            var sut = new Player();
            var puzzle = new Puzzle(puzzleString);
            int actual = sut.GuessLetter(guess, puzzle);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("DO")]
        public void GuessLetter_BadStringGuessShouldThrowException(string guess)
        {
            var sut = new Player();
            var puzzle = new Puzzle("DOG");
            Assert.Throws<ArgumentException>(() => sut.GuessLetter(guess, puzzle));
        }

        [Fact]
        public void GuessLetter_PuzzleNullShouldThrowException()
        {
            var sut = new Player();
            Assert.Throws<ArgumentNullException>(() => sut.GuessLetter('D', null));
        }

        [Fact]
        public void GuessLetter_SpinAmountNegativeShouldThrowException()
        {
            var sut = new Player();
            var puzzle = new Puzzle("DOG");
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.GuessLetter('D', puzzle, -1000));
        }

        [Fact]
        public void GuessLetter_GuessCorrectRoundMOneyShouldIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle("DOG");
            int spinAmount = 100;
            
            sut.GuessLetter('D', puzzle, spinAmount); // should match 1 letter
            int numMatches = 1;
            int expected = spinAmount * numMatches;
            int actual = sut.RoundMoney;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GuessLetter_GuessIncorrectRoundMoneyShouldNotIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle("DOG");
            int spinAmount = 100;

            sut.GuessLetter('X', puzzle, spinAmount); // should match 0 letters
            int numMatches = 0;
            int expected = spinAmount * numMatches;
            int actual = sut.RoundMoney;

            Assert.Equal(expected, actual);
        }



        [Fact]
        public void SolvePuzzle_PuzzleNullShouldThrowException()
        {
            var sut = new Player();
            Assert.Throws<ArgumentNullException>(() => sut.SolvePuzzle("DOG", null));
        }

        [Fact]
        public void SolvePuzzle_GuessCorrectTotalMoneyShouldIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle("DOG");

            int spinAmount = 100;
            sut.GuessLetter('D', puzzle, spinAmount);
            int numMatches = 1;

            sut.SolvePuzzle("DOG", puzzle);
            int expected = numMatches * spinAmount;
            int actual = sut.TotalMoney;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void SolvePuzzle_GuessIncorrectTotalMoneyShouldNotIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle("DOG");

            int spinAmount = 100;
            sut.GuessLetter('D', puzzle, spinAmount);

            sut.SolvePuzzle("CAT", puzzle);
            int expected = 0;
            int actual = sut.TotalMoney;

            Assert.Equal(expected, actual);

        }


    }
}
