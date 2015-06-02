using UnityEngine;
using System.Collections;

public class Deck
{
    private int _count;
    private Card[] _cards;

    public int Count { get { return _count; } }

    public Card this[int index]
    {
        get { return _cards[index]; }
    }

    public Deck(int cardCapacity)
    {
        _cards = new Card[cardCapacity];
    }

    public void PlaceCard(Card card)
    {
        _cards[_count] = card;
        _count++;
    }

    public Card TakeCard()
    {
        _count--;
        var card = _cards[0];
        for (int i = 0; i < _count; i++)
        {
            _cards[i] = _cards[i + 1];
        }
        _cards[_count] = null;
        return card;
    }

    public void Shuffle(IShuffleable shuffleObject)
    {
        shuffleObject.Shuffle(_cards);
    }
}
