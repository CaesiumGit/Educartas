using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum TurnState
{
    TakeCards,
    CheckForQuestion,
    ShowQuestion,
    ShowOptions,
    ShowPlayer2Choice,
    ShowCards,
    Player1Wins,
    Player2Wins,
    Draws,
    Player1TakeDrawCards,
    Player2TakeDrawCards,
    EndTurn
}

[Serializable]
public struct PlayerInfo
{
    public DeckBehavior DeckBehavior;
    public Transform Hand;
    public GameObject HandCard;
    public IMoveable MoveAction;
}

public class GameBehavior : MonoBehaviour, ITurnState
{
    public PlayerInfo Player1;
    public PlayerInfo Player2;
    public float CardVelocity;
    public GameObject OptionsMenu;
    public GameObject ShowChoicePanel;
    public DeckBehavior DrawDeck;
    public GameObject DefaultCard;
    public GameObject InfoText;

    private GameControl _gameControl;
    private TurnState _state;
    private bool _active;
    private Job _job;
    private float _timePassed;
    private List<Transform> _drawCards;

    #region MonoBehaviors methods
    // Use this for initialization
    void Start()
    {
        setupGameControl();
        setupPlayerObjects();
        //DebugPlayer();
        _job = new Job(_gameControl.RunGame(this));
        //_job.Start();
        _drawCards = new List<Transform>();

        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
        //DrawDeck.AddCard(Instantiate(DefaultCard, Vector3.zero, Quaternion.identity) as GameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_active)
        {
            _active = true;
            StartCoroutine(_gameControl.RunGame(this));
        }

        if (!_active)
            return;

        handleState();
    }

    void LateUpdate()
    {
        _gameControl.LateUpdate();
    }
    #endregion

