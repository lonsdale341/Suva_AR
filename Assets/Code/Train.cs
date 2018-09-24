using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public bool allowed;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "MainCamera")
        {
            if(allowed)
            {
                GameManager.Instance.AddApply(GameManager.TCrash.rail_tracks);
            }
            else
            {
                GameManager.Instance.AddCrash(GameManager.TCrash.rail_tracks);
            }
        }
    }
}
