using UnityEngine;

namespace Bonuses
{
    public class Bonus : MonoBehaviour
    {
        public void Initialize(Vector3 initialPosition)
        {
            transform.position = initialPosition;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                gameObject.SetActive(false);
            }
        }
    }
}