    private bool rotatePlayer2CardTo(float angleY)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(60, angleY, 0));
        Player2.HandCard.transform.localRotation = Quaternion.Lerp(Player2.HandCard.transform.localRotation, targetRotation, 2 * Time.deltaTime);

        return Player2.HandCard.transform.localEulerAngles.y < angleY - 2;
    }

    private bool moveCards(Transform card1Target, Transform card2Target)
    {
        bool player1Finished = Player1.MoveAction.MoveTo(Player1.HandCard.transform, card1Target, CardVelocity);
        bool player2Finished = Player2.MoveAction.MoveTo(Player2.HandCard.transform, card2Target, CardVelocity);
        if (!player1Finished && !player2Finished)
        {
            return false;
        }

        return true;
    }

    private bool moveDrawCards(Transform targetObject)
    {
        float distance = Vector3.Distance(targetObject.position, _drawCards[_drawCards.Count - 1].position);
        if (distance > 0.01f)
        {
            foreach (Transform card in _drawCards)
            {
                var moveableObject = card;
                moveableObject.position = Vector3.Lerp(moveableObject.position, targetObject.position, Time.deltaTime * CardVelocity);
                moveableObject.localRotation = Quaternion.Lerp(moveableObject.localRotation, targetObject.localRotation, Time.deltaTime * CardVelocity * 1.2f);
            }
            return true;
        }
        return false;
    }

    private void takeDrawCards()
    {
        _drawCards.Clear();
        foreach (Transform card in DrawDeck.transform)
        {
            _drawCards.Add(DrawDeck.TakeCard().transform);
        }
    }
    private void handleState()
    {
        switch (_state)
        {
            case TurnState.TakeCards:
                if (!moveCards(Player1.Hand, Player2.Hand))
                {
                    _gameControl.NextStep();

                }
                break;
            case TurnState.ShowCards:
                if (!rotatePlayer2CardTo(360))
                {
                    InfoText.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Space) && InfoText.activeInHierarchy == true)
                {
                    InfoText.SetActive(false);
                    _gameControl.NextStep();
                }

                break;
            case TurnState.Player1Wins:
                if (!moveCards(Player1.DeckBehavior.transform, Player1.DeckBehavior.transform))
                {
                    Player1.DeckBehavior.AddCard(Player1.HandCard);
                    Player1.DeckBehavior.AddCard(Player2.HandCard);
                    _gameControl.NextStep();
                }
                break;
            case TurnState.Player2Wins:
                if (!moveCards(Player2.DeckBehavior.transform, Player2.DeckBehavior.transform))
                {
                    Player2.DeckBehavior.AddCard(Player2.HandCard);
                    Player2.DeckBehavior.AddCard(Player1.HandCard);
                    _gameControl.NextStep();
                }
                break;
            case TurnState.ShowPlayer2Choice:
                wait(2);
                break;
            case TurnState.Draws:
                if (!moveCards(DrawDeck.transform, DrawDeck.transform))
                {
                    DrawDeck.AddCard(Player2.HandCard);
                    DrawDeck.AddCard(Player1.HandCard);
                }
                break;

            case TurnState.Player1TakeDrawCards:
                if (!moveDrawCards(Player1.DeckBehavior.transform))
                {
                    foreach (Transform card in _drawCards)
                    {
                        Player1.DeckBehavior.AddCard(card.gameObject);
                    }
                }
                break;
            case TurnState.Player2TakeDrawCards:
                if (!moveDrawCards(Player2.DeckBehavior.transform))
                {
                    foreach (Transform card in _drawCards)
                    {
                        Player2.DeckBehavior.AddCard(card.gameObject);
                    }
                }
                break;
            case TurnState.EndTurn:
                wait(1);
                //_timePassed = 0;
                break;
        }
    }

    private void enterState(TurnState state)
    {
        switch (state)
        {
            case TurnState.ShowCards:
                _timePassed = 0;
                break;
            case TurnState.TakeCards:
                Player1.HandCard = Player1.DeckBehavior.TakeCard();
                Player2.HandCard = Player2.DeckBehavior.TakeCard();
                break;
            case TurnState.ShowOptions:
                OptionsMenu.SetActive(true);
                break;
            case TurnState.ShowPlayer2Choice:
                ShowChoicePanel.SetActive(true);
                _timePassed = 0;
                break;
            case TurnState.Player1TakeDrawCards:
                takeDrawCards();
                break;
            case TurnState.Player2TakeDrawCards:
                takeDrawCards();
                break;
            case TurnState.EndTurn:
                //wait(1);
                _timePassed = 0;
                break;
        }
    }

    private void wait(float time)
    {
        _timePassed += Time.deltaTime;
        if (_timePassed >= time)
        {
            _gameControl.NextStep();
        }
    }

    private void exitState(TurnState state)
    {
        switch (state)
        {
            case TurnState.ShowOptions:
                OptionsMenu.SetActive(false);
                break;
            case TurnState.ShowPlayer2Choice:
                ShowChoicePanel.SetActive(false);
                break;
            case TurnState.Player1Wins:
                break;
            case TurnState.Player2Wins:
                break;
        }
    }

    //Será chamado pelo evento da gui da unity assim que alguma opção for selecionada
    public void OptionChoose()
    {
        _gameControl.NextStep();
    }

    #region Setup methods
    private void setupGameControl()
    {
        _gameControl = new GameControl(40);
        _gameControl.SetupPlayersDeck(new CardsCreator(), new ShuffleCards(), new CardDealer(40));
        _gameControl.SetupPlayers(GetComponent<PlayerChooseAction>(), new PlayerAnswerAction(), GetComponent<ComputerChooseAction>(), new PlayerAnswerAction());
        _gameControl.SetupTurnControl(new TurnControl(_gameControl.Player2, _gameControl.Player1), new QuestionCreator());
    }

    private void setupPlayerObjects()
    {
        var player1deck = _gameControl.Player1.Deck;
        var player2deck = _gameControl.Player2.Deck;

        fillDeckObject(player1deck, Player1.DeckBehavior);
        fillDeckObject(player2deck, Player2.DeckBehavior);

        Player1.MoveAction = new MovePlayerCard();
        Player2.MoveAction = new MovePlayerCard();

        //DebugPlayer();
    }
    #endregion

    #region Helper methods
    private void fillDeckObject(Deck playerDeck, DeckBehavior playerDeckBehavior)
    {
        for (int i = 0; i < playerDeck.Count; i++)
        {
            var cardObject = GameObject.Find(playerDeck[i].Name);
            playerDeckBehavior.AddCard(cardObject);
        }
    }

    private void DebugPlayer()
    {
        var player1deck = _gameControl.Player1.Deck;
        var player2deck = _gameControl.Player2.Deck;

        for (int i = 0; i < player2deck.Count; i++)
        {
            Debug.Log(player2deck[i].Name);
            Debug.Log(Player2.DeckBehavior.Cards[i]);
        }
    }
    #endregion

    // Update is called once per frame

    public void ChangeState(TurnState newState)
    {
        exitState(_state);
        enterState(newState);
        _state = newState;
    }
}
