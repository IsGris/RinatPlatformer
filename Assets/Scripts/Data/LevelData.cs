using System;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Contains data of current level that player are playing
    /// </summary>
    public class LevelData : MonoBehaviour
    {
        // EVENTS

        /// <summary>
        /// float - <see cref="Time.time"/> when level started
        /// </summary>
        public event Action<float> OnLevelStarted;
        /// <summary>
        /// float - <see cref="Time.time"/> when level ended
        /// </summary>
        public event Action<float> OnLevelEnded;
        /// <summary>
        /// int - new <see cref="FruitsCollected"/> value
        /// </summary>
        public event Action<int> OnFruitCountChange;

        // VARIABLES

        /// <summary>
        /// <see cref="Time.time"/> when level started
        /// </summary>
        public float StartTime
        {
            get => _startTime;
            private set
            {
                _startTime = value;
                if (float.IsNormal(value))
                    OnLevelStarted?.Invoke(value);
            }
        }
        /// <summary>
        /// <see cref="Time.time"/> when level ended
        /// </summary>
        public float EndTime
        {
            get => _endTime;
            private set
            {
				_endTime = value;
                if (float.IsNormal(value))
                    OnLevelEnded?.Invoke(value);
            }
	    }
        /// <summary>
        /// How many fruits collected on level
        /// </summary>
        public int FruitsCollected
        {
            get => _fruitsCollected;
            private set
            {
				_fruitsCollected = value;
				OnFruitCountChange?.Invoke(value);
			}
        }

        // INTERNAL VARIABLES

        private float _startTime;
        private float _endTime;
        private int _fruitsCollected;

        // PUBLIC

        public bool StartLevel()
        {
            if (float.IsNormal(StartTime)) // Level already started
                return false;

            StartTime = Time.time;
            return true;
        }

		public bool EndLevel()
		{
			if (float.IsNormal(EndTime)) // Level already ended
				return false;

			EndTime = Time.time;
			return true;
		}

		public void AddFruit() =>
            FruitsCollected += 1;
    }
}
