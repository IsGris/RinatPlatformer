using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    /// <summary>
    /// Attacks character that collides with this character
    /// </summary>
    /// <remarks>
    /// Requires BoxCollider2D at same GameObject with this component
    /// </remarks>
    [RequireComponent(typeof(BoxCollider2D))]
    public class CollisionAttack : BaseAttackStrategy
    {
        // INTERNAL VARIABLES

        [Inject] protected BoxCollider2D boxCollider;

        private CollisionAttackSettings collisionAttackSettings;

		// UNITY

		protected override void Awake()
		{
            base.Awake();

            collisionAttackSettings = settings as CollisionAttackSettings;
            if (!collisionAttackSettings)
            {
                Debug.LogError($"Can't convert AttackSettings to CollisionAttackSettings in \"{gameObject.name}\" GameObject");
                this.enabled = false;
            }
		}
        
		private void OnCollisionEnter2D(Collision2D collision)
		{
			var targetContext = collision.gameObject.GetComponent<GameObjectContext>();
            if (!targetContext) 
                return;
            HealthModel targetHealth = targetContext.Container.Resolve<HealthModel>();
            if (!targetHealth)
                return;

			if (collision.collider.bounds.min.y + collisionAttackSettings.CollisionOffset/*Apply offset to prevent small deviations*/ 
                > boxCollider.bounds.max.y) // check target bottom point higher than attacker top point
                if (collisionAttackSettings.AttackTopCollision) // check does attack allowed 
					ApplyAttack(targetHealth);

            else if (collision.collider.bounds.max.y - collisionAttackSettings.CollisionOffset/*Apply offset to prevent small deviations*/ 
                < boxCollider.bounds.min.y) // check does target top point below attacker lowest point
                if (collisionAttackSettings.AttackBottomCollision)  // check does attack allowed
					ApplyAttack(targetHealth);

			else if (collisionAttackSettings.AttackSideCollision) // if target not in upper and not in below position then it's on side
				ApplyAttack(targetHealth);
		}
	}
}
