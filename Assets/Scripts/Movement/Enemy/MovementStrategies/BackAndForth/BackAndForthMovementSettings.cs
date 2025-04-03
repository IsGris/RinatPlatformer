using System;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Class that contains settings for back and forth movement strategy
    /// </summary>
    [CreateAssetMenu(fileName = "NewBackAndForthMovementSettings", menuName = "MovementStrategies/BackAndForthMovementSettings")]
    public class BackAndForthMovementSettings : ScriptableObject
    {
		[Header("Initial")]
		[Tooltip("Direction in which GameObject moving on start")]
		public MovementDirection StartDirection = MovementDirection.Right;
	}
}
