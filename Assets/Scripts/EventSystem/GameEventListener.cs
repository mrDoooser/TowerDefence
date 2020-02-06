using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    public GameEvent Event;
    //public UnityEvent<GameObject> Response;
    //public UnityEvent Response;
    public GameEventObject Response;

    public void OnEventRaised(GameObject RaiseOwner)
    {
        Response.Invoke(RaiseOwner);
        //Response.Invoke();
    }

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
}
