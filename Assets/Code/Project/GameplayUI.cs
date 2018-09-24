using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Paradigm;
using TMPro;

public class GameplayUI : WindowControll
{
    public Text txtTimer;
    public GameObject backToZone;
    public GameObject backToStartZone;

    public Text txtCrashCounter;

    [Header("TrafficLight")] public float minDistanceToLight = 5f;

    public GameObject parentLight;
    public List<Image> listImgLight = new List<Image>();

    [Header("Quests")] public TextMeshProUGUI txtHeaderQuest;
    public Transform questContent;
    public GameObject exampleQuest;

    [Header("MinMap")] public Image imgPointPlayer;
    public Image imgPointTirgger;

    public List<DataQuestUI> listQuest = new List<DataQuestUI>();

    ParadigmControl getLinkParadigm;

    private short indexTimer;

    public void Start()
    {
        SetActive(backToZone, false);
        SetActive(backToStartZone, false);

        getLinkParadigm = ParadigmControl.Instance;

        exampleQuest.SetActive(false);

        if (getLinkParadigm)
        {
            Invoke("InitListQuest", 0.3f);
        }

        InitTimer();
    }

    public void Update()
    {
        TrafiicLightControl();

        if (!getLinkParadigm)
        {
            getLinkParadigm = ParadigmControl.Instance;
            return;
        }

        ListQuestControl();
        CrashControl();
        TimerControl();
        ZoneControl();
        MinMapControl();
    }

    public void InitTimer()
    {
        indexTimer = Globals.Instance.coreWorldTime.AddTimer(360);
    }

    private void TimerControl()
    {
        if (Globals.Instance.coreWorldTime.listDataTime.Count <= indexTimer)
        {
            return;
        }

        int allSecond = Globals.Instance.coreWorldTime.listDataTime[indexTimer].result.Seconds;
        allSecond += Globals.Instance.coreWorldTime.listDataTime[indexTimer].result.Minutes * 60;

        //txtTimer.text = allSecond.ToString();
        if (allSecond - 60 <= 0)
        {
            SetTimeString(txtTimer, 0);
        }
        else
        {
            SetTimeString(txtTimer, allSecond - 60);
        }

        if (allSecond <= 0)
        {
            GameManager.Instance.BackGame();
        }
    }

    private void ZoneControl()
    {
        Vector3 fixPos = Camera.main.transform.position;
        fixPos.y = PlayerControl.Instance.designerContructor.midleWorldPoint.y;

        if (Vector3.Distance(PlayerControl.Instance.designerContructor.midleWorldPoint, fixPos) >=
            PlayerControl.Instance.designerContructor.maxDistance)
        {
            SetActive(backToZone, true);
        }
        else
        {
            SetActive(backToZone, false);
        }
    }

    private void TrafiicLightControl()
    {
        return;
        bool find = false;
        TrafficLight getLight = null;

        if (getLinkParadigm)
        {
            for (int i = 0; i < getLinkParadigm.listLight.Count; i++)
            {
                if (Vector3.Distance(getLinkParadigm.listLight[i].transform.position, Camera.main.transform.position) <=
                    minDistanceToLight)
                {
                    SetActive(parentLight, true);
                    getLight = getLinkParadigm.listLight[i];
                    find = true;
                    break;
                }
            }

            if (!find)
            {
                SetActive(parentLight, false);
                return;
            }
        }
        else
        {
            SetActive(parentLight, false);
            return;
        }

        int activeIndex = 99;

        if (getLight)
        {
            switch (getLight.typeLight)
            {
                case TrafficLight.DataTrafficLight.TTypeLight.Red:
                    activeIndex = 0;
                    break;
                case TrafficLight.DataTrafficLight.TTypeLight.Yellow:
                    activeIndex = 1;
                    break;
                case TrafficLight.DataTrafficLight.TTypeLight.Green:
                    activeIndex = 2;
                    break;
            }
        }

        for (int i = 0; i < listImgLight.Count; i++)
        {
            if (activeIndex == i)
            {
                listImgLight[i].enabled = true;
            }
            else
            {
                listImgLight[i].enabled = false;
            }
        }
    }

