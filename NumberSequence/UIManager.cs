using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Events;
using Game.Shared;

public class UIManager : MonoBehaviour
{
    private NumberSequenceRequestData requestData;

    [SerializeField]
    private TextMeshProUGUI numberText = null;
    [SerializeField]
    private GameObject gamePanel = null;
    [SerializeField]
    private Text seqText = null;

    [Header("Level Loader Events")]
    [SerializeField]
    private LevelLoadEventSO LoadLevel = null;

    private void Start()
    {
        requestData = new NumberSequenceRequestData();

        numberText.enabled = false;
        gamePanel.SetActive(false);

        GetNewNumber();
    }

    public void GetNewNumber()
    {
        int n = LogicController.GenerateNewNumber();
        DisplayNumber(n);
    }

    public void DisplayNumber(int i)
    {
        gamePanel.SetActive(false);
        seqText.text = "";
        numberText.text = i.ToString();
        numberText.enabled = true;

        StartCoroutine(Timer(3));
    }

    // event triggered. when time is over
    public void HideNumber()
    {

        numberText.enabled = false;
        gamePanel.SetActive(true);

        Debug.Log(LogicController.NumberSeq);
    }

    public void InputNumber(string n)
    {
        seqText.text += n;

        int correctCounter = LogicController.CheckSequence(n[0]);
        // if the length for correct answer is still left
        if (correctCounter != -1)
        {
            // if the correct answer is guessed
            if (correctCounter == 0)
            {
                GetNewNumber();
            }
        }
        else
        {
            requestData.UpdateAnswer();
            LogicController.ResetGame();
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        SimpleJSON.JSONNode node = JsonUtility.ToJson(requestData, true);

        LoadLevel.Raise(new LevelLoadOption
        {
            sceneName = LevelLoader.GAMEOVER,
            unloadPrevious = true,
            information = node
        });
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        HideNumber();
    }
}
