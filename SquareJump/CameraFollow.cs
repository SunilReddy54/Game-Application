using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareJump
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject player;

        private void Start()
        {
            if (CameraManager.Instance != null)
            {
                CameraManager.Instance.CameraEnable = false;
            }
        }

        private void LateUpdate()
        {
            //if(Spawner.Instance.isInitialized)
            if(player != null)
                transform.position = new Vector3(
                    transform.position.x,
                    player.transform.position.y + 2f,
                    transform.position.z);
        }
    }
}
