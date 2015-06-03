using UnityEngine;
using System.Collections;

public interface IQuestionCreator
{
    Question CreateQuestion(Card card);
}
