using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChallengeTile : MonoBehaviour
{
    public GameManager gameManager;
    public MenuManager menuManager;
    public ScoreManager scoreManager;

    GameObject ChallengeOneWinner;

    public Text firstPlayer;
    public Text secondPlayer;

    public Text challengeP1;
    public Text challengeP2;
    public TMP_Text timer;
    public Text challengeOneWinnerText;
    public TMP_Text chOneP1countText;
    public TMP_Text chOneP2countText;


    public Text challengeOneGameP1;
    public Text challengeOneGameP2;
    public Text challengeOneWINNER;

    bool challengeStarted = false;
    int chOneP1score;
    int chOneP2score;


    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && (challengeStarted == true))
        {
            chOneP1score++;
            chOneP1countText.text = chOneP1score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.D) && (challengeStarted == true))
        {
            chOneP2score++;
            chOneP2countText.text = chOneP2score.ToString();
        }
        if ((chOneP1score >= 10))
        {
            ChallengeOneWinner = gameManager.FirstPlayer;
            Debug.Log(ChallengeOneWinner.name.ToString()+"  challengeOneWinner");
            FirstPlayerWins();
        }
        if ((chOneP2score >= 10))
        {
            ChallengeOneWinner = gameManager.SecondPlayer;
            Debug.Log(ChallengeOneWinner.name.ToString() + "  challengeOneWinner");
            SecondPlayerWins();
        }
    }

    public void BeginChallenge()
    {
        PlayerNames();
        StartCoroutine("StartChallenge");
    }

    public void PlayerNames()
    {
        gameManager.CheckNextPlayer();
        firstPlayer.text = gameManager.FirstPlayer.name.ToString();
        secondPlayer.text = gameManager.SecondPlayer.name.ToString();
        challengeOneGameP1.text = gameManager.FirstPlayer.name.ToString();
        challengeOneGameP2.text = gameManager.SecondPlayer.name.ToString();
    }

    IEnumerator StartChallenge()
    {
        yield return new WaitForSeconds(0.2f);
        menuManager.OpenChallenge();
        yield return new WaitForSeconds(1.5f);

        int num = 0;
        if (num == 0)
        {
            challengeP1.text = gameManager.FirstPlayer.name.ToString();
            challengeP2.text = gameManager.SecondPlayer.name.ToString();
            menuManager.CloseChallenge();
            menuManager.OpenChallengeOne();
            StartCoroutine("StartChallengeOne");
        }
        if (num == 1)
        {
        }

        //Debug.Log("FinishedChallenge");
        menuManager.CloseChallenge();

        StopCoroutine("StartChallenge");
    }

    IEnumerator StartChallengeOne()
    {
        int count = 4;
        timer.text = count.ToString();
        for (int i = 0; i < 4; i++)
        {
            count--;
            yield return new WaitForSeconds(1f);
            timer.text = count.ToString();
        }

        menuManager.CloseChallengeOne();
        menuManager.OpenChallengeOneGame();
        challengeOneWINNER.gameObject.SetActive(false);
        challengeStarted = true;

        StopCoroutine("StartChallengeOne");
    }

    IEnumerator EndChallengeOne()
    {
        yield return new WaitForSeconds(2.5f);

        menuManager.CloseChallengeOneGame();
        menuManager.TileComplete();
        timer.text = ("0");
        chOneP1score = 0;
        chOneP2score = 0;
        chOneP1countText.text = chOneP1score.ToString();
        chOneP2countText.text = chOneP2score.ToString();
        challengeOneWinnerText.text = ("START!");
        StopCoroutine("EndChallengeOne");
    }

    void FirstPlayerWins()
    {
        challengeStarted = false;
        chOneP1score = 0;
        chOneP2score = 0;

        challengeOneWinnerText.text = (gameManager.FirstPlayer.name.ToString());
        challengeOneWINNER.gameObject.SetActive(true);

        if (gameManager.firstPlayerRef == 0)
        {
            scoreManager.AdjustPointDirectly(1, 5);
        }
        if (gameManager.firstPlayerRef == 1)
        {
            scoreManager.AdjustPointDirectly(2, 5);
        }
        if (gameManager.firstPlayerRef == 2)
        {
            scoreManager.AdjustPointDirectly(3, 5);
        }
        if (gameManager.firstPlayerRef == 3)
        {
            scoreManager.AdjustPointDirectly(4, 5);
        }
        StartCoroutine("EndChallengeOne");
    }

    void SecondPlayerWins()
    {
        challengeStarted = false;
        chOneP1score = 0;
        chOneP2score = 0;

        challengeOneWinnerText.text = (gameManager.SecondPlayer.name.ToString());
        challengeOneWINNER.gameObject.SetActive(true);

        if (gameManager.secondPlayerRef == 0)
        {
            scoreManager.AdjustPointDirectly(1, 5);
        }
        if (gameManager.secondPlayerRef == 1)
        {
            scoreManager.AdjustPointDirectly(2, 5);
        }
        if (gameManager.secondPlayerRef == 2)
        {
            scoreManager.AdjustPointDirectly(3, 5);
        }
        if (gameManager.secondPlayerRef == 3)
        {
            scoreManager.AdjustPointDirectly(4, 5);
        }

        StartCoroutine("EndChallengeOne");
    }
}
