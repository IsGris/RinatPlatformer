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

		[Header("Wall Check")]
		public LayerMask WallLayer;
		[Tooltip("Width of the box that is spawned on left and on right side of GameObject to check whether it is colliding walls")]
		public float WallCheckWidth = 0.1f;
		[Tooltip("Small distance that is subtracted from collision check to prevent collisions where the object touches the edge of the collider")]
		public float WallCheckOffset = 0.03f;
	}
}
