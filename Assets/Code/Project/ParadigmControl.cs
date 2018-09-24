using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paradigm
{
    public class ParadigmControl : MonoBehaviour
    {
        public static ParadigmControl Instance;
        public GameObject city;
        public GameObject GOCity;
        public Transform content;
        public Transform center;

        public enum TState
        {
            Menu,
            Play
        }

        public TState typeState = TState.Menu;
        public List<FinishTrigger> listTriggers = new List<FinishTrigger>();
        public List<TrafficLight> listLight = new List<TrafficLight>();
        public List<AICar> listCars = new List<AICar>();
        public FinishTrigger startGameTrigger;
        public int curCounter = 0;

        public TruckAllow truckAllow;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StopGame();
            //GOCity.transform.localScale = new Vector3(PlayerControl.Instance.designerContructor.maxDistance / 15, PlayerControl.Instance.designerContructor.maxDistance / 15, PlayerControl.Instance.designerContructor.maxDistance / 15);

            //Vector3 fixPos = content.localPosition;
            //fixPos.x *= GOCity.transform.localScale.x;
            //fixPos.z *= GOCity.transform.localScale.z;

            //content.localPosition = fixPos;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                CellToPlay();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextTrigger();
            }
        }

        public void CellToPlay()
        {
            typeState = TState.Play;
            NextTrigger();
        }

        private void SetTriggres(int index)
        {
            curCounter = index;

            for (int i = 0; i < listTriggers.Count; i++)
            {
                listTriggers[i].gameObject.SetActive(i == index);
            }

            if (curCounter >= listTriggers.Count)
            {
                startGameTrigger.gameObject.SetActive(true);
            }

            /*else
            {
                startGameTrigger.gameObject.SetActive(false);
            }*/
        }

        public void StopGame()
        {
            for (int i = 0; i < listTriggers.Count; i++)
            {
                listTriggers[i].gameObject.SetActive(false);
            }

            startGameTrigger.gameObject.SetActive(false);

            curCounter = 0;

            ResetParadigm();
        }

        public void ResetParadigm()
        {
            foreach (AICar car in listCars)
            {
                car.ResetAI();
            }

            truckAllow.ResetTruck();
        }

        public void NextTrigger()
        {
            SetTriggres(curCounter);
            curCounter++;
        }

        public void ResetTriggres()
        {
            SetTriggres(listTriggers.Count + 1);
            curCounter = 0;
        }
    }
}