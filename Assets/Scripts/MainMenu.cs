using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameData gameData;

    public TMP_InputField p1Input;
    public TMP_InputField p2Input; 
    public TMP_InputField p3Input; 
    public TMP_InputField p4Input;

    string p1name;
    string p2name;
    string p3name;
    string p4name;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InputName()
    {
        p1name = p1Input.text;
        p2name = p2Input.text;
        p3name = p3Input.text;
        p4name = p4Input.text;

        //Debug.Log(p1name);
        //Debug.Log(p2name);
        //Debug.Log(p3name);
        //Debug.Log(p4name);

        GameData.p1Name = p1name;
        GameData.p2Name = p2name;
        GameData.p3Name = p3name;
        GameData.p4Name = p4name;

        gameData.SetNames();
    }
}
