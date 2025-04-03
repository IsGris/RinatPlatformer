using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
	public enum MovementDirection
	{
		Left = -1,
		None = 0,
		Right = 1
	}

	/// <summary>
	/// Model for movement in 2D space
	/// </summary>
	public class MovementModel : MonoBehaviour
	{
		// EVENTS

		/// <summary>
		/// Activates when movement durection changes
		/// </summary>
		/// <remarks>1st parameter <see cref="MovementDirection"/> is new movement direction</remarks>
		public event Action<MovementDirection> OnDirectionChange;
		/// <summary>
		/// Activates when run status changes
		/// </summary>
		/// <remarks>1st parameter <see cref="bool"/> is new run status</remarks>
		public event Action<bool> OnRunStatusChange;
		/// <summary>
		/// Activates every time fall status changes
		/// </summary>
		/// <remarks>1st parameter <see cref="bool"/> is new fall status</remarks>
		public event Action<bool> OnFallStatusChange;
		public event Action OnJump;

		// PUBLIC VARIABLES

		public MovementDirection Direction
		{
			get => _direction;
			set
			{
				if (value == _direction)
					return;

				DirectionChangeTime = Time.time;
				_direction = value;
				OnDirectionChange?.Invoke(value);
			}
		}

		public bool IsRunning
		{
			get => _isRunning;
			set
			{
				if (value == _isRunning)
					return;

				_isRunning = value;
				OnRunStatusChange?.Invoke(value);
			}
		}
		public bool IsFalling
		{
			get => _isFalling;
			set
			{
				if (value == _isFalling)
					return;

				_isFalling = value;
				OnFallStatusChange?.Invoke(value);
			}
		}
		/// <summary>
		/// Check does GameObject grounded based on Collider2D check
		/// </summary>
		public bool IsGrounded { get; private set; }

		// INITIAL VARIABLES

		[Serializable]
		public class MovementSettings
		{
			[Header("Movement Settings")]
			[SerializeField] public float MoveSpeed = 5f;
			[SerializeField] public float RunSpeed = 10f;
			[SerializeField] public float JumpForce = 10f;
			[Tooltip("Time amount how long it would take to transition from not moving to full speed")]
			[SerializeField] public float DirectionTransitionTime = 0.5f;
			[Tooltip("Time in seconds after leaving the ground during which a jump is still allowed")]
			[SerializeField] public float CoyoteTime = 0.2f;

			[Header("Ground Check")]
			[SerializeField] public LayerMask GroundLayer;
			[Tooltip("Height of the box that is spawned below GameObject to check whether it is on ground")]
			[SerializeField] public float GroundCheckHeight = 0.1f;
			[Tooltip("Small distance that is subtracted from collision check to prevent collisions where the object touches the edge of the collider")]
			[SerializeField] public float GroundCheckOffset = 0.03f;
		}

		[Inject] protected MovementSettings settings;

		// INTERNAL VARIABLES

		[Inject] protected new Rigidbody2D rigidbody;
		[Inject] protected new BoxCollider2D collider;
		[Inject(Id = "MovingObjectTransform")] protected new Transform transform;

		/// <summary>
		/// Current speed
		/// </summary>
		protected float Speed = 0f;
		/// <summary>
		/// Last <see cref="Time.time"/> when movement direction was changed
		/// </summary>
		protected float DirectionChangeTime = float.NegativeInfinity;
		/// <summary>
		/// Time when GameObject lastely jumped
		/// </summary>
		protected float LastJumpTime = float.NegativeInfinity;
		/// <summary>
		/// Time when GameObject lastely was on ground
		/// </summary>
		protected float LastGroundTime = float.NegativeInfinity;
		/// <summary>
		/// Current direction we are moving in
		/// </summary>
		protected MovementDirection _direction = MovementDirection.None;
		/// <summary>
		/// Internal field for tracking run state.
		/// </summary>
		/// <remarks>
		/// Use <see cref="IsRunning"/> instead to ensure event invocation.
		/// </remarks>
		protected bool _isRunning = false;
		/// <summary>
		/// Internal field for tracking falling state.
		/// </summary>
		/// <remarks>
		/// Use <see cref="IsFalling"/> instead to ensure event invocation.
		/// </remarks>
		private bool _isFalling = false;

		// UNITY

		private void Update()
		{
			IsGrounded = Physics2D.OverlapBox( // Create box under gameobject to check ground
						new(transform.position.x + collider.offset.x,
							transform.position.y + collider.offset.y
							- collider.size.y / 2/*Make it at bottom of gameobject*/
							- settings.GroundCheckHeight / 2
							- settings.GroundCheckOffset),
						new(collider.size.x - settings.GroundCheckOffset, settings.GroundCheckHeight),
						0,
						settings.GroundLayer
					);

			var NewFallStatus = false;
			if (IsGrounded) // If grounded, update LastGroundTime
				LastGroundTime = Time.time;
			else if (rigidbody.linearVelocityY < 0) // If not grounded and velocity is falling, set new fall status to true
				NewFallStatus = true;
			IsFalling = NewFallStatus;
			
			UpdateSpeed();

			rigidbody.linearVelocityX = Speed;
		}

		// PUBLIC

		/// <summary>
		/// Try to jump
		/// </summary>
		/// <returns>does jump was successful</returns>
		[ContextMenu("Jump")]
		public bool Jump()
		{
			if (!IsGrounded && !(LastGroundTime + settings.CoyoteTime >= Time.time && LastJumpTime + settings.CoyoteTime <= Time.time)) // Check does can jump
				return false;

			rigidbody.linearVelocityY = settings.JumpForce;
			LastJumpTime = Time.time;
			OnJump?.Invoke();
			return true;
		}

		// PRIVATE

		/// <summary>
		/// Update current speed based on <see cref="Direction"/>, <see cref="IsRunning"/>, <see cref="DirectionChangeTime"/> and <see cref="DirectionTransitionTime"/>
		/// </summary>
		private void UpdateSpeed()
		{
			if (Direction == MovementDirection.None)
			{
				Speed = 0f;
				return;
			}

			if (IsRunning)
			{
				// Set speed to maximum when running for better movement
				Speed = settings.RunSpeed * (int)Direction;
			} 
			else
			{
				if (settings.DirectionTransitionTime > 0) // Linear increase speed using DirectionChangeTime
					Speed = Mathf.Min(
								settings.MoveSpeed, 
								Mathf.Lerp(0, settings.MoveSpeed, (Time.time - DirectionChangeTime) / settings.DirectionTransitionTime)
							) * (int)Direction;
				else
					Speed = settings.MoveSpeed * (int)Direction;
			}
		}
	}
}
