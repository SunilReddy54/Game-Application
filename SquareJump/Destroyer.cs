using UnityEngine;

namespace SquareJump
{
    public class Destroyer : MonoBehaviour
    {
        private PlayerController player;
        public GameObject platformPrefab;

        private void Start()
        {
            player = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Spawner.Instance.AddNewPlatform();
            Destroy(collision.gameObject);
            player.Points++;
        }
    }
}
