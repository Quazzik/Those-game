using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DeleteBox : MonoBehaviour
{
    GameObject wall1;
    GameObject wall2;
    GameObject wall3;
    GameObject wall4;
    GameObject wall5;
    GameObject wall6;
    List <GameObject> walls = new List <GameObject>();
    public float step;
    Color _color;

    void Start()
    {
        wall1 = transform.GetChild(1).GetComponent<GameObject>();
        wall2 = transform.GetChild(2).GetComponent<GameObject>();
        wall3 = transform.GetChild(3).GetComponent<GameObject>();
        wall4 = transform.GetChild(4).GetComponent<GameObject>();
        wall5 = transform.GetChild(5).GetComponent<GameObject>();
        wall6 = transform.GetChild(6).GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
           /* Destroy(wall1,2f);
            Destroy(wall2,2f);
            Destroy(wall3,2f);
            Destroy(wall4,2f);
            Destroy(wall5,2f);
            Destroy(wall6,2f);*/
    }
}
