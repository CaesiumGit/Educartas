using UnityEngine;
using System.Collections;

public class PlayerChooseAction : MonoBehaviour, IChooseAction
{
    private int _choise;

    public int MakeChoice(Card card)
    {
        return _choise;
    }

    public void SetOption(int option)
    {
        _choise = option;
    }
}
