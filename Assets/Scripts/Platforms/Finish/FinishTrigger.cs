using UnityEngine;
using Zenject;

namespace Platformer
{
	/// <summary>
	/// Activates finish when player enter in trigger
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class FinishTrigger : MonoBehaviour
	{
		[Inject] LevelManager levelManager;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
				ApplyFinish();
		}

		private void ApplyFinish() =>
			levelManager.EndLevel();
	}
}
