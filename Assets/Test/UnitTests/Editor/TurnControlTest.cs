using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class TurnControlTest
    {
        [Test]
        public void SwitchPlayers_Test()
        {
            var player1 = new Player(null, null, null);
            var player2 = new Player(null, null, null);

            var turnControl = new TurnControl(player1, player2, null);

            turnControl.SwitchPlayers();

            Assert.AreEqual(turnControl.WinningPlayer, player2);
            Assert.AreEqual(turnControl.LosingPlayer, player1);
        }

        [Test]
        public void TakeCards_FillHandTest()
        {
            var player1 = TestHelper.CreatePlayer(10);
            var player2 = TestHelper.CreatePlayer(10);

            var turnControl = new TurnControl(player1, player2, null);

            var card1 = player1.Deck[0];
            var card2 = player2.Deck[0];

            turnControl.TakeCards();

            Assert.NotNull(player1.HandCard);
            Assert.NotNull(player2.HandCard);

            Assert.AreEqual(card1.Name, player1.HandCard.Name);
            Assert.AreEqual(card2.Name, player2.HandCard.Name);
        }

        #region EnableQuestion tests
        [Test]
        public void EnableQuestion_TurnsWithSameWinnerTest_True()
        {
            var enableQuestion = enableQuestionTest(3, 20, 20);

            Assert.IsTrue(enableQuestion);
        }

        [Test]
        public void EnableQuestion_TurnsWithSameWinnerTest_False()
        {
            var enableQuestion = enableQuestionTest(2, 20, 20);

            Assert.IsFalse(enableQuestion);
        }

        [Test]
        public void EnableQuestion_CardNumberDifferenceTest_True()
        {
            var enableQuestion = enableQuestionTest(2, 20, 5);

            Assert.IsTrue(enableQuestion);
        }

        [Test]
        public void EnableQuestion_CardNumberDifferenceTest_False()
        {
            var enableQuestion = enableQuestionTest(2, 5, 20);

            Assert.IsFalse(enableQuestion);
        }
        #endregion

        [Test]
        public void ChooseCardAttributeTest_Win()
        {

        }

        public void ChooseCardAttributeTest_Lose()
        {

        }

        public void ChooseCardAttributeTest_Draw()
        {

        }

        private bool enableQuestionTest(int turnsWithSameWinner, int winningPlayerDeckCount, int losingPlayerDeckCount)
        {
            var turnControl = new TurnControl(null, null, null);

            return turnControl.EnableQuestion(turnsWithSameWinner, winningPlayerDeckCount, losingPlayerDeckCount);
        }
    }

}