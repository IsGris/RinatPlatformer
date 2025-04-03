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
		/// <summary>
		/// Does character touch right wall
		/// </summary>
		public bool IsTouchRightWall { get; private set; }
		/// <summary>
		/// Does character touch left wall
		/// </summary>
		public bool IsTouchLeftWall { get; private set; }

		// INITIAL VARIABLES

		[Serializable]
		public class MovementSettings
		{
			[Header("Movement Settings")]
			[SerializeField] public float MoveSpeed = 5f;
			[SerializeField] public float RunSpeed = 10f;
			[SerializeField] public float JumpForce = 10f;
			[Tooltip("How fast character will approach desired speed")]
			[SerializeField] public float Acceleration = 15f;
			[Tooltip("Time in seconds after leaving the ground during which a jump is still allowed")]
			[SerializeField] public float CoyoteTime = 0.2f;

			[Header("Collision Check")]
			[SerializeField] public LayerMask CollisionLayer;
			[Tooltip("Thickness of the box that is spawned to check does character touching some obstacles")]
			[SerializeField] public float CollisionCheckThickness = 0.1f;
			[Tooltip("Small distance that is subtracted from collision check to prevent collisions where the object touches the edge of the collider")]
			[SerializeField] public float CollisionCheckOffset = 0.03f;
		}

		[Inject] protected MovementSettings settings;

		// INTERNAL VARIABLES

		[Inject] protected new Rigidbody2D rigidbody;
		[Inject] protected new BoxCollider2D collider;
		[Inject(Id = "CharacterTransform")] protected new Transform transform;

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
			UpdateGroundStatus();
			UpdateCollisionChecks();
			UpdateSpeed();
		}

		private void FixedUpdate() =>
			ApplySpeed(Time.fixedDeltaTime);

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

		/// <summary>
		/// Applies a force to the character in a given direction with a specified power
		/// </summary>
		/// <param name="direction">The normalized direction vector to apply the force</param>
		/// <param name="power">The magnitude of the force to apply</param>
		public void ApplyForce(Vector2 direction, float power) =>
			rigidbody.AddForce(direction.normalized * power, ForceMode2D.Impulse);

		// PRIVATE

		/// <summary>
		/// Applies <see cref="Speed"/> variable on character's Rigidbody gradually.
		/// </summary>
		/// <param name="Value">Modifier that scales the acceleration effect</param>
		private void ApplySpeed(float Value)
		{
			if (!IsGrounded && Speed == 0f) // If in air and doesnt moving then dont apply speed
				return;
			// Smoothly apply velocity towards desired speed
			rigidbody.linearVelocityX = Mathf.MoveTowards(rigidbody.linearVelocityX, Speed, settings.Acceleration * Value);
		}

		/// <summary>
		/// Update status for <see cref="IsGrounded"/> and <see cref="IsFalling"/>
		/// </summary>
		private void UpdateGroundStatus()
		{
			var NewFallStatus = false;
			if (IsGrounded) // If grounded, update LastGroundTime
				LastGroundTime = Time.time;
			else if (rigidbody.linearVelocityY < 0) // If not grounded and velocity is falling, set new fall status to true
				NewFallStatus = true;
			IsFalling = NewFallStatus;
		}

		/// <summary>
		/// Update variables for collision check between character and obstacles
		/// </summary>
		private void UpdateCollisionChecks()
		{
			IsGrounded = Physics2D.OverlapBox( // Create box under gameobject to check ground
						new(transform.position.x + collider.offset.x,
							transform.position.y + collider.offset.y
							- collider.size.y / 2/*Make it at bottom of gameobject*/
							- settings.CollisionCheckThickness / 2
							- settings.CollisionCheckOffset),
						new(collider.size.x - settings.CollisionCheckOffset, settings.CollisionCheckThickness),
						0,
						settings.CollisionLayer
					);
			IsTouchRightWall = Physics2D.OverlapBox( // Create box on right of gameobject to check wall
					new(transform.position.x + collider.offset.x
						+ collider.size.x / 2/*Make it at right side of gameobject*/
						+ settings.CollisionCheckThickness / 2
						+ settings.CollisionCheckOffset,
						transform.position.y + collider.offset.y),
					new(settings.CollisionCheckThickness, collider.size.y - settings.CollisionCheckOffset),
					0,
					settings.CollisionLayer
				);
			IsTouchLeftWall = Physics2D.OverlapBox( // Create box on ledt side of gameobject to check wall
						new(transform.position.x + collider.offset.x
							- collider.size.x / 2/*Make it at left side of gameobject*/
							- settings.CollisionCheckThickness / 2
							- settings.CollisionCheckOffset,
							transform.position.y + collider.offset.y),
						new(settings.CollisionCheckThickness, collider.size.y - settings.CollisionCheckOffset),
						0,
						settings.CollisionLayer
					);
		}

		/// <summary>
		/// Update current <see cref="Speed"/>
		/// </summary>
		private void UpdateSpeed()
		{
			if (Direction == MovementDirection.None)
			{
				Speed = 0f;
				return;
			}

			Speed = (IsRunning ? settings.RunSpeed : settings.MoveSpeed) * (int)Direction;

			if ((IsTouchLeftWall && Direction == MovementDirection.Left) || (IsTouchRightWall && Direction == MovementDirection.Right)) // Can't move in wall
				Speed = 0f;
		}
	}
}
