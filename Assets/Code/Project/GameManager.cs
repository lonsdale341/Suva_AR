using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paradigm;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public enum TType
    {
        MainMenu,
        Gameplay,
        BackToZone
    }

    public enum TCrash
    {
        blind_spot,
        oil_spills,
        parked_car,
        rail_tracks,
        roundabout,
        none
    }

    public PlayerControl playerController;
    public CoreMainMenu mainMenu;

    public GameObject crashSound;
    public GameObject doneSound;
    public List<AudioClip> listCrashSoundGerman;
    public List<AudioClip> listCrashSoundItal;
    public List<AudioClip> listCrashSoundFrench;
    public GameObject currentVoice;

    public int crashCounter = 0;
    public bool imun = false;

    public TType gameMode = TType.MainMenu;

    GameObject bufferDone;
    GameObject bufferCrash;

    private void Update()
    {
        CoreUpdate();
    }

    public void PreGame()
    {
        mainMenu.SwitchWindow(999);

        if (!PlayerControl.Instance.currentGround)
        {
            if (PlayerControl.Instance.designerContructor.typeDesign == DesignConstrctor.TDesign.Stop)
            {
                PlayerControl.Instance.Inititalization();
            }
            else
            {
                CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.PreStart);
            }
        }
        else
        {
            //StartGame();
            PlayerControl.Instance.currentGround.GetComponent<ParadigmControl>().CellToPlay();
            CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.PreStart);
        }
    }

    public void StartGame()
    {
        if (gameMode == TType.Gameplay)
        {
            return;
        }

        gameMode = TType.Gameplay;

        mainMenu.SwitchWindow(999);
        crashCounter = 0;

        Globals.Instance.coreProfile.listCrash = new List<TCrash>();
        Globals.Instance.coreProfile.listApply = new List<TCrash>();

        if (PlayerControl.Instance.currentGround)
        {
            PlayerControl.Instance.currentGround.GetComponent<ParadigmControl>().city.SetActive(true);
        }

        ParadigmControl.Instance.CellToPlay();
        CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.Gameplay);
        PlayerControl.Instance.Inititalization();
        imun = false;
    }

    public void BackGame()
    {
        if (gameMode == TType.BackToZone)
        {
            return;
        }

        Globals.Instance.coreProfile.timeSecond = Globals.Instance.coreWorldTime.listDataTime[0].result.Seconds;
        Globals.Instance.coreProfile.crashCounter = crashCounter;
        Globals.Instance.coreLeaderboard.AddResult();

        gameMode = TType.BackToZone;

        PlayerControl.Instance.currentGround.GetComponent<ParadigmControl>().StopGame();
        PlayerControl.Instance.currentGround.GetComponent<ParadigmControl>().city.SetActive(false);
        CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.Leadboard);

        CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Gameplay).GetComponent<GameplayUI>()
            .BackToZoneMode(true);
    }

    public void StopGame()
    {
        if (gameMode == TType.MainMenu)
        {
            return;
        }

        gameMode = TType.MainMenu;

        ParadigmControl.Instance.StopGame();

        //mainMenu.GetWindow(WindowControll.TTypeWindow.MainMenu).gameObject.SetActive(true);
        mainMenu.SwitchWindow(1);

        if (CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Leadboard))
        {
            Destroy(CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Leadboard).gameObject);
            PlayerControl.Instance.ResetInitialization();
        }

        imun = false;
    }

    public void Home()
    {
        gameMode = TType.MainMenu;
        //mainMenu.GetWindow(WindowControll.TTypeWindow.MainMenu).gameObject.SetActive(true);
        mainMenu.SwitchWindow(1);
        imun = false;
    }

    public void AddCrash(TCrash type)
    {
        if (gameMode != TType.Gameplay)
        {
            return;
        }

        if (imun)
        {
            return;
        }

        if (bufferDone || bufferCrash)
        {
            return;
        }

        crashCounter++;
        Globals.Instance.coreWorldTime.AwaeSecond(Globals.Instance.coreWorldTime.listDataTime.Count - 1,
            10 * crashCounter);

        GameObject sound = Instantiate(crashSound);

        if (!Globals.Instance.coreProfile.listCrash.Contains(type))
        {
            if (currentVoice)
            {
                Destroy(currentVoice.gameObject);
            }

            currentVoice = Instantiate(crashSound);

            switch (type)
            {
                case TCrash.blind_spot:
                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[3];
                            break;
                        case Globals.TLanguage.Italyano:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[3];
                            break;
                        case Globals.TLanguage.Francua:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[3];
                            break;
                    }

                    break;

                case TCrash.oil_spills:
                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[1];
                            break;
                        case Globals.TLanguage.Italyano:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[1];
                            break;
                        case Globals.TLanguage.Francua:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[1];
                            break;
                    }

                    break;

                case TCrash.parked_car:
                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[0];
                            break;
                        case Globals.TLanguage.Italyano:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[0];
                            break;
                        case Globals.TLanguage.Francua:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[0];
                            break;
                    }

                    break;

                case TCrash.rail_tracks:
                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[2];
                            break;
                        case Globals.TLanguage.Italyano:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[2];
                            break;
                        case Globals.TLanguage.Francua:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[2];
                            break;
                    }

                    break;

                case TCrash.roundabout:
                    switch (Globals.Instance.language)
                    {
                        case Globals.TLanguage.German:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[4];
                            break;
                        case Globals.TLanguage.Italyano:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[4];
                            break;
                        case Globals.TLanguage.Francua:
                            currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[4];
                            break;
                    }

                    break;
                case TCrash.none:
                    break;
                default:
                    Debug.Log("Check crash type");
                    break;
            }

            Globals.Instance.coreProfile.AddCrash(type);

            currentVoice.GetComponent<AudioSource>().Play();
            Destroy(currentVoice, currentVoice.GetComponent<AudioSource>().clip.length + 0.3f);


            if (bufferDone)
            {
                Destroy(bufferDone);
            }

            if (type != TCrash.none)
            {
                CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.Crash);
                StartCoroutine(CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Crash)
                    .GetComponent<CrashUI>().Start(type, currentVoice.GetComponent<AudioSource>().clip.length));

                bufferCrash = CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Crash).gameObject;
            }
        }

        Destroy(sound, 1.5f);
    }

    public void AddApply(TCrash type)
    {
        if (gameMode != TType.Gameplay)
        {
            return;
        }

        if (imun)
        {
            return;
        }

        if (Globals.Instance.coreProfile.listApply.Contains(type))
        {
            return;
        }

        if (Globals.Instance.coreProfile.listCrash.Contains(type))
        {
            return;
        }

        if (bufferDone || bufferCrash)
        {
            return;
        }

        GameObject sound = Instantiate(doneSound);

        if (currentVoice)
        {
            Destroy(currentVoice.gameObject);
        }

        currentVoice = Instantiate(crashSound);

        switch (type)
        {
            case TCrash.blind_spot:
                switch (Globals.Instance.language)
                {
                    case Globals.TLanguage.German:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[3];
                        break;
                    case Globals.TLanguage.Italyano:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[3];
                        break;
                    case Globals.TLanguage.Francua:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[3];
                        break;
                }

                break;

            case TCrash.oil_spills:
                switch (Globals.Instance.language)
                {
                    case Globals.TLanguage.German:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[1];
                        break;
                    case Globals.TLanguage.Italyano:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[1];
                        break;
                    case Globals.TLanguage.Francua:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[1];
                        break;
                }

                break;

            case TCrash.parked_car:
                switch (Globals.Instance.language)
                {
                    case Globals.TLanguage.German:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[0];
                        break;
                    case Globals.TLanguage.Italyano:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[0];
                        break;
                    case Globals.TLanguage.Francua:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[0];
                        break;
                }

                break;

            case TCrash.rail_tracks:
                switch (Globals.Instance.language)
                {
                    case Globals.TLanguage.German:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[2];
                        break;
                    case Globals.TLanguage.Italyano:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[2];
                        break;
                    case Globals.TLanguage.Francua:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[2];
                        break;
                }

                break;

            case TCrash.roundabout:
                switch (Globals.Instance.language)
                {
                    case Globals.TLanguage.German:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundGerman[4];
                        break;
                    case Globals.TLanguage.Italyano:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundItal[4];
                        break;
                    case Globals.TLanguage.Francua:
                        currentVoice.GetComponent<AudioSource>().clip = listCrashSoundFrench[4];
                        break;
                }

                break;
            case TCrash.none:
                break;
            default:
                Debug.Log("Check crash type");
                break;
        }

        Globals.Instance.coreProfile.AddApply(type);

        currentVoice.GetComponent<AudioSource>().Play();
        Destroy(currentVoice, currentVoice.GetComponent<AudioSource>().clip.length + 0.3f);


        if (bufferCrash)
        {
            Destroy(bufferCrash);
        }

        CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.Done);
        StartCoroutine(CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Done)
            .GetComponent<CrashUI>().Start(type, currentVoice.GetComponent<AudioSource>().clip.length));
        bufferDone = CoreAppControl.Instance.DialogApp.GetWindow(WindowControll.TTypeWindow.Done).gameObject;

        Destroy(sound, 1.5f);
    }

    public void BlinkSound()
    {
        GameObject sound = Instantiate(doneSound);
        Destroy(sound, 1.5f);
    }

    public void Calibration()
    {
        Transform target = ParadigmControl.Instance.transform;

        Vector3 fixRotation = PlayerControl.Instance.transform.eulerAngles;
        fixRotation.x = 0;
        fixRotation.y += 90;
        fixRotation.z = 0;

        target.eulerAngles = fixRotation;

        Vector3 fixDist = ParadigmControl.Instance.transform.position -
                          ParadigmControl.Instance.listTriggers[0].transform.position;
        fixDist.y = 0;

        Vector3 fixPosition = PlayerControl.Instance.transform.position + fixDist;
        fixPosition.y = ParadigmControl.Instance.transform.position.y;

        ParadigmControl.Instance.transform.position = fixPosition;
    }

    private void CoreUpdate()
    {
        if (gameMode == TType.Gameplay)
        {
            CoreAR.Instance.CoreUpdate();
        }
    }
}