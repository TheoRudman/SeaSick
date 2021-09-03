using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
    Transform waterPlane;
    Cloth waterCloth;
    [SerializeField]int closestVertexIndex = -1;

    void Start()
    {
        waterPlane = GameObject.Find("WaterPlane").transform;
        waterCloth = waterPlane.GetComponent<Cloth>(); 
    }

    void Update()
    {
        GetClosestVertex();
    }

    void GetClosestVertex()
    {
        for (int i = 0; i < waterCloth.vertices.Length; i++)
        {
            if (closestVertexIndex == -1)
            { closestVertexIndex = i; }
            float distance = Vector3.Distance(waterCloth.vertices[i], transform.position);
            float closestDistance = Vector3.Distance(waterCloth.vertices[closestVertexIndex], transform.position);

            if (distance < closestDistance)
            {
                closestVertexIndex = i;
            }
        }

        transform.localPosition = new Vector3(transform.localPosition.x, waterCloth.vertices[closestVertexIndex].y/30, transform.localPosition.z);
    }
}
