using UnityEngine;
using Zenject;

namespace Platformer
{
    public class HealthView : MonoBehaviour
    {
        // INTERNAL VARIABLES

        [Inject] protected HealthViewModel viewModel;
        [Inject] protected Animator animator;

        // UNITY

        private void OnEnable() =>
            SubscribeToEvents();

        private void OnDisable() =>
            UnsubscribeFromEvents();

        private void OnDestroy() =>
            UnsubscribeFromEvents();

		// PROTECTED

		protected virtual void SubscribeToEvents()
        {
            viewModel.OnHitAnimationRequired += ApplyHitAnimation;
            viewModel.OnDeathAnimationRequired += ApplyDeathAnimation;
		}

        protected virtual void UnsubscribeFromEvents()
        {
			viewModel.OnHitAnimationRequired -= ApplyHitAnimation;
			viewModel.OnDeathAnimationRequired -= ApplyDeathAnimation;
		}

		// PRIVATE

		private void ApplyHitAnimation() =>
            animator.SetTrigger("Hit");

        private void ApplyDeathAnimation() =>
            animator.SetTrigger("Disappear");
    }
}
