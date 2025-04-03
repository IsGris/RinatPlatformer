using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Config for <see cref="HealthModel"/> that contains default values on start
    /// </summary>
    [CreateAssetMenu(fileName = "NewHealthConfig", menuName = "Health/Config")]
    public class HealthConfig : ScriptableObject
    {
        [Header("Health")]
        [Tooltip("How much health on start")] public int Health = 1;
        [Tooltip("Maximum amount of health")] public int MaxHealth = 1;
        [Header("Damage")]
        [Tooltip("How much seconds GameObject immortal after taking damage")] public float ImmortalTime = 1;
        [Tooltip("Amount of power that knockback is applied to GameObject after taking damage")] public float KnockbackPower = 1;
	}
}
