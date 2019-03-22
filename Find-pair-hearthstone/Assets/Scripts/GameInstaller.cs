using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TableCardController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CardOffController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CardStore>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
    }
}
