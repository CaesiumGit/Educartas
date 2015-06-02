using UnityEngine;
using System.Collections;

public interface ITurnControl
{
    void StartTurn();
    void HandleTurn();
    void EndTurn();
}
