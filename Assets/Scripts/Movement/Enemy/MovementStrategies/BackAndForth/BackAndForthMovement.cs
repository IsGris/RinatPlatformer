using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public sealed class BackAndForthMovement : MovementStrategyBase
    {
        [Inject] private readonly BackAndForthMovementSettings settings;

		private void Start() =>
			movementModel.Direction = settings.StartDirection;

		/// <inheritdoc/>
		public override void UpdateMovement()
        {
			switch (movementModel.Direction)
			{
				case MovementDirection.Right:
					if (movementModel.IsTouchRightWall)
						movementModel.Direction = MovementDirection.Left;
					break;
				case MovementDirection.Left:
					if (movementModel.IsTouchLeftWall)
						movementModel.Direction = MovementDirection.Right;
					break;
			}
        }
	}
}
