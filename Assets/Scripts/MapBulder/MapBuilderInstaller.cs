using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Game.Environment.Map
{
    public class MapBuilderInstaller : MonoInstaller
    {
        [SerializeField] TileMeshLibrary tileMeshLibrary;
        [SerializeField] MapCreator mapCreator;
        public override void InstallBindings()
        {
            Container.Bind<TileMeshLibrary>().To<TileMeshLibrary>().FromInstance(tileMeshLibrary);
            Container.Bind<MapCreator>().To<MapCreator>().FromInstance(mapCreator);
        }
    }
}