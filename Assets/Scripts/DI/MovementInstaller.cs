using Platformer;
using UnityEngine;
using Zenject;

public class MovementInstaller : MonoInstaller
{
    [Header("Components")]
    public MovementModel movementModel;
    public MovementView movementView;
    public MovementPresenter movementPresenter;

    public Transform movingObjectTransform;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2d;
    public BoxCollider2D boxCollider2d;

	[Header("Settings")]
    public MovementModel.MovementSettings movementSettings;
	
    public override void InstallBindings()
    {
        Container.Bind<MovementModel>().FromInstance(movementModel).AsSingle();
        Container.Bind<MovementView>().FromInstance(movementView).AsSingle();
        Container.Bind<MovementPresenter>().FromInstance(movementPresenter).AsSingle();

		Container.Bind<Transform>().WithId("MovingObjectTransform").FromInstance(movingObjectTransform).AsSingle();
		Container.Bind<Animator>().FromInstance(animator).AsSingle();
		Container.Bind<SpriteRenderer>().FromInstance(spriteRenderer).AsSingle();
		Container.Bind<Rigidbody2D>().FromInstance(rigidbody2d).AsSingle();
        Container.Bind<BoxCollider2D>().FromInstance(boxCollider2d).AsSingle();

        Container.Bind<MovementModel.MovementSettings>().FromInstance(movementSettings).AsSingle();
    }
}