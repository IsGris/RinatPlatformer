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
		}

		// PRIVATE

		private void ApplyKnockback(Vector2 direction) =>
            movementModel.ApplyForce(direction, settings.KnockbackPower);
	}
}
