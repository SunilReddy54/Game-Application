using Game.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SquareJump
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float moveInput;
        [SerializeField]
        private float speed = 10.0f;

        private int points = 0;
        public int Points
        {
            get => points;
            set
            {
                points++;
                pointText.text = $"Points: {points}";
            }
        }

        public Text pointText;

        private float startTime;

        [SerializeField] private LevelLoadEventSO OnLevelLoad;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startTime = Time.time;
        }

        private void Update()
        {
            moveInput = 0;
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).position.x > Screen.width / 2)
                    moveInput = 1;
                else
                    moveInput = -1;
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if(rb.velocity.y < -20.0f)
            {
                // Died
                Time.timeScale = 0;
                if (CameraManager.Instance != null)
                {
                    CameraManager.Instance.CameraEnable = true;
                }
                float endTime = Time.time - startTime;
                SquareJumpRequestData data = new SquareJumpRequestData
                {
                    gameTime = endTime.ToString("0.00"),
                    points = points
                };
                OnLevelLoad.Raise(new LevelLoadOption
                {
                    information = JsonUtility.ToJson(data, true),
                    sceneName = LevelLoader.GAMEOVER,
                    unloadPrevious = true
                });
            }
        }
    }

    class SquareJumpRequestData
    {
        public string gameTime;
        public int points;
    }
}
