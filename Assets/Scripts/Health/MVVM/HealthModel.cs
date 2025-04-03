using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    /// <summary>
    /// Default health model for health system
    /// </summary>
    public class HealthModel : MonoBehaviour
    {
		// EVENTS

		/// <summary>
		/// Invokes every time health changed<br/><br/>
		/// 1st parameter - New health amount
		/// </summary>
		public event Action<int> OnHealthChange;
		/// <summary>
		/// Invokes every time <see cref="HealthModel"/> gets damaged<br/><br/>
		/// 1st parameter - Damage amount<br/>
		/// 2nd parameter - New health amount<br/>
		/// 3rd parameter - GameObject that is attacked us
		/// </summary>
		public event Action<int, int, GameObject> OnDamageTaken;
        /// <summary>
        /// Invokes every time death happens
        /// </summary>
        public event Action OnDeath;

        // VARIABLES

        public int Health
        {
            get => _health;
            set
            {
                var newHealth = Math.Clamp(value, 0, MaxHealth);
				if (_health == newHealth)
                    return;

                _health = newHealth;
                if (newHealth > 0)
                    OnHealthChange?.Invoke(newHealth);
                else
                    OnDeath?.Invoke();
            }
        }
        public int MaxHealth => _maxHealth;

        // INTERNAL VARIABLES
		
        [Inject] protected HealthConfig config;

        private int _health;
        private int _maxHealth;
        
		// UNITY

		void Awake()
        {
            _health = config.Health;
            _maxHealth = config.MaxHealth;
        }

		// PUBLIC

		/// <summary>
		/// Applies damage from <paramref name="Attacker"/>
		/// </summary>
		/// <param name="Attacker">GameObject that attacked this GameObject</param>
		/// <returns>True if damage was successful</returns>
		public virtual bool ApplyDamage(int Value, GameObject Attacker)
        {
            if (Health == 0)
                return false;

			Health -= Value;
			OnDamageTaken?.Invoke(Value, Health, Attacker);
			return true;
		}
	}
}
