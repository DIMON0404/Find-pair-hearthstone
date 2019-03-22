using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Card : MonoBehaviour
{
    public enum State { Face, Back, InDeck, Dumped };

    public State state;
    public string ID;
    public ParticleSystem greenParticles;
    public Vector2Int positionInGrid;
    
    private TableCardController cardController;
    [SerializeField]
    private Animator animator;
    private bool allowAction = false;                // Action will allowe after each animation play

    private void Awake()
    {
        ID = gameObject.name.Replace("(Clone)", "");
    }

    public void OnAddToScene(TableCardController tableCardController, Vector2Int position)
    {
        cardController = tableCardController;
        allowAction = true;
        greenParticles.Play();
        positionInGrid = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (allowAction && cardController.CanCardRotate)
        {
            switch (state)
            {
                case State.Face:
                    Close();
                    cardController.OnCardAction(this);
                    break;
                case State.Back:
                    Open();
                    cardController.OnCardAction(this);
                    break;
                default:
                    SetAnimationTrigger("NotAllowed");
                    break;
            }
        }
    }

    public void Open()
    {
        state = State.Face;
        SetAnimationTrigger("ToFace");
    }

    public void Close()
    {
        state = State.Back;
        SetAnimationTrigger("ToBack");
    }

    public void NotAllowedAction()
    {
        SetAnimationTrigger("NotAllowed");
    }

    private void SetAnimationTrigger(string trigger, bool lockAction = true)
    {
        if (allowAction)
        {
            animator.SetTrigger(trigger);
            allowAction &= !lockAction;
        }
    }

    public void OnAnimationEnd()
    {
        allowAction = true;
    }

    public void OnGoOut()
    {
        animator.SetTrigger("Explosion");
        allowAction = false;
        greenParticles.Stop();
    }
}
