using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTile : MonoBehaviour
{
    //public Player player;
    //public GameManager gameManager;
    public MenuManager menuManager;
    public DiceRoll diceRoll;

    string doubleRollstr = "DOUBLE ROLL";
    string stealPointstr = "STEAL POINTS";

    public TMP_Text itemText;
    void Start()
    {
        
    }


    void Update()
    {
    }

    public void BeginItemTile()
    {
        StartCoroutine("StartItemTile");
        itemText.text = (" ");
    }
    IEnumerator StartItemTile()
    {
        yield return new WaitForSeconds(0.2f);
        menuManager.OpenItemTile();

        int num = Random.Range(0, 3);
        //int num = 0;
        if (num == 0)
        { itemText.text = doubleRollstr;
            StartCoroutine("MovementItem");
        }
        if (num >= 1)
        { itemText.text = stealPointstr;
            StartCoroutine("PointsItem");
        }

        StopCoroutine("StartChallenge");  
    }


    IEnumerator MovementItem()
    {
        diceRoll.doubleRoll = true;
        yield return new WaitForSeconds(2.5f);
        menuManager.CloseItemTile();
        //diceRoll.CheckRemainingMap();
        menuManager.OpenRollMenu();
    }

    IEnumerator PointsItem()
    {
        yield return new WaitForSeconds(2.5f);
        menuManager.CloseItemTile();
        menuManager.OpenPointsItem();
    }
}
