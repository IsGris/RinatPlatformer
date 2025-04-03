using System;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Default settings for attack scripts
    /// </summary>
    [CreateAssetMenu(fileName = "NewAttackSettings", menuName = "Attack/AttackSettings")]
    public class AttackSettings : ScriptableObject
    {
        [Header("Attack")]
        public int Damage = 1;
        [Tooltip("Does need to apply knockback to attacker when attacked target")]
        public bool ApplyKnockbackOnAttack = false;
        public float KnockbackPower = 5;
        [Tooltip("Direction in which knockback is applied")]
		public Vector2 KnockbackDirection = new(0, 1);
    }
}
