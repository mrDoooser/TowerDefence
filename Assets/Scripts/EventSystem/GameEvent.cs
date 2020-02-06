using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "TowerDefence/Events/Create game event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(GameObject RaiseOwner)
    {
        int startCount = listeners.Count;
        for (int i = 0; i < listeners.Count; i++)
        {
            try
            {
                if(RaiseOwner)
                    listeners[i].OnEventRaised(RaiseOwner);
            }
            catch (System.Exception e)
            {
            }
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
            //Debug.Log("UnregisterListener for " + listener.gameObject);
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
