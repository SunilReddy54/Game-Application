using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareJump
{
    public class PlatformBounce : MonoBehaviour
    {
        [SerializeField]
        private bool startPlatform = false;
        public float jumpForce = 950f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb.velocity.y <= 0.5f)
            {
                rb.AddForce(Vector3.up * jumpForce);
            }

            if (startPlatform)
            {
                Spawner.Instance.Initialize(gameObject);
                startPlatform = false;
            }
        }
    }
}
