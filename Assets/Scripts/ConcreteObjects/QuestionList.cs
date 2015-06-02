using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionList
{
    private Dictionary<string, List<Question>> _questionsByCardName;
    private List<Question> _questionList;

    public Question this[int index]
    {
        get { return _questionList[index]; }
    }

    public List<Question> this[string cardName]
    {
        get { return _questionsByCardName[cardName]; }
    }

    public QuestionList()
    {
        _questionsByCardName = new Dictionary<string, List<Question>>();
        _questionList = new List<Question>();
    }

    public void AddQuestion(Question question)
    {
        if (!_questionsByCardName.ContainsKey(question.CardName))
        {
            _questionsByCardName[question.CardName] = new List<Question>();
        }
        if (!_questionsByCardName[question.CardName].Contains(question))
        {
            _questionsByCardName[question.CardName].Add(question);
            _questionList.Add(question);
        }
        else
            Debug.LogError(string.Format("A pergunta {0} já existe na lista.", question.QuestionText));
    }
}
