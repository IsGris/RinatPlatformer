using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public sealed class BackAndForthMovement : MovementStrategyBase
    {
        [Inject] private readonly BackAndForthMovementSettings settings;
		[Inject] private new readonly BoxCollider2D collider;
		[Inject(Id = "MovingObjectTransform")] private new readonly Transform transform;

		private bool IsCollidingRightWall =>
				Physics2D.OverlapBox( // Create box on right of gameobject to check wall
					new(transform.position.x + collider.offset.x
						+ collider.size.x / 2/*Make it at right side of gameobject*/
						+ settings.WallCheckWidth / 2
						+ settings.WallCheckOffset,
						transform.position.y + collider.offset.y),
					new(settings.WallCheckWidth, collider.size.y - settings.WallCheckOffset),
					0,
					settings.WallLayer
				);
		private bool IsCollidingLeftWall =>
			Physics2D.OverlapBox( // Create box on ledt side of gameobject to check wall
						new(transform.position.x + collider.offset.x
							- collider.size.x / 2/*Make it at left side of gameobject*/
							- settings.WallCheckWidth / 2
							- settings.WallCheckOffset,
							transform.position.y + collider.offset.y),
						new(settings.WallCheckWidth, collider.size.y - settings.WallCheckOffset),
						0,
						settings.WallLayer
					);

		private void Start() =>
			movementModel.Direction = settings.StartDirection;

		/// <inheritdoc/>
		public override void UpdateMovement()
        {
			switch (movementModel.Direction)
			{
				case MovementDirection.Right:
					if (IsCollidingRightWall)
						movementModel.Direction = MovementDirection.Left;
					break;
				case MovementDirection.Left:
					if (IsCollidingLeftWall)
						movementModel.Direction = MovementDirection.Right;
					break;
			}
        }
	}
}
