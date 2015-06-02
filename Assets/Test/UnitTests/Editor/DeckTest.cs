using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace UnitTests
{
    public class TestHelper
    {
        #region Deck related methods
        public static Card CreateCard(string name)
        {
            var card = new Card(name, new float[1]);

            return card;
        }

        public static Card[] CreateCards(int numberOfCards)
        {
            var cards = new Card[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
            {
                var card = CreateCard("card" + (i));

                cards[i] = card;
            }

            return cards;
        }

        public static Deck CreateFullDeck(Card[] cards)
        {
            Deck deck = new Deck(cards.Length);


            for (int i = 0; i < cards.Length; i++)
            {
                deck.PlaceCard(cards[i]);
            }

            return deck;
        }
        #endregion

        public static Player CreatePlayer(int numberOfCards)
        {
            var cards = CreateCards(numberOfCards);
            var deck = CreateFullDeck(cards);
            var player = new Player(deck, null, null);

            return player;
        }
    }


    [TestFixture]
    public class DeckTest
    {
        [Test]
        public void PlaceCard_RightCountTest()
        {
            int numberOfCards = 10;

            var cards = TestHelper.CreateCards(numberOfCards);

            var deck = TestHelper.CreateFullDeck(cards);

            Assert.AreEqual(numberOfCards, deck.Count);
        }

        [Test]
        public void PlaceCard_FillTest()
        {
            int numberOfCards = 10;

            var cards = TestHelper.CreateCards(numberOfCards);

            var deck = TestHelper.CreateFullDeck(cards);

            for (int i = 0; i < numberOfCards; i++)
            {
                Assert.NotNull(deck[0], "Index {0} is null", i);
                Assert.AreEqual(cards[i].Name, deck[i].Name);
            }
        }

        [Test]
        public void TakeCard_RightCountTest()
        {
            int numberOfCards = 10;
            int numberToRemove = 3;


            var cards = TestHelper.CreateCards(numberOfCards);

            var deck = TestHelper.CreateFullDeck(cards);

            for (int i = 0; i < numberToRemove; i++)
            {
                deck.TakeCard();
            }

            Assert.AreEqual(numberOfCards - numberToRemove, deck.Count);
        }

        [Test]
        public void TakeCard_Test()
        {
            int numberOfCards = 10;
            int numberToRemove = 3;

            var cards = TestHelper.CreateCards(numberOfCards);
            var removedCards = new Card[numberToRemove];

            var deck = TestHelper.CreateFullDeck(cards);

            for (int i = 0; i < numberToRemove; i++)
            {
                removedCards[i] = deck.TakeCard();
            }

            for (int i = 0; i < numberToRemove; i++)
            {
                Assert.NotNull(removedCards[i]);
                Assert.AreEqual(cards[i].Name, removedCards[i].Name);
            }
        }


        [Test]
        public void Shuffle_DeckTest()
        {
            int numberOfCards = 10;

            var cards = TestHelper.CreateCards(numberOfCards);

            var deck = TestHelper.CreateFullDeck(cards);

            var shuffleObject = new ShuffleCards();

            deck.Shuffle(shuffleObject);

            int equalCardNumber = 0;

            for (int i = 0; i < numberOfCards; i++)
            {
                if (cards[(numberOfCards - i) - 1].Name == deck[i].Name)
                {
                    equalCardNumber++;
                }
            }

            Assert.False(equalCardNumber >= numberOfCards / 2);
        }

    }
}
