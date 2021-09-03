using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinusPoints : MonoBehaviour
{
    public MenuManager menuManager;
    //public GameManager gameManager;
    public ScoreManager scoreManager;

    int randomNum = 0;
    public TMP_Text plusPointsText;
    private int field;

    public void BeginMinusPoints()
    {
        StartCoroutine("StartMinusPoints");
    }

    IEnumerator StartMinusPoints()
    {

        yield return new WaitForSeconds(0.5f);
        menuManager.OpenMinusPoints();
        for (int i = 0; i < 6; i++)
        {
            randomNum = Random.Range(-1, -3);
            yield return new WaitForSeconds(0.1f);
            plusPointsText.text = randomNum.ToString();
        }

        //scoreManager.minusPointNum = randomNum;
        yield return new WaitForSeconds(1.5f);

        scoreManager.AdjustPoints(randomNum);

        menuManager.CloseMinusPoints();
        menuManager.TileComplete();

        Debug.Log("FinishedPlusPoints");
    }
}
