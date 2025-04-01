using System;
using UnityEngine;

namespace Platformer
{
	[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
	public class MovementView : MonoBehaviour
	{
		// INTERNAL VARIABLES
		
		protected Animator animator;
		protected SpriteRenderer sprite;

		// UNITY

		private void Awake()
		{
			animator = GetComponent<Animator>();
			sprite = GetComponent<SpriteRenderer>();
		}

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
