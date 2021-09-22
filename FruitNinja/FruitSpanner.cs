using Game.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FruitSpanner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public Transform[] spawnPoints;

    private float startTime;

    private float minDelay = .25f;
    private float maxDelay = 1f;

    [SerializeField] private Text pointText = null;
    [SerializeField] private Text lifeText = null;

    int points = 0;
    int life = 3;

    private Coroutine spawnRoutine;
    [SerializeField] private LevelLoadEventSO OnLevelLoad;

    private void Awake()
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.CameraEnable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        pointText.text = "Points: 0";
        lifeText.text = "Life: 3";
        spawnRoutine = StartCoroutine(SpawnFruits()); 
    }

    void FruitCut()
    {
        points++;
        pointText.text = $"Points: {points}";
    }

    void FruitNoCut()
    {
        life--;
        lifeText.text = $"Life: {life}";
        if(life <= 0)
        {
            Time.timeScale = 0;
            StopCoroutine(spawnRoutine);

            // Game Over
            if (CameraManager.Instance != null)
            {
                CameraManager.Instance.CameraEnable = true;
            }
            float endTime = Time.time - startTime;
            FruitNinjaRequestData data = new FruitNinjaRequestData
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

    IEnumerator SpawnFruits ()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation)
                .GetComponent<Fruit>()
                .Init(FruitCut, FruitNoCut);
        }
    }
}

class FruitNinjaRequestData
{
    public string gameTime;
    public int points;
}