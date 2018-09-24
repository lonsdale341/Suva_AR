using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesdtMidle : MonoBehaviour
{

    public List<Transform> listPoints = new List<Transform>();
    public Vector3 buffer;
    public Transform center;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        buffer = listPoints[0].position;

        for (int i = 1; i < listPoints.Count; i++)
        {
            buffer += listPoints[i].transform.position;
        }


        buffer /= listPoints.Count;

        center.position = buffer;

       // Vector3 newPos = listPoints[listPoints.Count - 1].transform.position + (currentPoint.transform.position - listPoints[listPoints.Count - 1].transform.position) / 2;
    }
}
