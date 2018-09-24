using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;

public class MainMenu : WindowControll
{
    public CoreMainMenu coreMainMenu;

    public void Group()
    {
        if (PlayerControl.Instance.designerContructor.fixInit)
        {
            coreMainMenu.SwitchWindow(3);
        }
        else
        {
            GameManager.Instance.PreGame();
        }
    }
}
