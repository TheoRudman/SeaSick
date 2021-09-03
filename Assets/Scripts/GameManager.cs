using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MenuManager menuManager;
    public DiceRoll diceRoll;
    public Player player;
    public MainMenu mainMenu;
    public GameData gameData;

    public List<GameObject> tileTransformList;
    public List<GameObject> tileList;
    public List<GameObject> spawnedTiles;

    public GameObject tilePrefab;
    public GameObject startingTile;

    private GameObject nextTile;
    private GameObject tileSelected;

    public GameObject nextPlayer;

    public GameObject FirstPlayer;
    public GameObject SecondPlayer;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public string p1Name;
    public string p2Name;
    public string p3Name;
    public string p4Name;

    public TextMesh p1Title;
    public TextMesh p2Title;
    public TextMesh p3Title;
    public TextMesh p4Title;

    public List<GameObject> PlayerList;
    private int playerCount = 4;

    public Transform spawnLocation1;
    public Transform spawnLocation2;
    public Transform spawnLocation3;
    public Transform spawnLocation4;

    public AudioSource moveSound;
    public AudioSource moveSound2;
    public AudioSource tileSound;

    Transform latestTile;

    private Vector3 nextSpawnPoint;

    private int tilesToSpawnForward = 0;
    private int tilesToSpawnLeft = 0;
    private int tilesToSpawnRight = 0;
    private int tilesToSpawnBackward = 0;

    int tileSetsSpawned = 0;

    int challengeCount = 0;
    int emptyCount = 0;
    int minusCount = 0;
    int itemCount = 0;
    int plusCount = 0;

    public int firstPlayerRef;
    public int secondPlayerRef;

    int lastNumber = 6;
    public int counter = 0;
    public bool isPaused = false;

    int tileCounter = 0;
    int moveSoundCount = 0;

    public int currentPlayerNum;
    void Start()
    {
        p1Name = GameData.p1Name;
        p2Name = GameData.p2Name;
        p3Name = GameData.p3Name;
        p4Name = GameData.p4Name;

        p1Title.text = p1Name;
        p2Title.text = p2Name;
        p3Title.text = p3Name;
        p4Title.text = p4Name;

        Player1.name = p1Name;
        Player2.name = p2Name;
        Player3.name = p3Name;
        Player4.name = p4Name;

        ResetSpawnLocation();
        NumberOfPlayers();
        player.GetStartingRotation();
        StartCoroutine("SpawnTilesForward");

        int i = PlayerList.Count;
        Debug.Log(i);
        playerCount = 4;
    }

    public void ResetSpawnLocation()
    {
        nextSpawnPoint = spawnLocation1.transform.position;
    }

    public void ExpandMap()
    {
        if (diceRoll.rollMenuCount == PlayerList.Count)
        {
            player.pauseMovement = true;
            CheckTileDirection();
            StartCoroutine("ResetPause");
        }
        else
        {
            player.pauseMovement = false;
            //menuManager.OpenRollMenu();
            StartCoroutine("DelayBeforeNextRoll");
        }
    }

    IEnumerator DelayBeforeNextRoll()
    {
        yield return new WaitForSeconds(0.8f);
        menuManager.OpenRollMenu();
        StopCoroutine("DelayBeforeNextRoll");
    }

    public void NumberOfPlayers()
    {
        if (playerCount >= 2)
        {
            //Player1 = GameObject.Find("Player 1");
            PlayerList.Add(Player1);

            //Player2 = GameObject.Find("Player 2");
            PlayerList.Add(Player2);
            //Debug.Log("2 players");
        }
        if (playerCount >= 3)
        {
            //Player3 = GameObject.Find("Player 3");
            PlayerList.Add(Player3);
        }
        if (playerCount == 4)
        {
            //Player4 = GameObject.Find("Player 4");
            PlayerList.Add(Player4);
            //Debug.Log("4 players");
        }
    }

    public void SwapPlayer()
    {
        counter++;

        if (counter == (PlayerList.Count))
        {
            counter = 0;
            //Debug.Log("swapPlayer counter reset");
            nextPlayer = PlayerList[counter];
        }

        nextPlayer = PlayerList[counter];
        CompletedPlayersTurn();
        player.currentPlayer = nextPlayer;
    }

    public void CheckNextPlayer()
    {
        int i = counter;

        if (i == 4)
        { i = 0; }
        FirstPlayer = PlayerList[i];
        firstPlayerRef = i;
        //Debug.Log(FirstPlayer.ToString());

        i++;

        if (i == 4)
        { i = 0; }
        SecondPlayer = PlayerList[i];
        secondPlayerRef = i;
        //Debug.Log(SecondPlayer.ToString());
    }

    public void CompletedPlayersTurn()
    {
        ExpandMap();
        //Debug.Log("completeTurn");
    }

    private void TileSelection()
    { 
        bool isTilePlaced = false;
        int number = Random.Range(0, 5);
        if ((number == 0) && (challengeCount == 0) && (lastNumber != 0))
        {
            tileSelected = tileTransformList[0];
            challengeCount++;
            isTilePlaced = true;
            lastNumber = 0;
            tileCounter++;
            PlayTileSound();
        }
        if ((number == 1) && (emptyCount < 2) && (lastNumber != 1))
        {
            tileSelected = tileTransformList[1];
            isTilePlaced = true;
            emptyCount++;
            lastNumber = 1;
            tileCounter++;
            PlayTileSound();
        }
        if ((number == 2) && (itemCount < 2) && (lastNumber != 2) && (tileCounter != 5))
        {
            tileSelected = tileTransformList[4];
            isTilePlaced = true;
            itemCount++;
            lastNumber = 2;
            tileCounter++;
            PlayTileSound();
        }
        if ((number == 3) && (minusCount < 2) && (lastNumber != 3))
        {
            tileSelected = tileTransformList[3];
            isTilePlaced = true;
            minusCount++;
            lastNumber = 3;
            tileCounter++;
            PlayTileSound();
        }
        if ((number == 4) && (plusCount < 2) && (lastNumber != 4))
        {
            tileSelected = tileTransformList[2];
            isTilePlaced = true;
            plusCount++;
            lastNumber = 4;
            tileCounter++;
            PlayTileSound();
        }

        if (isTilePlaced == false)
        {
            TileSelection();
        }
    }

    public void PlayMoveSound()
    {
        moveSoundCount++;
        if (moveSoundCount%2 == 0)
        { moveSound.Play(); }
        else
        { moveSound2.Play(); }
    }

    void PlayTileSound()
    { tileSound.Play(); }

    private void RefreshTileSelection()
    {
        itemCount = 0;
        challengeCount = 0;
        minusCount = 0;
        plusCount = 0;
        emptyCount = 0;
    }
    public void CheckTileDirection()
    {
        if (tileSetsSpawned == 0)
        {  }
        if (tileSetsSpawned == 1)
        { StartCoroutine("SpawnTilesLeft"); }
        if (tileSetsSpawned == 2)
        { StartCoroutine("SpawnTilesRight"); }
        if (tileSetsSpawned == 3)
        { StartCoroutine("SpawnTilesBackwards"); }
        if (tileSetsSpawned >= 4)
        {  }
    }
    IEnumerator ResetPause()
    {
        yield return new WaitForSeconds(2f);

        player.UpdateCurrentPlayer();
        player.pauseMovement = false;

        diceRoll.rollMenuCount = 0;
        menuManager.OpenRollMenu();
        StopCoroutine("ResetPause");
    }

    public void RestartTiles()
    {
        for (int i = 0; i < spawnedTiles.Count; i++)
        {
            Destroy(spawnedTiles[i].gameObject);
            tileSetsSpawned = 0;
        }
        tilesToSpawnForward = 0;
        tilesToSpawnLeft = 0;
        tilesToSpawnRight = 0;
        tilesToSpawnBackward = 0;
        spawnedTiles.Clear();
        ResetSpawnLocation();

        StartCoroutine("SpawnTilesForward");
    }



    IEnumerator SpawnTilesForward()
    {
        int i = 0;
        while (i < 6)
        {
            TileSelection();
            nextTile = Instantiate(tileSelected) as GameObject;
            nextTile.transform.SetParent(transform);
            nextTile.transform.position = (nextSpawnPoint);
            spawnedTiles.Add(nextTile);

            latestTile = nextTile.transform.Find("SpawnPoint1");
            nextSpawnPoint = latestTile.transform.position;

            yield return new WaitForSeconds(0.3f);
            tilesToSpawnForward++;
            i++;
        }

        if (tilesToSpawnForward == 6)
        { 
            latestTile = nextTile.transform.Find("SpawnPoint2");
            nextSpawnPoint = latestTile.transform.position;
            RefreshTileSelection();
            tileSetsSpawned = 1;
            menuManager.OpenRollMenu();
            tileCounter = 0; 
            StopCoroutine("SpawnTilesForward");
        }
    }

    IEnumerator SpawnTilesLeft()
    {
        int i = 0;
        while (i < 6)
        {
            TileSelection();
            nextTile = Instantiate(tileSelected) as GameObject;
            nextTile.transform.SetParent(transform);
            nextTile.transform.position = (nextSpawnPoint);
            spawnedTiles.Add(nextTile);

            latestTile = nextTile.transform.Find("SpawnPoint2");
            nextSpawnPoint = latestTile.transform.position;

            yield return new WaitForSeconds(0.3f);
            tilesToSpawnLeft++;
            i++;
        }

        if (tilesToSpawnLeft == 6)
        {
            latestTile = nextTile.transform.Find("SpawnPoint3");
            nextSpawnPoint = latestTile.transform.position;
            RefreshTileSelection();
            tileSetsSpawned = 2;
            tileCounter = 0;
            StopCoroutine("SpawnTilesLeft");

        }
    }

    IEnumerator SpawnTilesRight()
    {
        int i = 0;
        while (i < 6 )
        {  
            TileSelection(); 
            nextTile = Instantiate(tileSelected) as GameObject;
            nextTile.transform.SetParent(transform); 
            nextTile.transform.position = (nextSpawnPoint);
            spawnedTiles.Add(nextTile);
            latestTile = nextTile.transform.Find("SpawnPoint3");
            nextSpawnPoint = latestTile.transform.position;    
            yield return new WaitForSeconds(0.3f); 
            tilesToSpawnRight++;  
            i++;
        }
       
        if (tilesToSpawnRight == 6)
        {
            latestTile = nextTile.transform.Find("SpawnPoint4");
            nextSpawnPoint = latestTile.transform.position;
            RefreshTileSelection();
            tileSetsSpawned = 3;
            tileCounter = 0;
            StopCoroutine("SpawnTilesRight");
        }
    }
    IEnumerator SpawnTilesBackwards()
    {
        int i = 0;
        while (i < 5)
        {

            TileSelection();
            nextTile = Instantiate(tileSelected) as GameObject;
            nextTile.transform.SetParent(transform);
            nextTile.transform.position = (nextSpawnPoint);    
            spawnedTiles.Add(nextTile);

            latestTile = nextTile.transform.Find("SpawnPoint4");
            nextSpawnPoint = latestTile.transform.position;

            yield return new WaitForSeconds(0.3f);
            tilesToSpawnBackward++;
            i++;
        }
        diceRoll.mapCompleted = true;

        if (tilesToSpawnBackward == 5)
        {
            RefreshTileSelection();
            tileSetsSpawned = 4;
            tileCounter = 0; 

            StopCoroutine("SpawnTilesBackwards");
        }
    }


}
