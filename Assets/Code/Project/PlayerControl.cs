using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paradigm
{
    public class PlayerControl : MonoBehaviourSingleton<PlayerControl>
    {
        [Header("Ground")] public bool initGroud = false;
        private bool groundStart = false;
        public GameObject prefabGround;
        public GameObject currentGround;

        public float velocity;
        private Vector3 latePosition;

        public DesignConstrctor designerContructor;

        private void Start()
        {
            StartCoroutine(CalculationVelocity());
        }

        public void Inititalization()
        {
            //InputManager.Instance.startHold += GroundStart;
            //InputManager.Instance.startHold += designerContructor.FixPoint;
            //designerContructor.Initialization();

            /* ------------------- */
            if (!initGroud)
            {
                initGroud = true;
                designerContructor.midleWorldPoint = transform.position;

                designerContructor.fixInit = true;
                designerContructor.typeDesign = DesignConstrctor.TDesign.Stop;
                GameManager.Instance.Home();

                GroundStart();
            }

            /* ------------------- */
        }

        private IEnumerator CalculationVelocity()
        {
            latePosition = transform.position;
            yield return new WaitForSeconds(0.2f);

            velocity = (latePosition - transform.position).magnitude;

            StartCoroutine(CalculationVelocity());
        }

        public void ResetInitialization()
        {
            //InputManager.Instance.startHold -= GroundStart;
            //InputManager.Instance.startHold -= designerContructor.FixPoint;
        }

        public void CoreUpdate()
        {
            //Как только мы нашли поверхность стартуем игру
            //if(CoreAR.Instance.init)
            //{
            if (designerContructor.fixInit)
            {
                InitGround();
            }
            else
            {
                designerContructor.CoreUpdate();
                designerContructor.PointControl(CoreAR.Instance.hitUpdatePosition);
            }

            //}
        }

        protected void Update()
        {
            CoreUpdate();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GroundStart();
            }
        }

        private void OnConstructor()
        {
        }

        private void InitGround()
        {
            if (initGroud)
            {
                return;
            }

            //cursorEnable = false;
            initGroud = true;

            //CoreAppControl.Instance.DialogApp.CellWindow(WindowControll.TTypeWindow.PreStart);
            GroundStart();
        }

        private void GroundStart()
        {
            if (initGroud == false || groundStart == true)
            {
                return;
            }

            currentGround = CoreAR.Instance.InstantiateObj(prefabGround);

            //currentGround.transform.rotation = Quaternion.Euler(0.0f, currentGround.transform.rotation.eulerAngles.y, currentGround.transform.rotation.z);
            currentGround.transform.rotation = Quaternion.identity;

            Vector3 newPos = currentGround.transform.position;

            newPos.x = designerContructor.midleWorldPoint.x;
            newPos.z = designerContructor.midleWorldPoint.z;

            currentGround.transform.position = newPos;
            currentGround.GetComponent<ParadigmControl>().city.SetActive(false);
            groundStart = true;
        }
    }
}