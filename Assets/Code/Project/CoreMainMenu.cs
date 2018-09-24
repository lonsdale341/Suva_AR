using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;

public class CoreMainMenu : WindowManager
{
    public GameObject languageMenu;

    private void Start()
    {
        base.Initialization();

        SwitchWindow(0);
        ;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SwitchWindow(int index)
    {
        for (int i = 0; i < ListWindow.Count; i++)
        {
            ListWindow[i].ShowState(index == i);
        }

        if (index == 3)
        {
            ListWindow[index].GetComponent<GroupMenu>().DelayPlay();
        }
    }

    public void SwitchLanguage(int index)
    {
        switch (index)
        {
            case 0:
                Globals.Instance.language = Globals.TLanguage.German;
                return;
            case 1:
                Globals.Instance.language = Globals.TLanguage.Francua;
                return;
            case 2:
                Globals.Instance.language = Globals.TLanguage.Italyano;
                return;
        }
    }

    public void CellCalibration()
    {
        GameManager.Instance.Calibration();
    }
}