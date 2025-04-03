using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "NewCollisionAttackSettings", menuName = "Attack/CollisionAttackSettings")]
    public class CollisionAttackSettings : AttackSettings
    {
        [Header("Attack sides")]
		[Tooltip("Offset used to prevent from small deviations when check collsion side")]
		public float CollisionOffset = 0.25f;
		[Tooltip("Does GameObject attacks other GameObject if target collided on side")]
		public bool AttackSideCollision;
		[Tooltip("Does GameObject attacks other GameObject if target collided on top")]
		public bool AttackTopCollision;
		[Tooltip("Does GameObject attacks other GameObject if target collided on bottom")]
		public bool AttackBottomCollision;
	}
}
