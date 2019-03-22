using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Game configuration", menuName = "New game configuration")]
public class GameConfiguration : ScriptableObjectInstaller<GameConfiguration>
{
    public float delayBeforeDiscard;
    public float delayBeforeTurnOver;

    public override void InstallBindings()
    {
        if (delayBeforeTurnOver < 1) delayBeforeTurnOver = 1;               // Waiting for end flip animation
        Container.Bind<GameConfiguration>().FromInstance(this).AsSingle();
    }
}
