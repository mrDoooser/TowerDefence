using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CheckpointController : MonoBehaviour
{
    [Inject]
    protected RoadController _roadController; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AbstractEnemy enemy = other.gameObject.GetComponent<AbstractEnemy>();
        //Debug.Log(gameObject + " collide with " + other.gameObject);
        if (enemy)
        {
            //Debug.Log(gameObject + " it's enemy!");
            enemy.GoToNextPoint(_roadController.GetNextCheckpoint(gameObject));
        }
    }

}
