using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public abstract class BaseAttackStrategy : MonoBehaviour
    {
		// EVENTS

		/// <summary>
		/// Invokes evey time attack happens<br/><br/>
		/// 1st parameter - Target that was attacked
		/// </summary>
		public event Action<GameObject> OnAttack;

        // INTERNAL VARIABLES

        [Inject] protected AttackSettings settings;
        [Inject(Optional = true)] protected MovementModel movementModel;

		// UNITY

		protected virtual void Awake()
		{
			if (settings.ApplyKnockbackOnAttack)
				OnAttack += ApplyKnockbackOnAttack;
		}
		
		// PROTECTED

		protected virtual void ApplyAttack(HealthModel targetHealthModel)
		{
			targetHealthModel.ApplyDamage(settings.Damage, gameObject);
			OnAttack?.Invoke(targetHealthModel.gameObject);
		}

		// PRIVATE
		
		private void ApplyKnockbackOnAttack(GameObject target)
		{
			if (settings.ApplyKnockbackOnAttack)
				movementModel.ApplyForce(settings.KnockbackDirection, settings.KnockbackPower);
		}
	}
}
