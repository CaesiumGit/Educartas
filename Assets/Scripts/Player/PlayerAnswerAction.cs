using UnityEngine;
using System.Collections;

public class PlayerAnswerAction : MonoBehaviour, IAnswerAction
{
    private int _choice;

    public int MakeAnswer(Question question)
    {
        return _choice;
    }

    public void SetChoice(int choice)
    {
        _choice = choice;
    }
}
