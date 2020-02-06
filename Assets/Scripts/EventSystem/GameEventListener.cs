using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    public GameEvent Event;
    public GameEventObject Response;

    public void OnEventRaised(GameObject RaiseOwner)
    {
        Response.Invoke((GameObject)RaiseOwner);
    }

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    private void OnDestroy()
    {
        Event.UnregisterListener(this);
    }
}
