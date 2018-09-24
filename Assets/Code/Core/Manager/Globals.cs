﻿using System.Collections;
using System.Collections.Generic;
using Paradigm.SystemSaveAndLoad;


namespace Paradigm
{
    public class Globals : Singleton<Globals>
    {
        public enum TLanguage
        {
            German, Francua, Italyano
        }

        public TLanguage language;

        //Local
        public CoreProfile coreProfile;
        public CoreWorldTime coreWorldTime;
        public CorePlayerPrefs corePlayerPrefs;
        public CoreLeaderboard coreLeaderboard;

        //Public


        public void Initialization()
        {
            coreProfile = new CoreProfile();
            coreWorldTime = new CoreWorldTime();
            corePlayerPrefs = new CorePlayerPrefs();
            coreLeaderboard = new CoreLeaderboard();

            Load();
        }

        public void CoreUpdate()
        {
            coreWorldTime.CoreUpdate();
        }

        #region Save and Load

        private void Load()
        {
        }

        public void Save()
        {
        }

        #endregion
    }
}
