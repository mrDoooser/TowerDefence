using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "TowerDefence/Create player params")]
public class PlayerParams : ScriptableObject
{
    [SerializeField]
    public int StartHealth = 100;

    [SerializeField]
    public int StartCoins = 10;
}
