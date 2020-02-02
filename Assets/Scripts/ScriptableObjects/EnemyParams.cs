using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParams", menuName = "TowerDefence/Create enemy params")]
public class EnemyParams : ScriptableObject
{
    [SerializeField]
    public int Health;
    [SerializeField]
    public float MovingSpeed;
    [SerializeField]
    public int CoinsForKillingMin = 10;
    [SerializeField]
    public int CoinsForKillingMax = 20;
    [SerializeField]
    public float Damage;
    [SerializeField]
    public GameObject Prefab;
}
