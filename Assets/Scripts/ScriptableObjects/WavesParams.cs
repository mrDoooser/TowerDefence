using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavesParams", menuName = "TowerDefence/Create waves params")]
public class WavesParams : ScriptableObject
{
    [SerializeField]
    public WaveParams[] Waves;
}
