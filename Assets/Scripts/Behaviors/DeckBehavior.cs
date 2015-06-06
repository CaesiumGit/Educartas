using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckBehavior : MonoBehaviour
{
    public Vector3 GapBetween;
    private List<GameObject> _cards;

    public int Count { get { return _cards.Count; } }

    public List<GameObject> Cards { get { return _cards; } }
    // Use this for initialization
    void Awake()
    {
        _cards = new List<GameObject>();
    }

    public void AddCard(GameObject card)
    {
        card.transform.parent = transform;
        _cards.Add(card);
        setPosition();
    }

    public GameObject TakeCard()
    {
        _cards[0].transform.parent = null;
        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    private void setPosition()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            var position = transform.position + ((_cards.Count - 1) - i) * GapBetween;
            var rotation = transform.rotation;
            _cards[i].transform.position = position;
            _cards[i].transform.rotation = rotation;
        }
    }
}
