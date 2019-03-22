using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardStore : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public Transform cardParent;

    private Deck deck;
    private List<GameObject> cardsInStore;


    private void Awake()
    {
        if (cardPrefabs.Length != 15)
        {
            Debug.LogError("Card prefabs is not 15");
            return;
        }
        deck = gameObject.AddComponent<Deck>();
    }

    public void ResetDeck()
    {
        if (cardsInStore != null)
        {
            while (cardsInStore.Count > 0)
            {
                GameObject card = cardsInStore[0];
                cardsInStore.Remove(card);
                Destroy(card);
            }
        }
        cardsInStore = new List<GameObject>(30);
        for (int i = 0; i < 15; i++)
        {
            cardsInStore.Add(AddCardToStoreFromPrefab(cardPrefabs[i]));
            cardsInStore.Add(AddCardToStoreFromPrefab(cardPrefabs[i]));
        }
        deck.Initialization(cardsInStore);
        deck.MixCards();
        deck.RecalculateCardsPosition();
    }

    public GameObject[] RequestCards(int count = 2)
    {
        if (count > cardsInStore.Count)
        {
            count = cardsInStore.Count;
        }
        GameObject[] result = cardsInStore.GetRange(cardsInStore.Count - count, count).ToArray();
        cardsInStore.RemoveRange(cardsInStore.Count - count, count);
        deck.RecalculateCardsPosition();
        return result;
    }

    private GameObject AddCardToStoreFromPrefab(GameObject prefab)
    {
        GameObject newCard = Instantiate(prefab, cardParent);
        return newCard;
    }
}
