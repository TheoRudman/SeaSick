using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Player player;
    public List<int> scores;


    public int p1score = 0;
    public int p2score = 0;
    public int p3score = 0;
    public int p4score = 0;

    public int plusPointNum;
    public int thisScore;

    public TMP_Text leaderboardOne;
    public TMP_Text leaderboardTwo;
    public TMP_Text leaderboardThree;
    public TMP_Text leaderboardFour;
    
    public string topPlayer;


    void Start()
    {
        UpdateLeaderboard();
    }


    void AddScores()
    {
        scores.Add(p1score);
        scores.Add(p2score);
        scores.Add(p3score);
        scores.Add(p4score);

        scores.Sort();
    }

    public void AdjustPoints(int point)
    {
        if (player.currentPlayer.tag == "Player1")
        {
            p1score += point;
        }
        if (player.currentPlayer.tag == "Player2")
        {
            p2score += point;
        }
        if (player.currentPlayer.tag == "Player3")
        {
            p3score += point;
        }
        if (player.currentPlayer.tag == "Player4")
        {
            p4score += point;
        }
        UpdateLeaderboard();
    }

    public void AdjustPointDirectly(int playerNum, int point)
    {
        if (playerNum == 1)
        {
            p1score += point;
            //Debug.Log("p1 gets points");
        }
        if (playerNum == 2)
        {
            p2score += point;
            //Debug.Log("p2 gets points");
        }
        if (playerNum == 3)
        {
            p3score += point;
            //Debug.Log("p3 gets points");
        }
        if (playerNum == 4)
        {
            p4score += point;
            //Debug.Log("p4 gets points");
        }
        UpdateLeaderboard();
    } 

    public void UpdateLeaderboard()
    {
        AddScores();

        leaderboardOne.SetText(GameData.p1Name + " : " + (p1score.ToString()));
        leaderboardTwo.SetText(GameData.p2Name + " : " + (p2score.ToString()));
        leaderboardThree.SetText(GameData.p3Name + " : " + (p3score.ToString()));
        leaderboardFour.SetText(GameData.p4Name + " : " + (p4score.ToString()));
    }

    public void FindTopPlayer()
    {
        if((p1score > p2score) && (p1score > p3score) && ( p1score > p4score))
        {
            topPlayer = "Player One";
        }
        if ((p2score > p1score) && (p2score > p3score) && (p2score > p4score))
        {
            topPlayer = "Player Two";
        }
        if ((p3score > p2score) && (p3score > p1score) && (p3score > p4score))
        {
            topPlayer = "Player Three";
        }
        if ((p4score > p2score) && (p4score > p3score) && (p4score > p1score))
        {
            topPlayer = "Player Four";
        }
        else
        {
            topPlayer = "DRAW";
        }
    }
}
