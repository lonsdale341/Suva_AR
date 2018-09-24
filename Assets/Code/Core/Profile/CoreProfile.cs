using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProfile
{
    public string name;
    public int timeSecond;
    public int crashCounter;
    public int result
    {
        get { return 100 + timeSecond - (crashCounter * 10); }
        private set { }
    }

    public List<GameManager.TCrash> listCrash = new List<GameManager.TCrash>();
    public List<GameManager.TCrash> listApply = new List<GameManager.TCrash>();

    public void AddCrash(GameManager.TCrash type)
    {
        if (listCrash.Contains(type)) { return; }

        listCrash.Add(type);
    }

    public void AddApply(GameManager.TCrash type)
    {
        if (listApply.Contains(type)) { return; }

        listApply.Add(type);
    }

    public void Reset()
    {
        name = null;
        timeSecond = 0;
        crashCounter = 0;
        listCrash = new List<GameManager.TCrash>();
    }
}
