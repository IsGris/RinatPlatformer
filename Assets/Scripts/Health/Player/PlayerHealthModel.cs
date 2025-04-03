using System;
using UnityEngine;

namespace Platformer
{
	/// <summary>
	/// HealthModel for player character
	/// </summary>
    public class PlayerHealthModel : HealthModel
	{
		/// <summary>
		/// Check does HealthModel is immortal right now to damage
		/// </summary>
		public bool IsImmortal =>
			lastDamageTime + config.ImmortalTime >= Time.time;

		/// <summary>
		/// Last <see cref="Time.time"/> when damage was applied
		/// </summary>
		private float lastDamageTime = float.NegativeInfinity;

        public override bool ApplyDamage(int Value, GameObject Attacker) 
        {
			if (IsImmortal || !base.ApplyDamage(Value, Attacker))
				return false;

			lastDamageTime = Time.time;
			return true;
        }
    }
}
