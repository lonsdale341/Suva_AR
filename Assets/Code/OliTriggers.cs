using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliTriggers : MonoBehaviour
{
    public bool allow = false;

    public void OnTriggerEnter(Collider other)
    {
        if (allow)
        {
            GameManager.Instance.AddApply(GameManager.TCrash.oil_spills);
        }
        else
        {
            GameManager.Instance.AddCrash(GameManager.TCrash.oil_spills);
        }
    }
}
