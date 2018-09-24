using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientTriggers : MonoBehaviour
{
    public AudioSource source;

    public bool state;

    private void OnTriggerEnter(Collider other)
    {
        state = true;
    }

    private void OnTriggerExit(Collider other)
    {
        state = false;
    }

    private void Update()
    {
        if(state && GameManager.Instance.gameMode == GameManager.TType.Gameplay)
        {
            source.volume = Mathf.Lerp(source.volume, 0.5f, 0.1f);
        }
        else
        {
            source.volume = Mathf.Lerp(source.volume, 0, 0.1f);
        }
    }
}
