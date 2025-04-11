using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public class PlayerHealthView : HealthView
    {
        // INTERNAL VARIABLES

        [Inject] protected MovementModel movementModel;
        [Inject] protected HealthConfig settings;
		[Inject] private GameUI ui;
		[Inject] private LevelData levelData;

		// PROTECTED

		protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            PlayerHealthViewModel playerViewModel = viewModel as PlayerHealthViewModel;
            if (!playerViewModel)
            {
                Debug.LogError($"Cast from {viewModel.GetType().Name} to PlayerHealthViewModel failed " +
                    $"in {this.GetType().Name} class in \"{gameObject.name}\" GameObject");
                return;
            }

            playerViewModel.OnKnockBackRequired += ApplyKnockback;
			playerViewModel.OnDeathUIRequired += ShowDeathUI;
		}

		protected override void UnsubscribeFromEvents()
        {
			base.UnsubscribeFromEvents();
			PlayerHealthViewModel playerViewModel = viewModel as PlayerHealthViewModel;
			if (!playerViewModel)
			{
				Debug.LogError($"Cast from {viewModel.GetType().Name} to PlayerHealthViewModel failed " +
					$"in {this.GetType().Name} class in \"{gameObject.name}\" GameObject");
				return;
			}

			playerViewModel.OnKnockBackRequired -= ApplyKnockback;
			playerViewModel.OnDeathUIRequired -= ShowDeathUI;
		}

		// PRIVATE

        private void ShowDeathUI() =>
            ui.ShowDeathUI(levelData.FruitsCollected);

		private void ApplyKnockback(Vector2 direction) =>
            movementModel.ApplyForce(direction, settings.KnockbackPower);
	}
}
