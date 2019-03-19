using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum State { Face, Back, InDeck, Dumped };

    public State state;
    public string ID;

    [SerializeField]
    private TableCardController cardController;
    [SerializeField]
    private Animator animator;
    private bool allowAction = true;                // Action will allowe after each animation play

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialization(TableCardController tableCardController)
    {
        cardController = tableCardController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (cardController.CanCardRotate)
        {
            switch (state)
            {
                case State.Face:
                    Close();
                    break;
                case State.Back:
                    Open();
                    break;
                default:
                    SetAnimationTrigger("NotAllowed");
                    break;
            }
        }
    }

    public void Open()
    {
        SetAnimationTrigger("ToFace");
    }

    public void Close()
    {
        SetAnimationTrigger("ToBack");
    }

    public void NotAllowedAction()
    {
        SetAnimationTrigger("NotAllowed");
    }

    private void SetAnimationTrigger(string trigger, bool lockAction = true)
    {
        animator.SetTrigger(trigger);
        allowAction &= !lockAction;
    }

    public void OnAnimationEnd(State state)
    {
        switch (state)
        {
            case State.Face:
                cardController.OnCardAction(this);
                break;
            case State.Back:
                cardController.OnCardAction(this);
                break;
        }
        allowAction = true;
        this.state = state;
    }

    public void OnGoOut()
    {
        animator.SetTrigger("Explosion");
    }
}
