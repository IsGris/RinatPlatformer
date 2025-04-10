using Platformer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewFruitInstaller", menuName = "Installers/FruitInstaller")]
public class FruitInstaller : ScriptableObjectInstaller<FruitInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<FruitCollectable>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FruitCollectableView>().FromComponentInHierarchy().AsSingle();
    }
}