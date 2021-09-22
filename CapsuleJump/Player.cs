using Game.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/*using Game.Events;*/

public class Player : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;

    private bool beginGame = false;
    [SerializeField] private float startTime = 0;

    private float gameTime;
    private float GameTime
    {
        get { return gameTime; }
        set
        {
            gameTime = value;
            timerText.text = "TIME: " + value.ToString("0.0");
        }
    }

    private int coins = 0;
    private int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            coinsText.text = $"COIN: {coins}";
        }
    }

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private LayerMask groundMask;

    private bool jumpKeyWasPressed;
    private bool canJump = false;

    [SerializeField] private Text timerText;
    [SerializeField] private Text coinsText;

    [SerializeField] private LevelLoadEventSO OnLevelLoad;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.CameraEnable = false;
        }
    }

    private void Start()
    {
        StartCoroutine(BeginGameRoutine(startTime));
    }

    private void Update()
    {
        if (!beginGame) return;
        GameTime += Time.deltaTime; 
    }

    public void Jump()
    {
        if (canJump)
            jumpKeyWasPressed = true;
    }

    private void FixedUpdate()
    {
        if (!beginGame) return;

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpKeyWasPressed = false;
        }

        float yVelocity = rigidbodyComponent.velocity.y;
        rigidbodyComponent.velocity = new Vector3(speed, yVelocity, 0);
    }

    private IEnumerator BeginGameRoutine(float time)
    {
        GameTime = time;
        coinsText.enabled = false;

        while(GameTime > 0)
        {
            yield return new WaitForEndOfFrame();
            GameTime -= Time.deltaTime;
        }

        GameTime = 0;
        coinsText.enabled = true;
        beginGame = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            Coins++;
        }
        else if (other.gameObject.name.Contains("Floor"))
            canJump = true;
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Floor"))
            canJump = false;
    }

    private void OnDestroy()
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.CameraEnable = true;
        }

        CapsuleJumpRequestData data = new CapsuleJumpRequestData();
        data.gameTime = GameTime.ToString("0.00");
        data.coins = Coins;
        OnLevelLoad.Raise(new LevelLoadOption
        {
            information = JsonUtility.ToJson(data, true),
            sceneName = LevelLoader.GAMEOVER,
            unloadPrevious = true
        });
    }
}

class CapsuleJumpRequestData
{
    public string gameTime;
    public int coins;
}