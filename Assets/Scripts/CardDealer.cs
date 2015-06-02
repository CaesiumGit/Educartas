using UnityEngine;
using System.Collections;

public class CardDealer : ICardDealer
{
    private int _numberOfCards;

    /// <summary>
    /// </summary>
    /// <param name="numberOfCards">Número total de cartas no game.</param>
    public CardDealer(int numberOfCards)
    {
        _numberOfCards = numberOfCards;
    }

    public void Deal(Deck deckToReceive, Deck deckToRemove)
    {
        for (int i = 0; i < _numberOfCards / 2; i++)
        {
            var card = deckToRemove.TakeCard();
            deckToReceive.PlaceCard(card);
        }
    }
}
