using UnityEngine;
using Zenject;

namespace Platformer
{
    /// <summary>
    /// Installer for level
    /// </summary>
    [CreateAssetMenu(fileName = "NewLevelInstaller", menuName = "Installers/LevelInstaller")]
    public class LevelInstaller : ScriptableObjectInstaller<LevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelData>().FromComponentInHierarchy().AsSingle();
			Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<LevelManagerPresenter>().FromComponentInHierarchy().AsSingle();
		}
    }
}
