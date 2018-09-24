using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMovementTrigger : MonoBehaviour
{    public bool allow = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "MainCamera")
        {
            if (allow)
            {
                GameManager.Instance.AddApply(GameManager.TCrash.roundabout);
            }
            else
            {
                GameManager.Instance.AddCrash(GameManager.TCrash.roundabout);
            }
        }
    }
}
