using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;

public class FinishTrigger : MonoBehaviour
{
    public enum TType
    {
        Finish,
        StartGame,
        Next
    }

    public string name;
    public BoxCollider bCollider;
    public MeshRenderer mRenderer;
    public ParadigmControl paradigmControl;

    public TType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            GameManager.Instance.BlinkSound();

            switch (type)
            {
                case TType.Finish:
                    GameManager.Instance.BackGame();
                    paradigmControl.StopGame();
                    return;
                case TType.StartGame:
                    //paradigmControl.NextTrigger();
                    GameManager.Instance.StartGame();
                    //CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Gameplay).GetComponent<GameplayUI>().InitTimer();
                    return;
                case TType.Next:
                    paradigmControl.NextTrigger();
                    return;
                default:
                    Debug.Log("Check trigger type");
                    return;
            }
        }
    }

    private void Set(bool state)
    {
        bCollider.enabled = state;
        mRenderer.enabled = state;
    }
}