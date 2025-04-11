using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Zenject;

namespace Platformer
{
    public class GameUI : MonoBehaviour
    {
        [Inject(Id = "FinishPanel")] UIDocument finish;
        [Inject(Id = "DeathPanel")] UIDocument death;
        [Inject] PlayerInput playerInput;
        [Inject] SceneManager sceneManager;

		// UNITY

		private void Start()
		{
            InitializeFinishPanel();
		}

		// FINISH

		public void ShowFinishUI(int Score)
        {
            var finishRoot = finish.rootVisualElement.Q<VisualElement>("RootContainer");
            finishRoot.Q<Label>("Score").text = Convert.ToString(Score);
            SetPanelShowState(finish, true);
        }

        private void InitializeFinishPanel() =>
            finish.rootVisualElement.Q<Button>("MainMenu").clicked += sceneManager.LoadMainMenu;

        // DEATH

		public void ShowDeathUI() =>
			SetPanelShowState(death, true);

        // PRIVATE

		/// <summary>
		/// Set panel show state
		/// </summary>
		/// <param name="panel">Panel that need to be show/hidden</param>
		/// <param name="ShowState">Does we need to show this panel or hide</param>
		private void SetPanelShowState(UIDocument panel, bool ShowState)
        {
            if (!panel)
                return;

            if (ShowState)
            {
                panel.rootVisualElement.Q<VisualElement>("RootContainer").RemoveFromClassList("unshowed");
                SetLockInputState(true);
            }
            else
            {
				panel.rootVisualElement.Q<VisualElement>("RootContainer").AddToClassList("unshowed");
                SetLockInputState(false);
			}
		}

        /// <summary>
        /// Lock/Unlock player input
        /// </summary>
        /// <param name="LockInput">If true player input will be locked otherwise unlocked</param>
        private void SetLockInputState(bool LockInput) =>
            playerInput.enabled = !LockInput;
	}
}
