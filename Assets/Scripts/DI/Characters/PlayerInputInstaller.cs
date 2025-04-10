using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Platformer;

[CreateAssetMenu(fileName = "NewPlayerInputInstaller", menuName = "Installers/PlayerInputInstaller")]
public class PlayerInputInstaller : ScriptableObjectInstaller<PlayerInputInstaller>
{
    [Header("Actions")]
    public string MoveActionName;
    public string JumpActionName;
    public string RunActionName;

	public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInputHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();

        Container.Bind<string>().WithId("MoveActionName").FromInstance(MoveActionName).AsCached();
        Container.Bind<string>().WithId("JumpActionName").FromInstance(JumpActionName).AsCached();
        Container.Bind<string>().WithId("RunActionName").FromInstance(RunActionName).AsCached();
	}
}