using UnityEngine;
using System.Collections;

public class Question
{
    public readonly string QuestionText;
    public readonly string[] Choices;
    public readonly int RightChoice;

    public Question(string questionText, string[] choices, int rightChoice)
    {
        QuestionText = questionText;
        Choices = choices;
        RightChoice = rightChoice;
    }
}
