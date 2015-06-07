using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionCreator : MonoBehaviour, IQuestionCreator
{
    public Text QuestionText;
    public List<Text> Answers;

    public Question CreateQuestion(Card card)
    {
        int attributeToQuestion = 0;//Random.Range(0, 4);
        int indexOfRightAnswer = Random.Range(0, 4);
        setQuestionText(card.Name, attributeToQuestion);
        Question question = new Question(indexOfRightAnswer,
            setChoices(attributeToQuestion, card.Attributes[attributeToQuestion], indexOfRightAnswer),
            indexOfRightAnswer);

        return question;
    }

    private string setQuestionText(string animalName, int attributeIndex)
    {
        string text = string.Format("Qual é {0} do {1}?", getAttributeName(attributeIndex), animalName);
        QuestionText.text = text;
        return text;
    }

    private float[] setChoices(int attributeIndex, float attributeValue, int indexOfRightAnswer)
    {
        var choices = new float[4] { 0, 0, 0, 0 };

        bool isDecimal = attributeValue >= 100 ? attributeValue % 100 != 0 : attributeValue % 10 != 0;

        for (int i = 0; i < Answers.Count; i++)
        {
            var answer = Answers[i];
            if (i == indexOfRightAnswer)
            {
                answer.text = getAttributeByUnit(attributeIndex, attributeValue, isDecimal);
                choices[i] = attributeValue;

            }
            else
            {
                bool numberExists = true;
                float fakeValue = 0;
                while (numberExists)
                {
                    fakeValue = fakeAttributeValue(attributeValue);

                    for (int n = 0; n < choices.Length; n++)
                    {
                        if (fakeValue == choices[n])
                        {
                            numberExists = true;
                            n += 10;
                        }
                        else
                            numberExists = false;
                    }
                }
                answer.text = getAttributeByUnit(attributeIndex, fakeValue, isDecimal);
                choices[i] = fakeValue;
            }


        }

        return choices;
    }

    private float fakeAttributeValue(float attributeValue)
    {
        if (attributeValue == 0)
            attributeValue = Random.Range(1, 5);

        float fakeAttributeValue = attributeValue;

        while (fakeAttributeValue == attributeValue)
        {
            fakeAttributeValue = attributeValue * Random.Range(0.5f, 2.1f);

            if (attributeValue % 1 == 0)
                fakeAttributeValue = Mathf.Round(fakeAttributeValue);
            else
                fakeAttributeValue = (float)System.Math.Round(fakeAttributeValue, 2);
        }

        return fakeAttributeValue;
    }

    private string getAttributeByUnit(int attributeIndex, float attributeValue, bool isDecimal)
    {
        string text = "";
        switch (attributeIndex)
        {
            case 0:
                text = createSizeText(attributeValue, isDecimal);
                break;
            case 1:
                text = attributeValue + " Km/h";
                break;
            case 2:
                text = attributeValue + "";
                break;
            case 3:
                text = attributeValue + " anos";
                break;
        }

        return text;
    }

    private string getAttributeName(int attributeIndex)
    {
        string text = "";
        switch (attributeIndex)
        {
            case 0:
                text = "o tamanho";
                break;
            case 1:
                text = "a velocidade";
                break;
            case 2:
                text = "a ninhada";
                break;
            case 3:
                text = "o tempo de vida";
                break;
        }

        return text;
    }

    private string createSizeText(float size, bool isDecimal)
    {
        float mSize = size / 100;
        string sizeText = "";
        if (size >= 100)
        {
            if (isDecimal)
                sizeText += System.Math.Round(size / 100, 1) + " m";
            else
                sizeText += Mathf.Round(size / 100) + " m";

        }
        else
        {
            if (isDecimal)
                sizeText += size + " cm";
            else
                sizeText += Mathf.Round(size / 10) * 10 + " cm";
        }


        return sizeText;
    }

}
