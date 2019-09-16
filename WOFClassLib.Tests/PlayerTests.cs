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

        [Fact]
        public void GuessLetter_PuzzleNullShouldThrowError()
        {
            var sut = new Player();
            Assert.Throws<ArgumentNullException>(() => sut.GuessLetter('D', null));
        }

        [Fact]
        public void GuessLetter_SpinAmountNegativeShouldThrowError()
        {
            var sut = new Player();
            var puzzle = new Puzzle();
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.GuessLetter('D', puzzle, -1000));
        }

        [Fact]
        public void GuessLetter_GuessCorrectRoundMOneyShouldIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle();
            int spinAmount = 100;
            
            // TODO: set the puzzle to "DOG"
            sut.GuessLetter('D', puzzle, spinAmount);
            int numMatches = 1;
            int expected = spinAmount * numMatches;
            int actual = sut.RoundMoney;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GuessLetter_GuessIncorrectRoundMOneyShouldNotIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle();
            int spinAmount = 100;

            // TODO: set the puzzle to "DOG"
            sut.GuessLetter('X', puzzle, spinAmount);
            int numMatches = 0;
            int expected = spinAmount * numMatches;
            int actual = sut.RoundMoney;

            Assert.Equal(expected, actual);
        }



        [Fact]
        public void SolvePuzzle_PuzzleNullShouldThrowError()
        {
            var sut = new Player();
            Assert.Throws<ArgumentNullException>(() => sut.SolvePuzzle("DOG", null));
        }

        [Fact]
        public void SolvePuzzle_GuessCorrectTotalMoneyShouldIncrease()
        {
            var sut = new Player();
            var puzzle = new Puzzle();

            // TODO: set puzzle equal to DOG
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
            var puzzle = new Puzzle();

            // TODO: set puzzle equal to DOG
            int spinAmount = 100;
            sut.GuessLetter('D', puzzle, spinAmount);

            sut.SolvePuzzle("CAT", puzzle);
            int expected = 0;
            int actual = sut.TotalMoney;

            Assert.Equal(expected, actual);

        }


    }
}
