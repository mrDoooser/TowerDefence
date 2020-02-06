using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "TowerDefence/Events/Create game event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(GameObject RaiseOwner)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(RaiseOwner);
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!InList(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (InList(listener))
        {
            listeners.Remove(listener);
        }
    }

    private bool InList(GameEventListener listener)
    {
        GameEventListener foundedListener = listeners.Find(item => item == listener);
        if(foundedListener)
        {
            return true;
        }

        return false;
    }
    

}
