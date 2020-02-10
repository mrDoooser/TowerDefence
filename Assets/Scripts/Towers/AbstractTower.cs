using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public struct TargetData
{
    public AbstractEnemy EnemyController;
    public float TimeStamp;
    public void Load(AbstractEnemy EnemyController, float TimeStamp)
    {
        this.EnemyController = EnemyController;
        this.TimeStamp = TimeStamp;
    }

}

public class AbstractTower : MonoBehaviour
{
    [Inject]
    protected CastleController _castleController;

    [Inject]
    protected GameController _gameController;

    [SerializeField]
    protected GameEventListener OnEnemyDieEventListener;

    public TowerParams TowerParams { get; set; }

    protected SphereCollider _trigger;

    //protected Dictionary<int, AbstractEnemy> _potencialTargets = new Dictionary<int, AbstractEnemy>();
    protected Dictionary<int, TargetData> _potencialTargets = new Dictionary<int, TargetData>();

    protected bool _isFiring;

    public virtual void Initialize(TowerParams TowerParams)
    {
        this.TowerParams = TowerParams;
        if (!_trigger)
            AddTrigger();

    }

    protected virtual void AddTrigger()
    {
        _trigger = gameObject.AddComponent<SphereCollider>();
        _trigger.isTrigger = true;
        _trigger.radius = TowerParams.Range;
    }

    public int GetSellPrice()
    {
        return (int)(TowerParams.BuildPrice * TowerParams.SellCoefficient);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (_potencialTargets.ContainsKey(otherObject.GetInstanceID()))
            return;

        AbstractEnemy newTarget = otherObject.GetComponentInParent<AbstractEnemy>();
        if (newTarget)
        {
            TargetData newRecord = new TargetData();
            newRecord.Load(newTarget, newTarget.TimeStamp);
            _potencialTargets.Add(otherObject.GetInstanceID(), newRecord);
        }

        if (!_isFiring && _potencialTargets.Keys.Count > 0)
        {
            StartFire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (_potencialTargets.ContainsKey(otherObject.GetInstanceID()))
        {
            _potencialTargets.Remove(otherObject.GetInstanceID());
        }

        if (_potencialTargets.Count == 0)
        {
            StopFire();
        }

    }

    protected virtual void StartFire()
    {
        _isFiring = true;
        StartCoroutine(Fire());
    }

    protected virtual IEnumerator Fire()
    {
        while (_isFiring)
        {
            AbstractEnemy nextTarget = GetNextTarget();
            //Debug.Log(gameObject + " has target " + nextTarget);
            if (nextTarget)
            {
                //Debug.Log(gameObject + " prepare to fire in " + nextTarget.gameObject);
                //nextTarget.TakeDamage(TowerParams.DamageType, TowerParams.Damage);
                SpawnProjectile(nextTarget);
                Debug.DrawLine(transform.position, nextTarget.transform.position, Color.red);
                //Debug.Log(gameObject + " fire in " + nextTarget.gameObject);
            }
            else
            {
                StopFire();
            }

            yield return new WaitForSeconds(TowerParams.ShootInterval);
        }
    }

    protected virtual void SpawnProjectile(AbstractEnemy Target)
    {
        GameObject newProjectile = Instantiate(TowerParams.ProjectilePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        newProjectile.transform.parent = transform; 
        AbstractProjectile projectileController = newProjectile.GetComponent<AbstractProjectile>();
        if(projectileController)
        {
            projectileController.SetParams(Target, TowerParams.ProjectileSpeed, TowerParams.DamageType, TowerParams.Damage);
        }
    }

    protected virtual void OnEnemyDie(GameObject DiedEnemy)
    {
        if (_potencialTargets.ContainsKey(DiedEnemy.GetInstanceID()))
            _potencialTargets.Remove(DiedEnemy.GetInstanceID());
    }

    protected virtual void StopFire()
    {
        StopCoroutine(Fire());
        _isFiring = false;
    }

    protected virtual AbstractEnemy GetNextTarget()
    {
        if(_potencialTargets.Count == 0)
            return null;

        AbstractEnemy bestTarget = null;
        float bestTime = float.MaxValue;
        foreach (int id in _potencialTargets.Keys)
        {
            if (bestTime > _potencialTargets[id].TimeStamp && _potencialTargets[id].EnemyController)
            {
                bestTime = _potencialTargets[id].TimeStamp;
                bestTarget = _potencialTargets[id].EnemyController;
            }
        }

        return bestTarget;

    }
}
