using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DamageType
{
    DT_Magic,
    DT_Fire,
    DT_Physical
}

[CreateAssetMenu(fileName = "TowerParams", menuName = "TowerDefence/Create tower params")]
public class TowerParams : ScriptableObject
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public string Description;

    [SerializeField]
    public int BuildPrice = 10;

    [SerializeField]
    public float SellCoefficient = 0.5f;

    [SerializeField]
    public float Range = 1;

    [SerializeField]
    public float ShootInterval = 1;

    [SerializeField]
    private DamageType damageType;
    internal DamageType DamageType { get => damageType; set => damageType = value; }

    [SerializeField]
    public float Damage = 10;

    [SerializeField]
    public Sprite Sprite;
    
    [SerializeField]
    public GameObject Prefab;

    [SerializeField]
    public float ProjectileSpeed = 1;

    [SerializeField]
    public GameObject ProjectilePrefab;
}
