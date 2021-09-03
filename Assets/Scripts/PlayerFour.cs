using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFour : MonoBehaviour
{
    public Rigidbody rb;
    public MenuManager menuManager;
    public GameObject nameText;

    int tileNumber = 5;
    public int score;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nameText != null)
        {
            nameText.transform.LookAt(Camera.main.transform.position);
            nameText.transform.Rotate(0,180,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlusTile")
        {
            tileNumber = 0;
           // Debug.Log("PLAYER FOUR Landed on PLUS");
            //Debug.Log(tileNum);
            menuManager.tileNum = 0;

        }
        if (other.tag == "MinusTile")
        {
            tileNumber = 1;
           // Debug.Log("PLAYER FOUR Landed on MINUS");
            //Debug.Log(tileNum);
            menuManager.tileNum = 1;
        }
        if (other.tag == "EmptyTile")
        {
            tileNumber = 2;
           // Debug.Log("PLAYER FOUR Landed on EMPTY");
            //Debug.Log(tileNum);
            menuManager.tileNum = 2;
        }
        if (other.tag == "ItemTile")
        {
            tileNumber = 3;
           // Debug.Log("PLAYER FOUR Landed on ITEM");
            //Debug.Log(tileNum);
            menuManager.tileNum = 3;
        }
        if (other.tag == "ChallengeTile")
        {
            tileNumber = 4;
          //  Debug.Log("PLAYER FOUR Landed on CHALLENGE");
            //Debug.Log(tileNum);
            menuManager.tileNum = 4;
        }
        if (tileNumber == 6)
        {
            Debug.Log("PLAYER FOUR ERROR: TILE NUM RESET");

        }
    }
}
