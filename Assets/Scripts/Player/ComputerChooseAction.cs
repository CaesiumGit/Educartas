using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComputerChooseAction : MonoBehaviour, IChooseAction
{
    public Text Player2ChoiceText;

    private void changeText(int choice)
    {
        string attribute = "";
        switch (choice)
        {
            case 0:
                attribute = "Tamanho";
                break;
            case 1:
                attribute = "Velocidade";
                break;
            case 2:
                attribute = "Número de filhotes";
                break;
            case 3:
                attribute = "Tempo de vida";
                break;

        }
        Player2ChoiceText.text = "Computador escolheu " + attribute;
    }

    public int MakeChoice(Card card)
    {
        int choice = Random.Range(0, 4);
        changeText(choice);
        return choice;
    }
}
