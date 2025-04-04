using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Represents that owner of this component can collect <see cref="ICollectable"/>
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Collector : MonoBehaviour
    {
		private void OnTriggerEnter2D(Collider2D collision)
		{
            var collectable = collision.gameObject.GetComponent<ICollectable>();
            if (collectable == null)
                return;

            collectable.Collect();
		}
	}
}
