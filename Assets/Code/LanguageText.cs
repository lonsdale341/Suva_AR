using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Paradigm;

public class LanguageText : CoreUI
{
    public string currentText;

    private TextMeshProUGUI txt;
    private FinishTrigger trigger;

    public bool point;

    public string german;
    public string franci;
    public string italia;

    public void Awake()
    {
        Initialization();
    }

    public void Initialization()
    {
        if (!point)
        {
            txt = GetComponent<TextMeshProUGUI>();
        }
        else
        {
            trigger = GetComponent<FinishTrigger>();
        }
    }

    private void Update()
    {
        Control();
    }

    public void Control()
    {
        if (!point && !txt) { return; }
        if (point && !trigger) { return; }

        if (!point)
        {
            switch (Globals.Instance.language)
            {
                case Globals.TLanguage.Francua: SetString(txt, franci); return;
                case Globals.TLanguage.German: SetString(txt, german); return;
                case Globals.TLanguage.Italyano: SetString(txt, italia); return;
            }
        }
        else
        {
            switch (Globals.Instance.language)
            {
                case Globals.TLanguage.Francua: currentText = franci; return;
                case Globals.TLanguage.German: currentText = german; return;
                case Globals.TLanguage.Italyano: currentText = italia; return;
            }
        }
    }
}
