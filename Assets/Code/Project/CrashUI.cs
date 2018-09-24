using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Paradigm;

[System.Serializable]
public class DataTutorial
{
    public GameManager.TCrash typeCrash;
    public Sprite spriteTutorial;
}

public class CrashUI : WindowControll
{
    public Image targetTutorial;
    public List<DataTutorial> listTutorial = new List<DataTutorial>();

    public IEnumerator Start(GameManager.TCrash _typeCrash, float delay)
    {
        TutorialControl(_typeCrash);

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void TutorialControl(GameManager.TCrash _typeCrash)
    {
        foreach(DataTutorial data in listTutorial)
        {
            if(_typeCrash == data.typeCrash)
            {
                targetTutorial.sprite = data.spriteTutorial;
                return;
            }
        }
    }
}
