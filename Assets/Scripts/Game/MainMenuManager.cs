using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Platformer
{
    public class MainMenuManager : MonoBehaviour
    {
        [Inject(Id = "MainMenu")] UIDocument mainMenu;
        [Inject] SceneManager sceneManager;

		private void Start()
		{
			mainMenu.rootVisualElement.Q<Button>("Play").clicked += () => sceneManager.LoadLevel(1);
		}
	}
}
