using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Platformer
{
    /// <summary>
    /// Used to manage level state
    /// </summary>
    public class LevelManager : MonoBehaviour
	{
		// EVENTS

		public event Action OnLevelStarted;
		public event Action OnLevelEnded;

		// INTERNAL VARIABLES

		[Inject] LevelData data;

		// PUBLIC

		public bool StartLevel()
		{
			if (!data.StartLevel())
				return false;

			OnLevelStarted?.Invoke();
			return true;
		}
		public bool EndLevel()
		{
			if (!data.EndLevel())
				return false;

			OnLevelEnded?.Invoke();
			return true;
		}

	}
}
