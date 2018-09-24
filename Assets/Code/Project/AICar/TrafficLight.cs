using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [System.Serializable]
    public class DataTrafficLight
    {
        public enum TTypeLight
        {
            Red,
            Yellow,
            Green
        }

        public TTypeLight typeLight = TTypeLight.Green;
        public float timer;
        public Material matLight;
        public MeshRenderer targetMesh;
    }

    public DataTrafficLight.TTypeLight typeLight;
    public float curTimer = 0;

    public List<DataTrafficLight> listTraffictLight = new List<DataTrafficLight>();
    private bool inversiaLight = false;
    private bool speedRed = false;

    private void Update()
    {
        TrafficLightControl();

        if (!speedRed && Paradigm.PlayerControl.Instance.velocity >= 0.4f && Vector3.Distance(transform.position, Paradigm.PlayerControl.Instance.transform.position) <= 5f)
        {
            speedRed = true;
        }
    }

    private void TrafficLightControl()
    {
        curTimer += Time.deltaTime;

        for (int i = 0; i < listTraffictLight.Count; i++)
        {
            if(typeLight == listTraffictLight[i].typeLight)
            {
                listTraffictLight[i].targetMesh.enabled = true;

                if(curTimer >= GetTimer(listTraffictLight[i]))
                {
                    int getIndex = 0;

                    if(!inversiaLight)
                    {
                        getIndex = GetIndex(typeLight) + 1;
                    }
                    else
                    {
                        getIndex = GetIndex(typeLight) - 1;
                    }

                    if(getIndex > 2 || getIndex < 0)
                    {
                        getIndex = 1;
                        inversiaLight = !inversiaLight;
                    }

                    typeLight = GetType(getIndex);

                    curTimer = 0;
                }
            }
            else
            {
                listTraffictLight[i].targetMesh.enabled = false;
            }
        }
    }

    private DataTrafficLight.TTypeLight GetType(int index)
    {
        if(index == 0)
        {
            return DataTrafficLight.TTypeLight.Red;
        }
        else if(index == 1)
        {
            speedRed = false;
            return DataTrafficLight.TTypeLight.Yellow;
        }
        else
        {
            speedRed = false;
            return DataTrafficLight.TTypeLight.Green;
        }
    }

    private int GetIndex(DataTrafficLight.TTypeLight type)
    {
        if(type == DataTrafficLight.TTypeLight.Red)
        {
            return 0;
        }
        else if(type == DataTrafficLight.TTypeLight.Yellow)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    private float GetTimer(DataTrafficLight type)
    {
        if(type.typeLight == DataTrafficLight.TTypeLight.Red)
        {
            return type.timer * 3;
        }

        return type.timer;
    }
}
