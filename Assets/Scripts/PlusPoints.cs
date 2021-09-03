using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusPoints : MonoBehaviour
{
    public MenuManager menuManager;
    //public GameManager gameManager;
    public ScoreManager scoreManager;

    int randomNum = 0;
    public TMP_Text plusPointsText;

    public void BeginPlusPoints()
    {
        StartCoroutine("StartPlusPoints"); 
    }

    IEnumerator StartPlusPoints()
    {

        yield return new WaitForSeconds(0.5f);
        menuManager.OpenPlusPoints();
        for (int i = 0; i < 6; i++)
        {
            randomNum = Random.Range(2, 5);
            yield return new WaitForSeconds(0.1f);
            plusPointsText.text = randomNum.ToString();
        }

        scoreManager.plusPointNum = randomNum;
        yield return new WaitForSeconds(1.5f);

        scoreManager.AdjustPoints(randomNum);

        menuManager.ClosePlusPoints();
        menuManager.TileComplete();

        Debug.Log("FinishedPlusPoints");
    }
}
