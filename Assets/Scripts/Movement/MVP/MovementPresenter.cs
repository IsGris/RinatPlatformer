using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    public class MovementPresenter : MonoBehaviour
    {
		// INTERNAL VARIABLES

		[Inject] protected MovementModel model;
		[Inject] protected MovementView view;

		// UNITY

		private void OnEnable() =>
			SubscribeToEvents();

		private void OnDisable() =>
			UnsubscribeFromEvents();

		private void OnDestroy() =>
			UnsubscribeFromEvents();

		// PRIVATE

		private void SubscribeToEvents()
		{
			model.OnJump += Model_OnJump;
			model.OnDirectionChange += Model_OnDirectionChange;
			model.OnRunStatusChange += Model_OnRunStatusChange;
			model.OnFallStatusChange += Model_OnFallStatusChange;
		}

		private void UnsubscribeFromEvents()
		{
			model.OnJump -= Model_OnJump;
			model.OnDirectionChange -= Model_OnDirectionChange;
			model.OnRunStatusChange -= Model_OnRunStatusChange;
			model.OnFallStatusChange -= Model_OnFallStatusChange;
		}

		// EVENTS

		private void Model_OnJump() =>
			view.ActivateJump();

		private void Model_OnDirectionChange(MovementDirection NewDirection)
		{
			if (NewDirection == MovementDirection.Right)
			{
				view.SetFacingRightState(true);
				view.SetMovingState(true);
			}
			else if (NewDirection == MovementDirection.Left)
			{
				view.SetFacingRightState(false);
				view.SetMovingState(true);
			} 
			else if (NewDirection == MovementDirection.None)
			{
				view.SetMovingState(false);
			}
		}
		
		private void Model_OnRunStatusChange(bool NewStatus) =>
			view.SetRunState(NewStatus);
		
		private void Model_OnFallStatusChange(bool NewStatus) =>
			view.SetFallState(NewStatus);
	}
}
