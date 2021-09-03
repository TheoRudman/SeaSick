using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static string p1Name;
    public static string p2Name;
    public static string p3Name;
    public static string p4Name;

    public void SetNames()
    {
        Debug.Log(p1Name);
        Debug.Log(p2Name); 
        Debug.Log(p3Name);
        Debug.Log(p4Name);
        SceneManager.LoadScene("MainGame");
    }
    
}
