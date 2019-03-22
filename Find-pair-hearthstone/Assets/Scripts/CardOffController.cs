using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardOffController : MonoBehaviour
{
    public Transform cardParent;

    private Deck deck;
    private CardMover cardMover;
    private List<GameObject> cardsInCardoff;

    private void Awake()
    {
        cardMover = gameObject.AddComponent<CardMover>();
        deck = gameObject.AddComponent<Deck>();
    }

    public void AddCardsToCardOff(Card[] cards)
    {
        foreach (var item in cards)
        {
            AddCardToCardOff(item);
        }
    }

    public void AddCardToCardOff(Card card)
    {
        card.transform.SetParent(cardParent);
        cardMover.MoveCard(card.transform, card.transform.position, cardParent.position, 1.5f, () => { deck.RecalculateCardsPosition(); });
        cardsInCardoff.Add(card.gameObject);
    }

    public void ResetDeck()
    {
        if (cardsInCardoff != null)
        {
            while (cardsInCardoff.Count > 0)
            {
                GameObject card = cardsInCardoff[0];
                cardsInCardoff.Remove(card);
                Destroy(card);
            }
        }
        cardsInCardoff = new List<GameObject>();
        deck.Initialization(cardsInCardoff);
    }
}
