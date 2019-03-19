using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCardController : MonoBehaviour
{
    public bool CanCardRotate
    {
        get;
        private set;
    } = true;

    private bool isOneCardOpen = false;
    private Card openedCard;
    [SerializeField]
    private CardOffController cardOffController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            yield return new WaitForSeconds(1f);
            cardOffController.AddCardsToCardOff(new Card[] { card1, card2 });
        }
        else
        {
            yield return new WaitForSeconds(2f);
            card1.Close();
            card2.Close();
        }
        CanCardRotate = true;
        isOneCardOpen = false;
    }
}
