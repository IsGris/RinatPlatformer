using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Platformer
{
    public class FruitCollectableView : MonoBehaviour
    {
        [Inject] FruitCollectable model;
        [Inject] Animator animator;

        void Awake()
        {
			model.OnCollected += OnCollected;
        }

		private void OnCollected()
        {
            animator.SetTrigger("Collect");
        }

        /// <summary>
        /// Called when Collect animation is ended using Animator Event<br/>
        /// Removes current GameObject
        /// </summary>
        public void RemoveGameObject() =>
            Destroy(gameObject);
    }
}
