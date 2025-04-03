using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public class PlayerHealthViewModel : HealthViewModel
    {
		// EVENTS

		/// <summary>
		/// Invokes every time player need to apply knockback from hit<br/><br/>
		/// 1st parameter - normalized Vector in which direction need to apply knockback
		/// </summary>
		public event Action<Vector2> OnKnockBackRequired;

		// INTERNAL VARIABLES

		[Inject(Id = "CharacterTransform")] private readonly Transform playerTransform;

		// PROTECTED

		protected override void HandleTakeDamage(int Damage, int NewHealth, GameObject Attacker)
		{
			base.HandleTakeDamage(Damage, NewHealth, Attacker);

			Vector2 knockbackDirection;
			if (playerTransform.position.x < Attacker.transform.position.x) // Player is on left side of attacker
				knockbackDirection = new(-0.5f, 0.5f);
			else // Player is on right side of attacker
				knockbackDirection = new(0.5f, 0.5f);
			OnKnockBackRequired?.Invoke(knockbackDirection);
		}
	}
}
