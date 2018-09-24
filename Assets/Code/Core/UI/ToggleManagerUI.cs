﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paradigm
{
    public class ToggleManagerUI : CoreUI
    {
        #region Data

        public bool initState = false;
        public List<ToggleUI> listToggle = new List<ToggleUI>();

        #endregion

        #region Unity

        private void Start()
        {
            Initialization();

            if (initState)
            {
                for (int i = 0; i < listToggle.Count; i++)
                {
                    if (i == 0)
                    {
                        listToggle[i].SwitchState(true);
                    }
                    else
                    {
                        listToggle[i].SwitchState(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < listToggle.Count; i++)
                {
                    listToggle[i].SwitchState(false);
                }
            }
        }

        #endregion

        #region Core

        public void Initialization()
        {
            foreach (ToggleUI item in GetComponentsInChildren<ToggleUI>())
            {
                item.Initialization(this);
                listToggle.Add(item);
            }
        }

        public void Selected(ToggleUI target)
        {
            CoreSelected(target);
        }

        public void Selected(int index)
        {
            if (index > listToggle.Count - 1)
            {
                DebugLog("ToogleManager Array error", DDebug.TColor.Error);
                return;
            }

            CoreSelected(listToggle[index]);
        }

        private void CoreSelected(ToggleUI target)
        {
            if (listToggle.Count == 0) { return; }

            foreach (ToggleUI Item in listToggle)
            {
                if (Item == target)
                {
                    target.SwitchState(true);
                }
                else
                {
                    Item.SwitchState(false);
                }
            }
        }

        #endregion
    }
}