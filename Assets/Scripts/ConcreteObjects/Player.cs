using UnityEngine;
using System.Collections;

public class Player
{
    private IChooseAction _chooseAction;
    private IAnswerAction _answerAction;
    private Deck _deck;
    private Card _handCard;

    public Card HandCard { get { return _handCard; } set { _handCard = value; } }

    public Deck Deck { get { return _deck; } }

    public IChooseAction ChooseAction { get { return _chooseAction; } }
    public IAnswerAction AnswerAction { get { return _answerAction; } }

    public Player(Deck deck, IChooseAction chooseAction, IAnswerAction answerAction)
    {
        _deck = deck;
        _chooseAction = chooseAction;
        _answerAction = answerAction;
    }

}