    public void BackToZoneMode(bool state)
    {
        SetActive(backToStartZone, state);
    }

    public void CrashControl()
    {
        txtCrashCounter.text = GameManager.Instance.crashCounter.ToString();
    }

    #region Quest

    public void InitListQuest()
    {
        if (listQuest.Count > 0)
        {
            for (int i = 0; i < listQuest.Count; i++)
            {
                if (listQuest[i])
                {
                    Destroy(listQuest[i]);
                }
            }
        }

        listQuest = new List<DataQuestUI>();

        for (int i = 0; i < getLinkParadigm.listTriggers.Count; i++)
        {
            if (!string.IsNullOrEmpty(getLinkParadigm.listTriggers[i].name))
            {
                GameObject newQuest = Instantiate(exampleQuest, questContent);
                newQuest.SetActive(true);

                var DataUI = newQuest.GetComponent<DataQuestUI>();

                getLinkParadigm.listTriggers[i].GetComponent<LanguageText>().Initialization();
                getLinkParadigm.listTriggers[i].GetComponent<LanguageText>().Control();
                SetString(DataUI.txtName, getLinkParadigm.listTriggers[i].GetComponent<LanguageText>().currentText);
                DataUI.index = i;

                listQuest.Add(DataUI);
            }
        }
    }

    public void ListQuestControl()
    {
        if (listQuest.Count > 0)
        {
            for (int i = 0; i < listQuest.Count; i++)
            {
                if (getLinkParadigm.curCounter - 1 > listQuest[i].index)
                {
                    listQuest[i].state = true;
                }
                else if (getLinkParadigm.curCounter > listQuest[i].index)
                {
                    listQuest[i].current = true;
                }
                else
                {
                    listQuest[i].state = false;
                    listQuest[i].current = false;
                }
            }
        }

        SetString(txtHeaderQuest, (getLinkParadigm.curCounter - 1) + "/" + listQuest.Count);
    }

    #endregion

    #region MiniMap

    [Header("Mini_Map")] public uint mapScaleModifier = 32;

    private void MinMapControl()
    {
        Vector2 invertPosition = Vector2.zero;
        invertPosition.x = -ParadigmControl.Instance.center.transform
            .InverseTransformDirection(Camera.main.transform.position - ParadigmControl.Instance.center.position).x;
        invertPosition.y = -ParadigmControl.Instance.center.transform
            .InverseTransformDirection(Camera.main.transform.position - ParadigmControl.Instance.center.position).z;

        imgPointPlayer.transform.localPosition = invertPosition * mapScaleModifier;

        imgPointTirgger.enabled = true;

        Vector3 newRot = Vector3.zero;
        newRot.x = 0;
        newRot.y = 0;
        newRot.z = (-PlayerControl.Instance.transform.eulerAngles.y) + ParadigmControl.Instance.transform.eulerAngles.y;
        imgPointPlayer.transform.localEulerAngles = newRot;

        foreach (FinishTrigger t in ParadigmControl.Instance.listTriggers)
        {
            if (t.gameObject.activeSelf)
            {
                Vector2 invertTrigger = Vector2.zero;
                invertTrigger.x = -ParadigmControl.Instance.center.transform
                    .InverseTransformDirection(t.transform.position -
                                               ParadigmControl.Instance.center.position).x;
                invertTrigger.y = -ParadigmControl.Instance.center.transform
                    .InverseTransformDirection(t.transform.position -
                                               ParadigmControl.Instance.center.position).z;

                imgPointTirgger.transform.localPosition = invertTrigger * mapScaleModifier;
                return;
            }
        }

        imgPointTirgger.enabled = false;
    }

    #endregion

    public void CallLeadboard()
    {
        CoreAppControl.Instance.DialogApp.CellWindow(TTypeWindow.Leadboard);
    }
}