using Platformer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewHealthInstaller", menuName = "Installers/HealthInstaller")]
public class HealthInstaller : ScriptableObjectInstaller<HealthInstaller>
{
    public HealthConfig healthConfig;
	
    public override void InstallBindings()
    {
        Container.Bind<HealthModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<HealthViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<HealthView>().FromComponentInHierarchy().AsSingle();

        Container.Bind<HealthConfig>().FromInstance(healthConfig).AsSingle();
    }
}