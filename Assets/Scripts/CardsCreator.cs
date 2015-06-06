using UnityEngine;
using System.Collections;

public class CardsCreator : ICardsCreator
{
    public Card[] CreateCards(int numberOfCards)
    {
        var cards = new Card[numberOfCards];
        var cardsData = CardDatabase.GetCards();

        for (int i = 0; i < cards.Length; i++)
        {
            var cardData = cardsData[i];
            var name = cardsData[i].ScientificName;
            var attributes = new float[4];

            attributes[0] = cardData.Size;
            attributes[1] = cardData.Speed;
            attributes[2] = cardData.LitterNumber;
            attributes[3] = cardData.LifeSpan;

            cards[i] = new Card(name, attributes);
        }

        return cards;
    }
}
