using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public void Play()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}
