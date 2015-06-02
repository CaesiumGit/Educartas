using UnityEngine;
using System.Collections;

public enum Results
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

    private QuestionList _questions;

    public Player WinningPlayer { get { return _winningPlayer; } }
    public Player LosingPlayer { get { return _losingPlayer; } }

    public TurnControl(Player winningPlayer, Player losingPlayer, QuestionList questions)
    {
        _winningPlayer = winningPlayer;
        _losingPlayer = losingPlayer;
        _questions = questions;
    }

    #region Interface methods
    public void StartTurn()
    {
        TakeCards();
    }

    public void HandleTurn()
    {
        if (EnableQuestion(_turnsWithSameWinner, _winningPlayer.Deck.Count, _losingPlayer.Deck.Count))
        {
            //TO-DO
        }
        else
        {
            switch (ChooseCardAttribute(_winningPlayer.ChooseAction, _winningPlayer.HandCard, _losingPlayer.HandCard))
            {
                case Results.Lose:
                    SwitchPlayers();    
                    break;
                case Results.Win:
                    break;
                case Results.Draw:
                    break;
            }
        }
    }

    public void EndTurn()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public void SwitchPlayers()
    {
        var tmp = _winningPlayer;
        _winningPlayer = _losingPlayer;
        _losingPlayer = tmp;
    }

    public void TakeCards()
    {
        _winningPlayer.HandCard = _winningPlayer.Deck.TakeCard();
        _losingPlayer.HandCard = _losingPlayer.Deck.TakeCard();
    }

    /// <summary>
    /// Player que tem iniciativa no turno escolhe um atributo e o valor é comparado com o outro player.
    /// </summary>
    public Results ChooseCardAttribute(IChooseAction winningPlayerAction, Card winningPlayerCard, Card losingPlayerCard)
    {
        //TO-DO
        return Results.Win;
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
    public void WinnerTakesAll()
    {
        //TO-DO take all hand cards and draw cards
    }

}
