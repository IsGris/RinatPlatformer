using Platformer;
using UnityEngine;
using Zenject;

public class SceneManagerInstaller : MonoInstaller
{
    public override void InstallBindings() =>
		Container.Bind<SceneManager>().FromComponentInHierarchy().AsSingle();
}