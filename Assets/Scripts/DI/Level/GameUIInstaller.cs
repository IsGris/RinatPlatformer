using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Platformer;
using UnityEngine.InputSystem;

public class GameUIInstaller : MonoInstaller
{
    [SerializeField] private UIDocument deathPanel;
    [SerializeField] private UIDocument finishPanel;

    public override void InstallBindings()
    {
        Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
		Container.Bind<UIDocument>().WithId("DeathPanel").FromInstance(deathPanel).AsCached();
        Container.Bind<UIDocument>().WithId("FinishPanel").FromInstance(finishPanel).AsCached();
	}
}