﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paradigm
{
    public class DataTime
    {
        public short index;
        public DateTime startTime;
        public DateTime endTime;
        public TimeSpan result;
        public TimeSpan awaySecond;
        public bool start = false;
    }

    public class CoreWorldTime
    {
        [Header("Data")]
        public List<DataTime> listDataTime = new List<DataTime>();

        public float worldTimeScale { get; private set; }
        private float lookTimeScale = 1;
        private float lookTimeScaleLerp = 1f;

        #region Core

        public void SetTimer(DateTime newTime)
        {
            DataTime newData = new DataTime();

            newData.index = (short)(listDataTime.Count);
            newData.startTime = DateTime.Now;
            newData.endTime = newTime;
            newData.start = true;

            listDataTime.Add(newData);
        }

        public short AddTimer(int delaySecond)
        {
            DataTime newData = new DataTime();

            newData.index = (short)(listDataTime.Count);
            newData.startTime = DateTime.Now;
            newData.endTime = DateTime.Now.AddSeconds(delaySecond);
            newData.start = true;

            listDataTime.Add(newData);

            return (short)(listDataTime.Count - 1);
        }

        public void AwaeSecond(int index, int second)
        {
            listDataTime[index].awaySecond -= new TimeSpan(0, 0, second);
        }

        public void RefreshTimer(short index, int delaySecond)
        {
            foreach(DataTime time in listDataTime)
            {
                if(time.index == index)
                {
                    time.startTime = DateTime.Now;
                    time.endTime = DateTime.Now.AddSeconds(delaySecond);
                }
            }
        }

        public void StartTimer(int index)
        {
            foreach (DataTime time in listDataTime)
            {
                if (time.index == index)
                {
                    time.start = true;
                    return;
                }
            }
        }

        public void StopTimer(int index)
        {
            foreach (DataTime time in listDataTime)
            {
                if (time.index == index)
                {
                    time.endTime = DateTime.Now;
                    time.start = false;
                    return;
                }
            }
        }

        public void CoreUpdate()
        {
            ResultWorldTime();
            LoopTimeScale();
        }

        private void ResultWorldTime()
        {
            foreach(DataTime time in listDataTime)
            {
                if (time.start && DateTime.Now <= time.endTime)
                {
                    time.result = (time.endTime + time.awaySecond) - DateTime.Now;
                }
                else
                {
                    time.start = false;
                }
            }
        }

        #region Time Scale

        public void SetTimeScale(float newTimer, float newLookTimeScaleLerp = 1f)
        {
            if (newTimer < 0) { return; }
            if (newLookTimeScaleLerp < 0 || newLookTimeScaleLerp > 1) { return; }

            lookTimeScale = newTimer;
            lookTimeScaleLerp = newLookTimeScaleLerp;
        }

        private void LoopTimeScale()
        {
            if(Math.Abs(worldTimeScale - lookTimeScale) >= 0.01f)
            {
                worldTimeScale = Mathf.Lerp(worldTimeScale, lookTimeScale, lookTimeScaleLerp * Time.deltaTime);
                Time.timeScale = worldTimeScale;
            }
        }

        #endregion

        #endregion
    }
}