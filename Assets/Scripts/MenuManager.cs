using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject rollMenuUI;
    public GameObject plusPointsUI;
    public GameObject minusPointsUI;
    public GameObject challengeUI;
    public GameObject challengeOneUI;
    public GameObject challengeTwoUI;
    public GameObject challengeOneGameUI;
    public GameObject itemTileUI;
    public GameObject emptyTileUI;
    public GameObject roundOverUI;
    public GameObject nextRoundUI;
    public GameObject gameOverUI;
    public GameObject pointItemUI;
    public GameObject viewBoardButton;
    public GameObject returnToRollButton;
    public GameObject showBoardUI;

    public PlusPoints plusPoints;
    public MinusPoints minusPoints;
    public ChallengeTile challengeTile;
    public ItemTile itemTile;
    public EmptyTile emptyTile;
    public Player player;
    public DiceRoll diceRoll;
    public ScoreManager scoreManager;
    public GameManager gameManager;
    public StealPoints stealPoints;

    public Text playerNameText;
    public TMP_Text plusPointsText;
    public TMP_Text minusPointsText;
    public Text roundWinnerText;
    public TMP_Text gameOverName;
    public TMP_Text viewBoardText;

    public AudioSource minusPointClip;
    public AudioSource plusPointsClip;


    public string playerName;
    public int randomNum = 666;
    public int tileNum = 9;
    public bool endOfRound = false;
    int roundCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void RetrievePlayerName(string name)
    {
        playerNameText.text = name;
    }

    public void LaunchTile()
    {
        if (endOfRound == false)
        {
            //Debug.Log("tilenum = " + tileNum);

            if (tileNum == 0)
            {
                plusPoints.BeginPlusPoints();
                //TileComplete();
            }
            if (tileNum == 1)
            {
                minusPoints.BeginMinusPoints();
                //minusPointClip.Play();
                //TileComplete();
            }
            if (tileNum == 2)
            {
                emptyTile.BeginEmpty();
                //TileComplete();
            }
            if (tileNum == 3)
            {
                itemTile.BeginItemTile();
                //TileComplete();
            }
            if (tileNum == 4)
            {
                challengeTile.BeginChallenge();
                //TileComplete();
            }
        }
    }

    public void ShowBoard()
    {
        rollMenuUI.SetActive(false);
        viewBoardButton.SetActive(false);
        returnToRollButton.SetActive(true);
        //viewBoardText.text = ("RETURN TO ROLL");
    }
    public void ReturnToRoll()
    {
        rollMenuUI.SetActive(true);
        returnToRollButton.SetActive(false);
        viewBoardButton.SetActive(true);
        //viewBoardText.text = ("SHOW BOARD");
    }

    public void TileComplete()
    {
        plusPointsText.text = (" ");
        minusPointsText.text = (" ");

        //scoreManager.AdjustPoints(1);
        gameManager.SwapPlayer();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Quit()
    {
        Debug.Log("Closing Game");
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        //EditorSceneManager.LoadScene("");
    }
    public void CloseRollMenu()
    {
        rollMenuUI.SetActive(false);
        diceRoll.diceRollButton.interactable = true;
        viewBoardButton.SetActive(false);
        returnToRollButton.SetActive(false);
    }
    public void OpenRollMenu()
    {
        player.UpdateCurrentPlayer();
        diceRoll.rollNumText.text = " ";
        rollMenuUI.SetActive(true);
        viewBoardButton.SetActive(true);
    }
    public void OpenPlusPoints()
    {
        plusPointsUI.SetActive(true);
    }
    public void ClosePlusPoints() 
    {
        plusPointsUI.SetActive(false);
    }
    public void OpenMinusPoints()
    {
        minusPointsUI.SetActive(true);
    }
    public void CloseMinusPoints()
    {
        minusPointsUI.SetActive(false);
    }
    public void OpenChallenge()
    {
        challengeUI.SetActive(true);
    }
    public void CloseChallenge()
    {
        challengeUI.SetActive(false);
    }
    public void OpenChallengeOne()
    {
        challengeOneUI.SetActive(true);
    }
    public void CloseChallengeOne()
    {
        challengeOneUI.SetActive(false);
    }
    public void CloseChallengeOneGame()
    {
        challengeOneGameUI.SetActive(false);
    }
    public void OpenChallengeOneGame()
    {
        challengeOneGameUI.SetActive(true);
    }
    public void OpenItemTile()
    {
        itemTileUI.SetActive(true);
    }
    public void CloseItemTile()
    {
        itemTileUI.SetActive(false);
    }
    public void OpenEmptyTile()
    {
        emptyTileUI.SetActive(true);
    }
    public void CloseEmptyTile()
    {
        emptyTileUI.SetActive(false);
    }
    public void OpenRoundOver()
    {
        roundOverUI.SetActive(true);
    }
    public void CloseRoundOver()
    {
        roundOverUI.SetActive(false);
    }
    public void OpenNextRound()
    {
        nextRoundUI.SetActive(true);
        //gameManager.RestartTiles();
    }
    public void CloseNextRound()
    {
        nextRoundUI.SetActive(false);
    }
    public void OpenGameOver()
    {
        gameOverUI.SetActive(true);
        scoreManager.FindTopPlayer();
        gameOverName.text = scoreManager.topPlayer;
    }
    public void CloseGameOver()
    {
        gameOverUI.SetActive(false);
    }

    public void OpenPointsItem()
    { 
        pointItemUI.SetActive(true);
        stealPoints.EnableButtons();
        stealPoints.DisablePlayerButton();
        stealPoints.SetPlayerName();
    }
    public void ClosePointsItem()
    {
        pointItemUI.SetActive(false);
    }
        
    public void RoundOver()
    {

        endOfRound = true;
        roundWinnerText.text = player.currentPlayer.name;
        OpenRoundOver();
        scoreManager.AdjustPoints(10);
        StartCoroutine("ResetRound");
        roundCount++;
    }
    IEnumerator ResetRound()
    {
        yield return new WaitForSeconds(3f);
        CloseRoundOver();
        diceRoll.doubleRoll = false;
        yield return new WaitForSeconds(0.5f);
        if (roundCount == 1)
        {
            Debug.Log("Round reset, roundCount = " + roundCount);
            endOfRound = false;
            diceRoll.rollMenuCount = 0;
            gameManager.player.ResetToPlayerOne();
            OpenNextRound();
        }
        else
        {
            OpenGameOver();
        }
        StopCoroutine("ResetRound");
    }
}

