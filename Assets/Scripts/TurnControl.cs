using UnityEngine;
using System.Collections;

public enum ChoiceResult
{
    Win,
    Lose,
    Draw
}

public class TurnControl : ITurnControl
{
    //Jogador que venceu o ultimo turno e tem a iniciativa no turn atual.
    private Player _winningPlayer;
    private Player _losingPlayer;

    //Conta quantas turnos seguidos como o mesmo jogador vencedor.
    private int _turnsWithSameWinner;

    //Deck que armazenará as cartas que ficarem reservadas devido a empate.
    private Deck _drawCards;

    public Player WinningPlayer { get { return _winningPlayer; } }
    public Player LosingPlayer { get { return _losingPlayer; } }

    private bool _draw;
    private IQuestionCreator _questionCreator;

    public TurnControl(Player winningPlayer, Player losingPlayer, IQuestionCreator questionCreator)
    {
        _winningPlayer = winningPlayer;
        _losingPlayer = losingPlayer;

        _drawCards = new Deck(40);
        _questionCreator = questionCreator;
    }

    #region Interface methods
    public void StartTurn()
    {
        TakeCards();
        _draw = false;
        if (EnableQuestion(_turnsWithSameWinner, _winningPlayer.Deck.Count, _losingPlayer.Deck.Count))
        {
            if (AnswerQuestion(_losingPlayer.AnswerAction, _questionCreator.CreateQuestion(_winningPlayer.HandCard)))
            {
                SwitchPlayers();
            }
        }
    }

    public void HandleTurn()
    {
        switch (ChooseCardAttribute(_winningPlayer.ChooseAction, _winningPlayer.HandCard, _losingPlayer.HandCard))
        {
            case ChoiceResult.Lose:
                SwitchPlayers();
                break;
            case ChoiceResult.Win:
                _turnsWithSameWinner++;
                break;
            case ChoiceResult.Draw:
                _drawCards.PlaceCard(_winningPlayer.HandCard);
                _drawCards.PlaceCard(_losingPlayer.HandCard);
                _draw = true;
                break;
        }
    }

    public void EndTurn()
    {
        //Checa se houve empate
        if (!_draw)
            WinnerTakesAll(_drawCards, _losingPlayer.HandCard, _winningPlayer.HandCard, _winningPlayer.Deck);
    }
    #endregion

    public void SwitchPlayers()
    {
        var tmp = _winningPlayer;
        _winningPlayer = _losingPlayer;
        _losingPlayer = tmp;
        _turnsWithSameWinner = 0;
    }

    public void TakeCards()
    {
        _winningPlayer.HandCard = _winningPlayer.Deck.TakeCard();
        _losingPlayer.HandCard = _losingPlayer.Deck.TakeCard();
    }

    /// <summary>
    /// Player que tem iniciativa no turno escolhe um atributo e o valor é comparado com o outro player.
    /// </summary>
    public ChoiceResult ChooseCardAttribute(IChooseAction winningPlayerAction, Card winningPlayerCard, Card losingPlayerCard)
    {
        var result = ChoiceResult.Draw;
        var indexChoose = winningPlayerAction.MakeChoice(winningPlayerCard);

        if (winningPlayerCard.Attributes[indexChoose] > losingPlayerCard.Attributes[indexChoose])
            result = ChoiceResult.Win;

        if (winningPlayerCard.Attributes[indexChoose] < losingPlayerCard.Attributes[indexChoose])
            result = ChoiceResult.Lose;

        //TO-DO
        return result;
    }

    public bool AnswerQuestion(IAnswerAction answerAction, Question question)
    {
        var answer = answerAction.MakeAnswer(question);

        return answer == question.RightChoice;
    }

    public bool EnableQuestion(int turnsWithSameWinner, int winningPlayerDeckCount, int losingPlayerDeckCount)
    {
        if (turnsWithSameWinner >= 3)
            return true;

        if (winningPlayerDeckCount - losingPlayerDeckCount >= 15)
            return true;
        //TO-DO
        return false;
    }

    /// <summary>
    /// Vencedor recolhe a carta do adversario e quaisquer cartas 
    /// </summary>
    public void WinnerTakesAll(Deck drawCards, Card losingPlayerCard, Card winnerPlayerCard, Deck winnerPlayerDeck)
    {
        winnerPlayerDeck.PlaceCard(winnerPlayerCard);
        winnerPlayerDeck.PlaceCard(losingPlayerCard);

        int count = drawCards.Count;
        for (int i = 0; i < count; i++)
        {
            winnerPlayerDeck.PlaceCard(drawCards.TakeCard());
        }

    }

}
