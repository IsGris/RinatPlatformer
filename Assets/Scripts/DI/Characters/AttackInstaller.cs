using Platformer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewAttackInstaller", menuName = "Installers/AttackInstaller")]
public class AttackInstaller : ScriptableObjectInstaller<AttackInstaller>
{
    public AttackSettings attackSettings;
    public bool InstallBoxCollider = false;
	
    public override void InstallBindings()
    {
        Container.Bind<BaseAttackStrategy>().FromComponentInHierarchy().AsSingle();
        Container.Bind<AttackSettings>().FromInstance(attackSettings).AsSingle();
        if (InstallBoxCollider)
            Container.Bind<BoxCollider2D>().FromComponentInHierarchy().AsSingle();
	}
}