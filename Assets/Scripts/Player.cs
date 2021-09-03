using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movement = 4f;
    public int diceRoll = 2;

    public GameManager manager;
    public MenuManager menuManager;
    public TileScript tileScript;
    public DiceRoll diceRollScript;

    public bool needToturn = false;
    public bool canMove = true;
    public bool pauseMovement = false;

    public Rigidbody rb1;
    public Rigidbody rb2;
    public Rigidbody rb3;
    public Rigidbody rb4;

    public GameObject currentPlayer;
    public float time = 0.40f;

    public GameObject plusTile;
    public GameObject minusTile;
    public GameObject challengeTile;
    public GameObject emptyTile;
    public GameObject itemTile;

    public GameObject p1Spawn;
    public GameObject p2Spawn;
    public GameObject p3Spawn;
    public GameObject p4Spawn;

    public int tileNum = 4;

    public List<int> allPlayersMoves;
    public int currentHighestPlayer;

    int currentCount = 0;
    int p1MoveCount;
    int p2MoveCount;
    int p3MoveCount;
    int p4MoveCount;
    public int currentPlayerMoveCount;
    int turnDegrees = 360;
    bool outOfMap = false;

    Quaternion startingRotation;

    void Start()
    {
        rb1.GetComponent<Rigidbody>();
        rb2.GetComponent<Rigidbody>(); 
        rb3.GetComponent<Rigidbody>(); 
        rb4.GetComponent<Rigidbody>();

        //
    }

    void Update()
    {
        UpdateCurrentPlayer();
        CheckForFinalSquare();
        //OutOfMapCheck();
    }

    public void GetStartingRotation()
    { startingRotation = manager.PlayerList[0].gameObject.transform.localRotation; }

    public IEnumerator MovePlayer()
    {
        if (currentCount == 0)
        { currentCount = diceRoll; }

        OutOfMapCheck();

        if (pauseMovement == false && outOfMap == false)
        {
            while ((currentCount > 0 && needToturn == false) && outOfMap == false)
            {
                UpdateCurrentPlayer();
                CheckTurn();
                OutOfMapCheck();

                if (needToturn == false && pauseMovement == false && menuManager.endOfRound == false && outOfMap == false)
                {

                    currentPlayer.transform.Translate(Vector3.forward * movement);
                    currentCount--;
                    UpdateCurrentPlayer();
                    UpdateMoveCount();
                    manager.PlayMoveSound();
                    yield return new WaitForSeconds(time);
                }
            }
            if ((currentCount > 0 && needToturn == true) && outOfMap == false)
            {
                UpdateCurrentPlayer();
                ResetDirection();
                CheckTurn();
                currentPlayer.transform.localEulerAngles = new Vector3(0, turnDegrees, 0);
                needToturn = false;
                OutOfMapCheck();

                while (currentCount > 0 && pauseMovement == false)
                {
                    ResetDirection();
                    OutOfMapCheck();
                    if (outOfMap == false)
                    {
                        currentPlayer.transform.Translate(Vector3.forward * movement);
                        manager.PlayMoveSound();
                    }

                    currentCount--;
                    UpdateCurrentPlayer();
                    UpdateMoveCount();
                    yield return new WaitForSeconds(time);
                }
            }
            if (currentCount == 0)
            {              
                UpdateCurrentPlayer();
                HighestCurrentPlayer();
                if (pauseMovement == false && pauseMovement == false)
                {
                    //Debug.Log("turn done");
                    menuManager.LaunchTile();
                    currentPlayer = manager.nextPlayer;
                }
            }
            if (outOfMap == true && pauseMovement == false)
            {
                if (menuManager.tileNum == 3)
                {
                    //Debug.Log("landed on item again");
                    menuManager.TileComplete();
                    outOfMap = false;
                }
                else if (pauseMovement == false)
                {  
                    //Debug.Log("Out of map, turn done");
                    UpdateCurrentPlayer();
                    HighestCurrentPlayer();
                    menuManager.LaunchTile();
                    currentPlayer = manager.nextPlayer;
                    outOfMap = false;
                }
            }
        }
        if (outOfMap == true)
        {
            menuManager.TileComplete();
            outOfMap = false;
        }
    }

    private void OutOfMapCheck()
    {
        CheckForFinalSquare();

        if (currentPlayer.tag == "Player1")
        {
            if (p1MoveCount >= (manager.spawnedTiles.Count) + 1)
            { outOfMap = true;
                Debug.Log("Player 1 out of map");
            }
        }
        if (currentPlayer.tag == "Player2")
        { 
            if (p2MoveCount >= manager.spawnedTiles.Count + 1)
            { outOfMap = true;
                Debug.Log("Player 2 out of map");
            }
        }
        if (currentPlayer.tag == "Player3")
        { 
            if (p3MoveCount >= manager.spawnedTiles.Count + 1)
            { outOfMap = true;
                Debug.Log("Player 3 out of map");
            }
        }
        if (currentPlayer.tag == "Player4")
        {
            if (p4MoveCount >= manager.spawnedTiles.Count + 1)
            { outOfMap = true;
                Debug.Log("Player 4 out of map");
            }
        }
    }

    private void UpdateMoveCount()
    {
        if (currentPlayer.tag == "Player1")
        { p1MoveCount++;}
        if (currentPlayer.tag == "Player2")
        { p2MoveCount++;}
        if (currentPlayer.tag == "Player3")
        { p3MoveCount++;}
        if (currentPlayer.tag == "Player4")
        { p4MoveCount++;}
    }

    public void UpdateCurrentPlayer()
    {
        if (currentPlayer.tag == "Player1")
        { currentPlayerMoveCount = p1MoveCount; }
        if (currentPlayer.tag == "Player2")
        { currentPlayerMoveCount = p2MoveCount; }
        if (currentPlayer.tag == "Player3")
        { currentPlayerMoveCount = p3MoveCount; }
        if (currentPlayer.tag == "Player4")
        { currentPlayerMoveCount = p4MoveCount; }
    }

    public void HighestCurrentPlayer()
    {
        allPlayersMoves.Clear();
        allPlayersMoves.Add(p1MoveCount);
        allPlayersMoves.Add(p2MoveCount);
        allPlayersMoves.Add(p3MoveCount);
        allPlayersMoves.Add(p4MoveCount);
        allPlayersMoves.Sort();
        currentHighestPlayer = allPlayersMoves[3];
    }

    public void ResetDirection()
    {
        if ((currentPlayerMoveCount == 0) || (currentPlayerMoveCount == 24))
        { turnDegrees = 90; }
        if ((currentPlayerMoveCount > 0) && (currentPlayerMoveCount <= 5))
        { turnDegrees = 90;}
        if ((currentPlayerMoveCount >= 6) && (currentPlayerMoveCount <= 11))
        { turnDegrees = 0;}
        if ((currentPlayerMoveCount >= 12) && (currentPlayerMoveCount <= 17))
        { turnDegrees = 270; }
        //if ((currentPlayerMoveCount >= 18) && (currentPlayerMoveCount <= 23))
        //{ turnDegrees = 180;}
        if ((currentPlayerMoveCount >= 18) && (currentPlayerMoveCount <= 24))
        { turnDegrees = 180; }
    }

    void CheckTurn()
    {
        UpdateCurrentPlayer();
        if (currentPlayerMoveCount == 6)
        {
            turnDegrees = 0;

            needToturn = true;
        }
        if (currentPlayerMoveCount == 12)
        {
            turnDegrees = 270;
            needToturn = true;
        }
        if (currentPlayerMoveCount == 18)
        {
            turnDegrees = 180;
            needToturn = true;
        }
        //if (currentPlayerMoveCount == 23 )
            if (currentPlayerMoveCount == 24)
            {
            Debug.Log("COMPLETED MAP");
            pauseMovement = true;
            currentCount = 0;
            diceRoll = 0;
            turnDegrees = 90;
            ResetPositions();
        }
    }

    void CheckForFinalSquare()
    {
        //if (currentPlayerMoveCount >= 23)
        if (currentPlayerMoveCount >= 24)
        {
            Debug.Log("COMPLETED MAP");
            pauseMovement = true;
            currentCount = 0;
            diceRoll = 0;
            turnDegrees = 90;
            ResetPositions();
        }
    }

    void ResetPositions()
    {
        Debug.Log("reset pos");

        manager.Player1.transform.position = p1Spawn.transform.position;
        manager.Player2.transform.position = p2Spawn.transform.position;
        manager.Player3.transform.position = p3Spawn.transform.position;
        manager.Player4.transform.position = p4Spawn.transform.position;
        if (currentPlayer.tag == "Player1")
        {
            currentPlayer.transform.position = p1Spawn.transform.position;
        }
        if (currentPlayer.tag == "Player2")
        {
            currentPlayer.transform.position = p2Spawn.transform.position;
        }
        if (currentPlayer.tag == "Player3")
        {
            currentPlayer.transform.position = p3Spawn.transform.position;
        }
        if (currentPlayer.tag == "Player4")
        {
            currentPlayer.transform.position = p4Spawn.transform.position;
        }
        manager.Player1.transform.localRotation = startingRotation;
        manager.Player2.transform.localRotation = startingRotation;
        manager.Player3.transform.localRotation = startingRotation;
        manager.Player4.transform.localRotation = startingRotation;
        menuManager.RoundOver();

        outOfMap = false;
        pauseMovement = false;
        diceRollScript.doubleRoll = false;
        p1MoveCount = 0;
        p2MoveCount = 0;
        p3MoveCount = 0;
        p4MoveCount = 0;
        currentCount = 0;
        currentPlayerMoveCount = 0;
        diceRollScript.mapCompleted = false;
        ResetDirection();
    }

    public void ResetToPlayerOne()
    {
        manager.counter = 0;
        currentPlayer = manager.PlayerList[0];
        manager.nextPlayer = manager.PlayerList[1];
        outOfMap = false;
        pauseMovement = false;
        Debug.Log("tile count = " + manager.spawnedTiles.Count);
    }
}
