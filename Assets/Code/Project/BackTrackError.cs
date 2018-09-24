using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrackError : MonoBehaviour
{
    public Vector3 result;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            result = Quaternion.LookRotation(other.transform.forward, Vector3.up).eulerAngles;

            if (result.y > 180 && result.y <= 290)
            {
                GameManager.Instance.AddCrash(GameManager.TCrash.none);
            }
        }
    }
}
