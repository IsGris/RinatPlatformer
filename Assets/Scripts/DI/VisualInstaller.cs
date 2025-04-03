using Platformer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewVisualInstaller", menuName = "Installers/VisualInstaller")]
public class VisualInstaller : ScriptableObjectInstaller<VisualInstaller>
{
    public override void InstallBindings()
    {
		Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
		Container.Bind<SpriteRenderer>().FromComponentInHierarchy().AsSingle();
    }
}