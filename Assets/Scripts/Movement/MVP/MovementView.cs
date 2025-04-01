using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
	public class MovementView : MonoBehaviour
	{
		// INTERNAL VARIABLES
		
		[Inject] protected Animator animator;
		[Inject] protected SpriteRenderer sprite;

		// PUBLIC

		public void SetFallState(in bool NewState) =>
			animator.SetBool("IsFalling", NewState);
		public void SetRunState(in bool NewState) =>
			animator.SetBool("IsRunning", NewState);
		public void SetFacingRightState(in bool NewState)
		{
			if (NewState)
				sprite.flipX = false;
			else
				sprite.flipX = true;
		}
		public void SetMovingState(in bool NewState) =>
			animator.SetBool("IsMoving", NewState);
		public void ActivateJump() =>
			animator.SetTrigger("Jump");
	}
}
