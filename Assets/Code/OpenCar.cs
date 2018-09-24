using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;

public class OpenCar : MonoBehaviour
{
    public float timeToOpen = 1f;
    public float ractDistance = 2f;

    public AudioSource aSource;

    public float timer;
    public bool enableTrigger = false;

    public Animator car;

    private void OnTriggerEnter(Collider other)
    {
        if (!enableTrigger)
        {
            enableTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(enableTrigger)
        {
            enableTrigger = false;
        }
    }

    private void Update()
    {
        if(enableTrigger)
        {
            timer += Time.deltaTime;

            if(timer >= timeToOpen)
            {
                car.SetTrigger("Open");
                aSource.Play();
                Vector3 fixDistance = PlayerControl.Instance.transform.position;
                fixDistance.y = transform.position.y;

                print(Vector3.Distance(fixDistance, transform.position));

                if (Vector3.Distance(fixDistance, transform.position) <= 1.2f)
                {
                    GameManager.Instance.AddCrash(GameManager.TCrash.parked_car);
                }
                else
                {
                    GameManager.Instance.AddApply(GameManager.TCrash.parked_car);
                }

                timer = 0;
                enableTrigger = false;
            }
        }
    }
}
