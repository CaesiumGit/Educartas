using UnityEngine;
using System.Collections;

public interface ICardsCreator
{
    Card[] CreateCards(int numberOfCards);
}
