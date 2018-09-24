using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;

public class AngleMovement : MonoBehaviourSingleton<AngleMovement>
{
    public Transform target;
    private Transform localTransform;

    public float curDistance;

    public float midleDist = 3f;
    public float offsetDist = 1.5f;

    public bool entered = false;
    public float alertTimer = 1.5f;
    public bool alertEvent = false;
    public float timer;

    private void Start()
    {
        target = Camera.main.transform;
        localTransform = transform;
    }

    private void Update()
    {
        return; // ToDo Here is Temp fix for "Round triggers"
        CalculationDistance();

        EventControl();
    }

    private void CalculationDistance()
    {
        Vector3 fixPos = target.position;
        fixPos.y = localTransform.position.y;

        curDistance = Vector3.Distance(fixPos, localTransform.position);
    }

    private void EventControl()
    {
        if(curDistance <= midleDist + (offsetDist * 2))
        {
            entered = true;

            if (curDistance >= midleDist + offsetDist || curDistance <= midleDist - offsetDist)
            {
                if (timer >= alertTimer)
                {
                    alertEvent = true;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else
            {
                if (timer > 0) { timer -= Time.deltaTime * 2; }
                else { alertEvent = false; }
            }
        }
        else
        {
            if (entered)
            {
                if (!alertEvent)
                {
                    //GameManager.Instance.AddApply(GameManager.TCrash.roundabout);
                }
                else
                {
                    GameManager.Instance.AddCrash(GameManager.TCrash.roundabout);
                }
            }

            entered = false;

            if (timer > 0) { timer -= Time.deltaTime * 2; }
            else { alertEvent = false; }
        }
    }
}
