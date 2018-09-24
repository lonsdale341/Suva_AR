using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;
using TMPro;

public class Leadboard : WindowControll
{
    [System.Serializable]
    public class DataEvent
    {
        public GameManager.TCrash type;
        public string descriptionGerman;
        public string descriptionFrench;
        public string descriptionItal;
    }

    [Header("UI")]
    public Transform content;
    public GameObject examplePrefab;
    public List<LeadboardPole> listResult = new List<LeadboardPole>();

    public List<DataEvent> listEvents = new List<DataEvent>();

    //public TextMeshProUGUI txtNameGroup;
    public EventsPole exampleEvent;
    public Transform eventParent;


    public override void Initialization()
    {
        base.Initialization();
    }

    private void Start()
    {
        examplePrefab.SetActive(false);

        InitList();
    }

    #region Leadboard

    public void InitList()
    {
        InitListEvents();

        return;

        for (int i = 0; i < listResult.Count; i++)
        {
            if (listResult[i]) { Destroy(listResult[i].gameObject); }
        }

        listResult = new List<LeadboardPole>();

        List<CoreProfile> getList = Globals.Instance.coreLeaderboard.listProfile;

        var temp = new CoreProfile();
        for (int i = 0; i < getList.Count; i++)
        {
            for (int w = 0; w < getList.Count; w++)
            {
                if(getList[i].result < getList[w].result)
                {
                    temp = getList[i];
                    getList[i] = getList[w];
                    getList[w] = temp;
                }
            }
        }

        for (int i = 0; i < getList.Count; i++)
        {
            if (listResult.Count < 10)
            {
                GameObject newPrefab = Instantiate(examplePrefab, content);
                newPrefab.SetActive(true);

                LeadboardPole Data = newPrefab.GetComponent<LeadboardPole>();
                Data.Initialization(getList[i].name, i + 1, getList[i].result, getList[i].name == Globals.Instance.coreProfile.name, Globals.Instance.coreProfile.crashCounter);

                listResult.Add(Data);
            }
            else
            {
                break;
            }
        }
    }

    #endregion

    public void NextGame()
    {
        GameManager.Instance.StopGame();
    }

    #region Events

    [SerializeField] private TextMeshProUGUI titleText;
    
    public void InitListEvents()
    {
        //SetString(txtNameGroup, Globals.Instance.coreProfile.name);
        List<GameManager.TCrash> listFirst = new List<GameManager.TCrash>();
        listFirst.Add(GameManager.TCrash.parked_car);
        listFirst.Add(GameManager.TCrash.blind_spot);
        listFirst.Add(GameManager.TCrash.roundabout);
        listFirst.Add(GameManager.TCrash.oil_spills);
        listFirst.Add(GameManager.TCrash.rail_tracks);

        for (int w = 0; w < listFirst.Count; w++)
        {
            for (int i = 0; i < listEvents.Count; i++)
            {
                if (listFirst[w] == listEvents[i].type)
                {
                    GameObject newEvent = Instantiate(exampleEvent.gameObject, eventParent);

                    EventsPole dataPole = newEvent.GetComponent<EventsPole>();

                    if (Globals.Instance.coreProfile.listApply.Contains(listEvents[i].type))
                    {
                        dataPole.doneObj.SetActive(true);
                        dataPole.crashObj.SetActive(false);
                    }
                    else
                    {
                        dataPole.doneObj.SetActive(false);
                        dataPole.crashObj.SetActive(true);
                    }

                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German: SetString(dataPole.txt, listEvents[i].descriptionGerman); break;
                        case Globals.TLanguage.Francua: SetString(dataPole.txt, listEvents[i].descriptionFrench); break;
                        case Globals.TLanguage.Italyano: SetString(dataPole.txt, listEvents[i].descriptionItal); break;
                    }
                    
                    titleText.gameObject.SetActive(true);
                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German: SetString(titleText, "Auswertung SUVA-City 2.0"); break;
                        case Globals.TLanguage.Francua: SetString(titleText, "Evaluation SUVA-City 2.0"); break;
                        case Globals.TLanguage.Italyano: SetString(titleText, "Analisi SUVA-City 2.0"); break;
                    }

                    break;
                }
            }
        }
    }

    #endregion
}
