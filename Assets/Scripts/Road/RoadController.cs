using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField]
    GameObject[] CheckPoints;

    [SerializeField]
    GameObject StartPosition;


    public Vector3 GetStartCheckpoint()
    {
        return CheckPoints[0].transform.position;
    }


    public Vector3 GetNextCheckpoint(GameObject CurrentCheckpoint)
    {
        for (int i = 0; i < CheckPoints.Length-1; i++)
        {
            if(CheckPoints[i] == CurrentCheckpoint)
            {
                return CheckPoints[i + 1].transform.position;
            }
        }

        return CurrentCheckpoint.transform.position;
    }
}
