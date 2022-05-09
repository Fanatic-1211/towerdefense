using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapBuilderInstaller : MonoInstaller
{
    [SerializeField] TileMeshLibrary tileMeshLibrary;
    public override void InstallBindings()
    {
        Container.Bind<TileMeshLibrary>().To<TileMeshLibrary>().FromInstance(tileMeshLibrary);
    }
}
