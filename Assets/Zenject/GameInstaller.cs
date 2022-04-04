using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] Bank concreteBank;
    [SerializeField] TowerFactory concreteTowerFactory;
    public override void InstallBindings()
    {
        Container.Bind<IBillingSystem>().To<Bank>().FromInstance(concreteBank);///.AsSingle();
        Container.Bind<TowerFactory>().FromInstance(concreteTowerFactory);
    }
}
