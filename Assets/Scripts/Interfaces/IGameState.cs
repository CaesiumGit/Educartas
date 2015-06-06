using UnityEngine;
using System.Collections;
using System;

public interface ITurnState
{
    void ChangeState(TurnState newState);
}
