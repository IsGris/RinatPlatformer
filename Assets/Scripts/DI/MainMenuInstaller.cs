using Zenject;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuInstaller : MonoInstaller
{
	[SerializeField] UIDocument mainMenuUI;
	public override void InstallBindings() =>
		Container.Bind<UIDocument>().WithId("MainMenu").FromInstance(mainMenuUI).AsCached();
}
