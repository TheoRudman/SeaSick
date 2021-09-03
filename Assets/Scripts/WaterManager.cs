using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public float height;
    public float time;

    public Vector2 Scroll = new Vector2(0.05f, 0.05f);
    Vector2 Offset = new Vector2(0f, 0f);

    void Update()
    {
        Offset += Scroll * Time.deltaTime;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", Offset);
    }

    void Start()
    {
        iTween.MoveBy(this.gameObject, iTween.Hash("y", height, "time", time, "looptype", "pingpong", "easetype", iTween.EaseType.easeInOutSine));
    }
}
