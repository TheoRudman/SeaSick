using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : MonoBehaviour
{
    public MenuManager menuManager;


    public void BeginEmpty()
    {
        StartCoroutine("StartEmpty");
    }

    IEnumerator StartEmpty()
    {
        menuManager.OpenEmptyTile();
        yield return new WaitForSeconds(2.5f);
        menuManager.CloseEmptyTile();
        menuManager.TileComplete();
        StopCoroutine("StartEmpty");
    }
}
