using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelConfig", menuName = "TowerDefence/Create game level config")]
public class GameLevelConfig : ScriptableObject
{
    [SerializeField]
    public string LevelName;

    [SerializeField]
    public PlayerParams PlayerParams;

    [SerializeField]
    public WavesParams WavesParams;

}


