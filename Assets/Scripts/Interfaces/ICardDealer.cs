using UnityEngine;
using System.Collections;

public interface ICardDealer
{
    /// <summary>
    /// Remove cartas de um deck e insere em outro usando um determinado padrão.
    /// </summary>
    /// <param name="deckToReceive">Deck quer irá recber as cartas.</param>
    /// <param name="deckToRemove">Deck que terá as cartas removidas.</param>
    void Deal(Deck deckToReceive, Deck deckToRemove);
}
