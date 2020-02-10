using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveParams", menuName = "TowerDefence/Create wave params")]
public class WaveParams : ScriptableObject
{
    [SerializeField]
    public EnemyParams EnemyParams;

    [SerializeField]
    public int EnemyNum;

    [SerializeField]
    public float InWaveDelay;

    [SerializeField]
    public float WaveTime;
}
