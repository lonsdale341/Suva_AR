using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAllow : MonoBehaviour
{
    public TruckTrigger truckTrigger;
    public AICar car;

    public bool isEnterPlayer = false;
    public bool carEnter = false;

    private float timer;
    private float truckTriggerStandTime = 5f;
    private float successDisplayDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnterPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnterPlayer = false;
        }
    }

    private void Update()
    {
        if (isEnterPlayer)
        {
            if (truckTrigger.isEntered && !carEnter)
            {
                carEnter = true;
            }

            if (carEnter)
            {
                if (!truckTrigger.isEntered)
                {
                    Paradigm.CoreAppControl.Instance.DialogApp.CellWindow(Paradigm.WindowControll.TTypeWindow.Done);
                    carEnter = false;
                }
            }
        }

        if (isEnterPlayer && timer <= truckTriggerStandTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= truckTriggerStandTime)
        {
            var successCoroutine = StartCoroutine(SuccessStayBehindTrackRoutine(successDisplayDelay));

            car.type = AICar.TType.Start;
            timer = 0;
        }
    }

    private IEnumerator SuccessStayBehindTrackRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.AddApply(GameManager.TCrash.blind_spot);
    }

    public void ResetTruck()
    {
        isEnterPlayer = false;
        carEnter = false;
        timer = 0;
    }
}