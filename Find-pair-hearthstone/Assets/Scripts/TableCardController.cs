using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TableCardController : MonoBehaviour
{
    public bool CanCardRotate
    {
        get;
        private set;
    } = true;
    public Transform cardParent;

    private bool isOneCardOpen = false;
    private Card openedCard;
    [Inject]
    private CardOffController cardOffController;
    [Inject]
    private CardStore cardStore;
    private CardMover cardMover;
    [Inject]
    private GameController gameController;
    [Inject]
    private GameConfiguration configuration;
    private Vector2 gridSize = new Vector2(1.2f, 1.2f);

    private bool[,] gameGrid;

    private List<GameObject> cardsOnTable;

    private void Awake()
    {
        cardsOnTable = new List<GameObject>(16);
        cardMover = gameObject.AddComponent<CardMover>();
        gameGrid = new bool[4, 4];
    }

    public void StartGame()
    {
        RequestNewCards(true);
    }

    public void OnCardAction(Card card)
    {
        if (isOneCardOpen)
        {
            if (card == openedCard)
            {
                isOneCardOpen = false;
            }
            else
            {
                CanCardRotate = false;
                StartCoroutine(CompareCards(card, openedCard));
            }
        }
        else
        {
            isOneCardOpen = true;
            openedCard = card;
        }
    }

    private IEnumerator CompareCards(Card card1, Card card2)
    {
        if (card1.ID.Equals(card2.ID))
        {
            yield return new WaitForSeconds(configuration.delayBeforeDiscard);
            DiscardCard(card1);
            DiscardCard(card2);
            yield return new WaitForSeconds(2f);
            RequestNewCards(true);
            if (cardsOnTable.Count == 0)
            {
                gameController.CurrentState = GameController.State.End;
            }
        }
        else
        {
            yield return new WaitForSeconds(configuration.delayBeforeTurnOver);
            card1.Close();
            card2.Close();
        }
        CanCardRotate = true;
        isOneCardOpen = false;
    }

    private void RequestNewCards(bool animationOn)
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (!gameGrid[i, j])
                {
                    emptyPositions.Add(new Vector2Int(i, j));
                }
            }
        }
        GameObject[] cards = cardStore.RequestCards(emptyPositions.Count);

        for (int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i].GetComponent<Card>();
            Vector2Int position = emptyPositions[i];
            gameGrid[position.x, position.y] = true;
            cardsOnTable.Add(card.gameObject);
            cards[i].transform.SetParent(cardParent);
            if (animationOn)
            {
                cardMover.MoveCard(
                    cards[i].transform,
                    cards[i].transform.position,
                    CalculatePosition(position),
                    i * 0.2f,
                    () => { OnCardAddToTable(card, position); }
                    );
            }
            else
            {
                cards[i].transform.position = CalculatePosition(position);
                OnCardAddToTable(card, position);
            }
        }
    }

    private void DiscardCard(Card card)
    {
        gameGrid[card.positionInGrid.x, card.positionInGrid.y] = false;
        card.OnGoOut();
        cardOffController.AddCardToCardOff(card);
        cardsOnTable.Remove(card.gameObject);
    }

    private Vector3 CalculatePosition(Vector2Int position)
    {
        return new Vector3(position.x * gridSize.x - gridSize.x * 1.5f, 0, -position.y * gridSize.y + gridSize.y * 1.5f);
    }

    private void OnCardAddToTable(Card card, Vector2Int position)
    {
        card.OnAddToScene(this, position);
    }
}
