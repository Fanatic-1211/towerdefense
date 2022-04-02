using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] Bank concreteBank;
    public override void InstallBindings()
    {
        Container.Bind<IBillingSystem>().To<Bank>().FromInstance(concreteBank);///.AsSingle();
    }
}
