using UnityEngine;
using System.Collections;

public class CardDatabase
{
    private static AnimalCardData[] _cards;

    public static AnimalCardData[] GetCards()
    {
        if (_cards == null)
            _cards = Resources.LoadAll<AnimalCardData>(@"AnimalCardData");

        return _cards;
    }
}
