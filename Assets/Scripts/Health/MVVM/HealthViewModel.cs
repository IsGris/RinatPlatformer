using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public class HealthViewModel : MonoBehaviour
    {
        // EVENTS

        /// <summary>
        /// Activates when need to update health<br/>
        /// 1st parameter - new health amount
        /// </summary>
        public event Action<int> OnHealthUpdateRequired;
		public event Action OnHitAnimationRequired;
        public event Action OnDeathAnimationRequired;
        public event Action OnOnlyVisaulRequired;

		// INTERNAL VARIABLES

		[Inject] protected HealthModel model;

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
			model.OnDamageTaken += HandleTakeDamage;
			model.OnHealthChange += HandleHealthChange;
			model.OnDeath += HandleDeath;
        }


		protected virtual void UnsubscribeFromEvents()
        {
            model.OnDamageTaken -= HandleTakeDamage;
			model.OnHealthChange -= HandleHealthChange;
			model.OnDeath -= HandleDeath;
		}

        // HANDLERS

		protected virtual void HandleTakeDamage(int Damage, int NewHealth, GameObject Attacker) =>
            OnHitAnimationRequired?.Invoke();

        protected virtual void HandleHealthChange(int NewValue) =>
			OnHealthUpdateRequired?.Invoke(NewValue);

		protected virtual void HandleDeath()
        {
            OnDeathAnimationRequired?.Invoke();
            // Enable only visual components just for death animation
            OnOnlyVisaulRequired?.Invoke();
        }
	
    }
}
