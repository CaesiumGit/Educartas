using UnityEngine;
using System.Collections;
using NUnit.Framework;


namespace UnitTests
{
    [TestFixture]
    public class CardDealerTest
    {
        [Test]
        public void Deal_DeckFilledTest()
        {
            int numberOfCards = 10;

            var cards = TestHelper.CreateCards(numberOfCards);

            var mainDeck = TestHelper.CreateFullDeck(cards);

            var player1Deck = new Deck(numberOfCards);
            var player2Deck = new Deck(numberOfCards);

            var cardDealer = new CardDealer(numberOfCards);

            cardDealer.Deal(player1Deck, mainDeck);
            cardDealer.Deal(player2Deck, mainDeck);

            Assert.AreEqual(numberOfCards / 2, player1Deck.Count);
            Assert.AreEqual(numberOfCards / 2, player2Deck.Count);
        }

        [Test]
        public void Deal_EmptiedDeckTest()
        {
            int numberOfCards = 10;

            var cards = TestHelper.CreateCards(numberOfCards);

            var mainDeck = TestHelper.CreateFullDeck(cards);

            var player1Deck = new Deck(numberOfCards);
            var player2Deck = new Deck(numberOfCards);

            var cardDealer = new CardDealer(numberOfCards);

            cardDealer.Deal(player1Deck, mainDeck);
            cardDealer.Deal(player2Deck, mainDeck);

            Assert.AreEqual(0, mainDeck.Count);
        }
    }


}
