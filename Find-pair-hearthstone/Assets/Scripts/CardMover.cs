using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardMover : MonoBehaviour
{
    public void MoveCard(Transform card, Vector3 startPosition, Vector3 targetPosition, float delay, System.Action callback = null)
    {
        StartCoroutine(MoveCardCoroutine(card, startPosition, targetPosition, delay, callback));
    }

    private IEnumerator MoveCardCoroutine(Transform card, Vector3 startPosition, Vector3 targetPosition, float delay, System.Action callback = null)
    {
        yield return new WaitForSeconds(delay);
        float time = 0;
        while (time < 1)
        {
            card.position = Vector3.Lerp(startPosition, targetPosition, time);
            time += Time.deltaTime;
            yield return null;
        }
        card.position = targetPosition;
        callback?.Invoke();
    }
}
