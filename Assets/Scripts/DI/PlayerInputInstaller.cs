using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Platformer;

public class PlayerInputInstaller : MonoInstaller
{
    [Header("Components")]
    public PlayerController playerController;
    public PlayerInputHandler playerInputHandler;
    public PlayerInput playerInput;

    [Header("Actions")]
    public string MoveActionName;
    public string JumpActionName;
    public string RunActionName;

	public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
        Container.Bind<PlayerInputHandler>().FromInstance(playerInputHandler).AsSingle();
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();

        Container.Bind<string>().WithId("MoveActionName").FromInstance(MoveActionName).AsCached();
        Container.Bind<string>().WithId("JumpActionName").FromInstance(JumpActionName).AsCached();
        Container.Bind<string>().WithId("RunActionName").FromInstance(RunActionName).AsCached();
	}
}