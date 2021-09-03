using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StealPoints : MonoBehaviour
{
    public ScoreManager scoreManager;
    public MenuManager menuManager;
    public Player player;

    public Button p1Button;
    public Button p2Button;
    public Button p3Button;
    public Button p4Button;

    public Text playerName;
    public Text p1Text;
    public Text p2Text;
    public Text p3Text;
    public Text p4Text;
    public void SetPlayerName()
    {
        playerName.text = (player.currentPlayer.name);
        p1Text.text = GameData.p1Name;
        p2Text.text = GameData.p2Name;
        p3Text.text = GameData.p3Name;
        p4Text.text = GameData.p4Name;
    }

    public void StealPlayerOne()
    {
        scoreManager.AdjustPointDirectly(1, -5);
        scoreManager.AdjustPoints(5);
        RemoveButtons();
        StartCoroutine("WaitTime");
    }
    public void StealPlayerTwo()
    {
        scoreManager.AdjustPointDirectly(2, -5);
        scoreManager.AdjustPoints(5);
        RemoveButtons();
        StartCoroutine("WaitTime");
    }
    public void StealPlayerThree()
    {
        scoreManager.AdjustPointDirectly(3, -5);
        scoreManager.AdjustPoints(5);
        RemoveButtons();
        StartCoroutine("WaitTime");
    }
    public void StealPlayerFour()
    {
        scoreManager.AdjustPointDirectly(4, -5);
        scoreManager.AdjustPoints(5);
        RemoveButtons();
        StartCoroutine("WaitTime");
    }

    void RemoveButtons()
    {
        p1Button.interactable = false;
        p2Button.interactable = false;
        p3Button.interactable = false;
        p4Button.interactable = false;
    }

    public void EnableButtons()
    {
        p1Button.interactable = true;
        p2Button.interactable = true;
        p3Button.interactable = true;
        p4Button.interactable = true;
    }

    public void DisablePlayerButton()
    {
        if (player.currentPlayer.tag == "Player1")
        {
            p1Button.interactable = false;
        }
        if (player.currentPlayer.tag == "Player2")
        {
            p2Button.interactable = false;
        }
        if (player.currentPlayer.tag == "Player3")
        {
            p3Button.interactable = false;
        }
        if (player.currentPlayer.tag == "Player4")
        {
            p4Button.interactable = false;
        }
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        menuManager.ClosePointsItem();
        menuManager.TileComplete();
        StopCoroutine("WaitTime");
    }
}