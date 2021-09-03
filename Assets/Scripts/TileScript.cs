using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileScript : MonoBehaviour
{
    public MenuManager menuManager;
    public DiceRoll diceRoll;
    //Player player;

    public GameObject plusTile;
    public GameObject minusTile;
    public GameObject challengeTile;
    public GameObject emptyTile;
    public GameObject itemTile;
    //public GameObject currentTile;

    public TMP_Text plusPointsText;
    public TMP_Text playerNameText;


    //int randomNum = 0;

    //public int currentTileRef;
    //int plusTileRef = 0;
    //int minusTileRef = 1;
    //int emptyTileRef = 2;
    //int itemTileRef = 3;
    //int challengeTileRef = 4;

    public int i = 999;

    void Start()
    {
        string name;
        name = gameObject.name;
        //StartCoroutine("DelayAndReset");
    }


    void Update()
    {
        //playerNameText.text = player.currentPlayer.ToString();
    }

    void Awake()
    {

    }


    //IEnumerator OpenTile()
    //{
    //    yield return new WaitForSeconds(1f);
    //}
    //IEnumerator PlusPoints()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    menuManager.OpenPlusPoints();
    //    for (int i = 0; i < 6; i++)
    //    {
    //        randomNum = Random.Range(2, 5);
    //        yield return new WaitForSeconds(0.1f);
    //        plusPointsText.text = randomNum.ToString();
    //    }
    //    yield return new WaitForSeconds(2.5f);
    //    menuManager.ClosePlusPoints();
    //    CompletedTurn();
    //    StopCoroutine("PlusPoints");
    //}
    //public void CompletedTurn()
    //{
    //    menuManager.OpenRollMenu();
    //}
}
