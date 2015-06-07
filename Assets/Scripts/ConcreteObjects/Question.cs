using UnityEngine;
using System.Collections;

public class Question
{
    public readonly int AttributeIndex;
    public readonly float[] Choices;
    public readonly int RightChoice;

    public Question(int attributeIndex, float[] choices, int rightChoice)
    {
        AttributeIndex = attributeIndex;
        Choices = choices;
        RightChoice = rightChoice;
    }
}
