using UnityEngine;
using System.Collections;

public class ShuffleCards : IShuffleable
{
    public void Shuffle(Card[] cards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            var tmp = cards[i];
            int r = Random.Range(i, cards.Length);
            cards[i] = cards[r];
            cards[r] = tmp;
        }
    }
}
