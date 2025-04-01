using UnityEngine;
using Zenject;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        // INTERNAL VARIABLES
        
        [Inject] protected PlayerInputHandler inputHandler;
		[Inject] protected MovementModel movementModel;

		// UNITY

		private void Awake()
		{
			inputHandler.OnMoveInput += Move;
			inputHandler.OnJump += Jump;
			inputHandler.OnRunStart += StartRunning;
			inputHandler.OnRunEnd += StopRunning;
		}

		// PUBLIC

		public void Move(float Value)
		{
			if (Value > 0)
				movementModel.Direction = MovementDirection.Right;
			else if (Value < 0)
				movementModel.Direction = MovementDirection.Left;
			else
				movementModel.Direction = MovementDirection.None;
		}

		public void Jump() =>
			movementModel.Jump();

		public void StartRunning() =>
			movementModel.IsRunning = true;
		public void StopRunning() =>
			movementModel.IsRunning = false;
	}
}
