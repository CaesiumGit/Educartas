using UnityEngine;
using System.Collections;

public class GameControl
{
    private int _numberOfCards;
    private string _cardsPath;

    private Player _player1;
    private Player _player2;

    private ITurnControl _turnControl;

    public GameControl(int numberOfCards, string cardsPath, ITurnControl turnControl)
    {
        _numberOfCards = numberOfCards;
        _cardsPath = cardsPath;
        _turnControl = turnControl;
    }

    public void SetupPlayers(ICardsCreator cardsCreator, IShuffleable shuffleCards, ICardDealer cardDealer)
    {
        var cards = cardsCreator.CreateCards(_numberOfCards);

        shuffleCards.Shuffle(cards);

        var mainDeck = new Deck(_numberOfCards);

        for (int i = 0; i < cards.Length; i++)
        {
            mainDeck.PlaceCard(cards[i]);
        }

        var player1Deck = new Deck(_numberOfCards);
        var player2Deck = new Deck(_numberOfCards);

        cardDealer.Deal(player1Deck, mainDeck);
        cardDealer.Deal(player2Deck, mainDeck);

        _player1 = new Player(player1Deck, new PlayerChooseAction(), new PlayerAnswerAction());
        _player2 = new Player(player2Deck, new PlayerChooseAction(), new PlayerAnswerAction());
    }
}
