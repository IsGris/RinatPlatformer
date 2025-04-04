using System;
using UnityEngine;
using Zenject;

namespace Platformer
{
    /// <summary>
    /// Represents fruit that can be collected by player
    /// </summary>
    public class FruitCollectable : MonoBehaviour, ICollectable
	{
        public event Action OnCollected;

        [Inject] LevelData levelData;
        private bool IsCollected = false;

        [ContextMenu("Collect")]
        public void Collect()
        {
            if (IsCollected)
                return;

            levelData.AddFruit();
            IsCollected = true;
            OnCollected?.Invoke();
        }
    }
}
