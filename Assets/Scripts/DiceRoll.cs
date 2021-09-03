using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public MenuManager menuManager;
    public GameManager gameManager;
    public Player myPlayer;
    public Text rollNumText;
    public Text playerNameText;
    public Button diceRollButton;

    public int rollNumber = 2;
    public float randomNumTimer = 0.1f;
    public int randomNum = 2;
    public int rollMenuCount = 0;
    public bool doubleRoll = false;
    public bool outOfMap = false;
    public bool mapCompleted = false;
    int remainingTiles = 6;

    void Start()
    {
    }

    void Update()
    {
        playerNameText.text = myPlayer.currentPlayer.ToString();
    }

    public void StartRoll()
    {
        diceRollButton.interactable = false;
        StartCoroutine("RandomNum");  }

    public void CheckRemainingMap()
    {
        if (mapCompleted == false)
        {
            remainingTiles = gameManager.spawnedTiles.Count - myPlayer.currentPlayerMoveCount;
            if (remainingTiles < 6)
            { outOfMap = true; }
            if (remainingTiles >= 6)
            { outOfMap = false; }
        }
    }

    IEnumerator RandomNum()
    {
        CheckRemainingMap();
        int previousNum = 0;
        if (outOfMap == false)
        {
            Debug.Log("Normal move limit");
            for (int i = 0; i < 6; i++)
            {
                randomNum = Random.Range(1, 7);
                if (previousNum == randomNum)
                {
                    randomNum = Random.Range(1, 7);
                }
                previousNum = randomNum;
                yield return new WaitForSeconds(randomNumTimer);
                rollNumText.text = randomNum.ToString();
            }
        }
        else
        {
            Debug.Log("Not enough tiles remain, new limit =" + remainingTiles);
            for (int i = 0; i < 6; i++)
            {
                randomNum = Random.Range(1, remainingTiles);
                yield return new WaitForSeconds(randomNumTimer);
                rollNumText.text = randomNum.ToString();
            }
        }
        StartCoroutine("SetRollNum");
        StopCoroutine("RandomNum");
    }
    IEnumerator SetRollNum()
    {
        myPlayer.diceRoll = randomNum;
        //myPlayer.diceRoll = 6;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("RollTheDice");
        StopCoroutine("SetRollNum");
    }
    IEnumerator RollTheDice()
    {
        menuManager.CloseRollMenu();
        if (doubleRoll == false)
        { rollMenuCount++; }
        doubleRoll = false;
        yield return new WaitForSeconds(0.5f);
        myPlayer.StartCoroutine("MovePlayer");
        StopCoroutine("RollTheDice");

    }
}
