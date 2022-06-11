using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] Bank concreteBank;
    [SerializeField] TowerFactory concreteTowerFactory;
    [SerializeField] BuyPanelController buyPanel;
    [SerializeField] DraggableManager draggableManager;
    public override void InstallBindings()
    {
        Container.Bind<IBillingSystem>().To<Bank>().FromInstance(concreteBank);//.AsSingle();
        Container.Bind<IMarket>().To<BuyPanelController>().FromInstance(buyPanel);//.AsSingle();
        Container.Bind<TowerFactory>().FromInstance(concreteTowerFactory);
        Container.Bind<DraggableManager>().FromInstance(draggableManager);
    }
}
