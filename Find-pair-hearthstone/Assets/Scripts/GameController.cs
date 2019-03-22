using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject]
    private CardStore cardStore;
    [Inject]
    private CardOffController cardOffController;
    [Inject]
    private TableCardController cardController;

    public enum State { Start, Game, End };

    private State currentState;
    public State CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
            switch (value)
            {
                case State.Start:
                    break;
                case State.Game:
                    cardOffController.ResetDeck();
                    cardStore.ResetDeck();
                    cardController.StartGame();
                    break;
                case State.End:
                    break;
                default:
                    break;
            }
            onStateChange?.Invoke(value);
        }
    }

    public System.Action<State> onStateChange;

    private void Start()
    {
        CurrentState = State.Start;
    }

}
