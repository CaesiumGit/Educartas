using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

public class GameControl
{
    private int _numberOfCards;

    private Player _player1;
    private Player _player2;
    private Deck _player1Deck;
    private Deck _player2Deck;

    public Player Player1 { get { return _player1; } }
    public Player Player2 { get { return _player2; } }

    private TurnControl _turnControl;
    private bool _nextStep;
    private bool _draw;
    private int _turnsWithSameWinner;
    private IQuestionCreator _questionCreator;

    public GameControl(int numberOfCards)
    {
        _numberOfCards = numberOfCards;
    }

    #region Setup methods
    public void SetupPlayersDeck(ICardsCreator cardsCreator, IShuffleable shuffleCards, ICardDealer cardDealer)
    {
        var cards = cardsCreator.CreateCards(_numberOfCards);

        shuffleCards.Shuffle(cards);

        var mainDeck = new Deck(_numberOfCards);

        for (int i = 0; i < cards.Length; i++)
        {
            mainDeck.PlaceCard(cards[i]);
        }

        _player1Deck = new Deck(_numberOfCards);
        _player2Deck = new Deck(_numberOfCards);

        cardDealer.Deal(_player1Deck, mainDeck);
        cardDealer.Deal(_player2Deck, mainDeck);


    }

    public void SetupPlayers(IChooseAction player1ChooseAction, IAnswerAction player1AnswerQuestion,
        IChooseAction player2ChooseAction, IAnswerAction player2AnswerQuestion)
    {
        _player1 = new Player(_player1Deck, player1ChooseAction, player1AnswerQuestion);
        _player2 = new Player(_player2Deck, player2ChooseAction, player2AnswerQuestion);

    }

    public void SetupTurnControl(TurnControl turnControl, IQuestionCreator questionCreator)
    {
        _turnControl = turnControl;
        _questionCreator = questionCreator;
        //_turnControl.DrawCards.PlaceCard(new Card(null, null));
    }
    #endregion

    public static void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
        var type = assembly.GetType("UnityEditorInternal.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public IEnumerator RunGame(ITurnState state)
    {
        while (true)
        {
            ClearLog();
            _draw = false;
            _turnControl.TakeCards();
            state.ChangeState(TurnState.TakeCards);
            #region Stop
            while (!_nextStep)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            #endregion
            bool answer = false;
            if (_turnControl.EnableQuestion(_turnsWithSameWinner, _turnControl.WinningPlayer.Deck.Count, _turnControl.LosingPlayer.Deck.Count))
            {
                var question = _questionCreator.CreateQuestion(_turnControl.WinningPlayer.HandCard);
                if (_turnControl.WinningPlayer == _player2)
                {
                    state.ChangeState(TurnState.ShowQuestion);
                    answer = _turnControl.AnswerQuestion(_turnControl.LosingPlayer.AnswerAction, question);
                }
                else
                {
                    state.ChangeState(TurnState.ShowQuestionToComputer);
                    #region Stop
                    while (!_nextStep)
                    {
                        yield return null;
                    }
                    yield return new WaitForEndOfFrame();
                    #endregion
                    answer = _turnControl.AnswerQuestion(_turnControl.LosingPlayer.AnswerAction, question);
                    if (!answer)
                        state.ChangeState(TurnState.ComputerWrong);
                    else
                        state.ChangeState(TurnState.ComputerRight);
                }
                #region Stop
                while (!_nextStep)
                {
                    yield return null;
                }
                yield return new WaitForEndOfFrame();
                #endregion
                handleQuestion(answer);
            }

            ChoiceResult choiceResult = ChoiceResult.Draw;
            if (_turnControl.WinningPlayer == _player1)
            {
                state.ChangeState(TurnState.ShowOptions);
                #region Stop
                while (!_nextStep)
                {
                    yield return null;
                }
                yield return new WaitForEndOfFrame();
                #endregion
                choiceResult = _turnControl.ChooseCardAttribute(_turnControl.WinningPlayer.ChooseAction, _turnControl.WinningPlayer.HandCard, _turnControl.LosingPlayer.HandCard);
            }
            else
            {
                choiceResult = _turnControl.ChooseCardAttribute(_turnControl.WinningPlayer.ChooseAction, _turnControl.WinningPlayer.HandCard, _turnControl.LosingPlayer.HandCard);
                state.ChangeState(TurnState.ShowPlayer2Choice);
                #region Stop
                while (!_nextStep)
                {
                    yield return null;
                }
                yield return new WaitForEndOfFrame();
                #endregion
            }
            state.ChangeState(TurnState.ShowCards);
            //Debug
            //
            #region Stop
            while (!_nextStep)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            #endregion
            handleChoice(state, choiceResult);
            state.ChangeState(winnerState());
            #region Stop
            while (!_nextStep)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            #endregion
            state.ChangeState(TurnState.EndTurn);
            #region Stop
            while (!_nextStep)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            #endregion
            if (!_draw)
            {
                if (_turnControl.DrawCards.Count > 0)
                {
                    if (_turnControl.WinningPlayer == _player1)
                    {
                        state.ChangeState(TurnState.Player1TakeDrawCards);
                    }
                    else
                    {
                        state.ChangeState(TurnState.Player2TakeDrawCards);
                    }
                }
                _turnControl.WinnerTakesAll(_turnControl.DrawCards, _turnControl.LosingPlayer.HandCard, _turnControl.WinningPlayer.HandCard, _turnControl.WinningPlayer.Deck);
            }
        }
    }

    private void handleQuestion(bool rightAnswer)
    {
        if (rightAnswer)
        {
            _turnControl.SwitchPlayers(out _turnsWithSameWinner);
        }
    }

    private void handleChoice(ITurnState state, ChoiceResult result)
    {
        switch (result)
        {
            case ChoiceResult.Lose:
                _turnControl.SwitchPlayers(out _turnsWithSameWinner);
                break;
            case ChoiceResult.Win:
                _turnsWithSameWinner++;
                break;
            case ChoiceResult.Draw:
                _turnControl.DrawCards.PlaceCard(_turnControl.WinningPlayer.HandCard);
                _turnControl.DrawCards.PlaceCard(_turnControl.LosingPlayer.HandCard);
                _draw = true;
                break;
        }
        Debug.Log(result);
    }

    public TurnState winnerState()
    {
        if (_draw)
            return TurnState.Draws;

        if (_turnControl.WinningPlayer == _player1)
            return TurnState.Player1Wins;

        return TurnState.Player2Wins;
    }

    public void NextStep()
    {
        _nextStep = true;
    }

    public void LateUpdate()
    {
        _nextStep = false;
    }
}
