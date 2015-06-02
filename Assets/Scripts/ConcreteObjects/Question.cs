using UnityEngine;
using System.Collections;

public class Question
{
    public readonly string QuestionText;
    public readonly string[] Choises;
    public readonly int RightChoice;
    /// <summary>
    /// Nome da carta a qual a pergunta está relacionada.
    /// </summary>
    public readonly string CardName;
}
