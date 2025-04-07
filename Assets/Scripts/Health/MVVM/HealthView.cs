using UnityEngine;
using System;
using Zenject;
using UnityEngine.UI;
using System.Linq;

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

		// PUBLIC
		
		/// <summary>
		/// Visual component types used for health
		/// </summary>
		private static readonly Type[] visualTypes = new Type[]
	    {
		    typeof(SpriteRenderer),
		    typeof(Animator),
			typeof(HealthView),
			typeof(HealthViewModel)
	    };

		/// <summary>
		/// Disable all components except for visual ones
		/// </summary>
		public void DisableAllButVisual()
        {
			Component[] components = GetComponents<Component>();

			foreach (var comp in components)
			{
				if (comp == this)
					continue; // Dont disable this component

				// Check if component type is NOT a visual type
				bool isVisual = visualTypes.Any(t => t.IsAssignableFrom(comp.GetType()));

				if (!isVisual && comp is Behaviour behaviour) // Check does component not visual and can be disabled
					behaviour.enabled = false;
				else if (comp is Rigidbody2D) // Rigidbody can't be disabled so set simulated to false
					((Rigidbody2D)comp).simulated = false;
			}
		}

		/// <summary>
		/// Deletes current game object
		/// </summary>
		/// <remarks>
		/// Used from Animation clip event when death animation ends
		/// </remarks>
		public void DeleteGameObject() =>
			Destroy(gameObject);

		public void ApplyHitAnimation() =>
			animator.SetTrigger("Hit");

		public void ApplyDeathAnimation() =>
			animator.SetTrigger("Disappear");

		// PROTECTED

		protected virtual void SubscribeToEvents()
        {
            viewModel.OnHitAnimationRequired += ApplyHitAnimation;
            viewModel.OnDeathAnimationRequired += ApplyDeathAnimation;
            viewModel.OnOnlyVisaulRequired += DisableAllButVisual;
		}

		protected virtual void UnsubscribeFromEvents()
        {
			viewModel.OnHitAnimationRequired -= ApplyHitAnimation;
			viewModel.OnDeathAnimationRequired -= ApplyDeathAnimation;
            viewModel.OnOnlyVisaulRequired -= DisableAllButVisual;
        }
    }
}
