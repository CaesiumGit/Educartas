using UnityEngine;
using System.Collections;

public class ComputerAnswerAction : IAnswerAction
{
    public int MakeAnswer(Question question)
    {
        return Random.Range(0, 4);
    }
}
