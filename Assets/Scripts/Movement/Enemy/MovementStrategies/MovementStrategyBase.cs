using UnityEngine;
using Zenject;

namespace Platformer
{
    /// <summary>
    /// Base class for all movement strategies used in AI
    /// </summary>
    public abstract class MovementStrategyBase : MonoBehaviour
	{
		/// <summary>
		/// Movement model that is used for movement in target GameObject
		/// </summary>
		[Inject] protected MovementModel movementModel;
        /// <summary>
        /// Update current movement variables from <see cref="movementModel"/> based on current position
        /// </summary>
        public abstract void UpdateMovement();
        /// <summary>
        /// Update movement when tick happens
        /// </summary>
        public virtual void Update() =>
            UpdateMovement();
    }
}
