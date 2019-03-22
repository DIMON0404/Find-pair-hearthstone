using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI : MonoBehaviour
{
    public Button startButton;
    public GameObject finalText;

    [Inject]
    private GameController gameController;

    private void Awake()
    {
        gameController.onStateChange += OnStateChange;
    }

    public void OnStartButton()
    {
        gameController.CurrentState = GameController.State.Game;
    }

    private void OnStateChange(GameController.State state)
    {
        switch (state)
        {
            case GameController.State.Start:
                startButton.gameObject.SetActive(true);
                finalText.SetActive(false);
                break;
            case GameController.State.Game:
                startButton.gameObject.SetActive(false);
                break;
            case GameController.State.End:
                finalText.SetActive(true);
                StartCoroutine(WaitForTouch(() => { gameController.CurrentState = GameController.State.Start; }));
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitForTouch(System.Action callback)
    {
        while (Input.touchCount == 0)
        {
            yield return null;
        }
        callback?.Invoke();
    }

}
