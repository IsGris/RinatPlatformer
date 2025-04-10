using Platformer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewMovementInstaller", menuName = "Installers/MovementInstaller")]
public class MovementInstaller : ScriptableObjectInstaller<MovementInstaller>
{
    public MovementModel.MovementSettings movementSettings;
	
    public override void InstallBindings()
    {
        Container.Bind<MovementModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MovementView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MovementPresenter>().FromComponentInHierarchy().AsSingle();

		Container.Bind<Transform>().WithId("CharacterTransform").FromComponentInHierarchy().AsSingle();
		Container.Bind<Rigidbody2D>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BoxCollider2D>().FromComponentInHierarchy().AsSingle();

        Container.Bind<MovementModel.MovementSettings>().FromInstance(movementSettings).AsSingle();
    }
}