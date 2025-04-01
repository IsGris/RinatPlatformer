using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Platformer
{
	[RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
		// EVENTS

		public event Action<float> OnMoveInput;
		public event Action OnJump;
		public event Action OnRunStart;
		public event Action OnRunEnd;

		// VARIABLES

		[Inject(Id = "MoveActionName")] private readonly string _moveActionName;
		[Inject(Id = "JumpActionName")] private readonly string _jumpActionName;
		[Inject(Id = "RunActionName")] private readonly string _runActionName;

		[Inject] private readonly PlayerInput _playerInput;

		private InputAction _moveAction;
		private InputAction _jumpAction;
		private InputAction _runAction;

		// UNITY FUNCTIONS

		private void Start()
		{
			Debug.Log("Started initializing player input");

			// Init input
			_moveAction = InitAction(_moveActionName);
			if (_moveAction != null) // Check does action initialized properly
			{
				if (_moveAction.expectedControlType == "Axis") // Check does input action returns what we need
				{
					_moveAction.performed += _context =>
						OnMoveInput.Invoke(_context.ReadValue<float>());
					_moveAction.canceled += _context =>
						OnMoveInput.Invoke(0);
				}
				else
					Debug.LogErrorFormat("Input Action {0} have wrong ControlType. Have: {1}, Expected: {2}",
						nameof(_moveAction), _moveAction.expectedControlType, "Axis");
			}

			_jumpAction = InitAction(_jumpActionName);
			if (_jumpAction != null) // Check does action initialized properly
				_jumpAction.performed += _ => OnJump.Invoke();

			_runAction = InitAction(_runActionName);
			if (_runAction != null) // Check does action initialized properly
			{
				_runAction.performed += _ => OnRunStart.Invoke();
				_runAction.canceled += _ => OnRunEnd.Invoke();
			}

			Debug.Log("Player input initialized");
		}

		// PROTECTED FUNCTIONS

		/// <summary>
		/// Initialize and get InputAction using <see cref="_playerInput"/> and ActionName
		/// </summary>
		/// <param name="ActionName">Name of input action</param>
		/// <returns>Input action if found otherwise null</returns>
		protected InputAction InitAction(in string ActionName)
		{
			var result = _playerInput.actions.FindAction(ActionName);
			if (result == null)
				Debug.LogWarningFormat("Error on initializing input action with name: {0}", ActionName);
			return result;
		}
	}
}
