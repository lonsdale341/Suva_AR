using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imunitet : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "MainCamera")
        {
            GameManager.Instance.imun = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "MainCamera")
        {
            GameManager.Instance.imun = false;
        }
    }
}
