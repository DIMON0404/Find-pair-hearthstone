using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOffController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCardsToCardOff(Card[] cards)
    {
        foreach (var item in cards)
        {
            item.OnGoOut();
        }
    }
}
