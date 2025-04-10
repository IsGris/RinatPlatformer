using Platformer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewEnemyMovementInstaller", menuName = "Installers/EnemyMovementInstaller")]
public class EnemyMovementInstaller : MovementInstaller
{
    [Header("Movement strategies")]
    public BackAndForthMovementSettings backAndForthMovementSettings;
	
    public override void InstallBindings()
    {
        base.InstallBindings();
        Container.Bind<BackAndForthMovementSettings>().FromInstance(backAndForthMovementSettings).AsSingle();
    }
}