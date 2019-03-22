using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<GameObject> cardsInStore;
    [SerializeField]
    private float cardHeight = 0.05f;
    [SerializeField]
    private int maxDeckHeight = 5;

    public void Initialization(List<GameObject> cardsInStore)
    {
        this.cardsInStore = cardsInStore;
    }

    public void RecalculateCardsPosition()
    {
        int cardsCount = cardsInStore.Count;
        if (cardsCount <= 0) return;

        float oneCardHeight = cardHeight;
        if (cardsCount > maxDeckHeight)
        {
            oneCardHeight *= (float)maxDeckHeight / cardsInStore.Count;
        }
        for (int i = 0; i < cardsCount; i++)
        {
            cardsInStore[i].transform.localPosition = new Vector3(0, oneCardHeight * i, 0);
        }
    }

    public void MixCards()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject tempGO = cardsInStore[i];
            int randomIndex = Random.Range(0, 30);
            cardsInStore[i] = cardsInStore[randomIndex];
            cardsInStore[randomIndex] = tempGO;
        }
    }
}
