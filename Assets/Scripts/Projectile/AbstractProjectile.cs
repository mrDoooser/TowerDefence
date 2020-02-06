using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractProjectile : MonoBehaviour
{
    protected GameObject _targetObject;
    protected AbstractEnemy _target;
    protected float _speed;
    protected DamageType _damageType;
    protected float _damage;
    protected bool _isActive;

    public void SetParams(AbstractEnemy Target, float Speed, DamageType DamageType, float Damage)
    {
        _target = Target;
        _speed = Speed;
        _damageType = DamageType;
        _damage = Damage;
        _targetObject = _target.gameObject;
        _isActive = true;
    }

    public void OnEnemyDie(GameObject DiedEnemy)
    {
        if (!_target || DiedEnemy == _targetObject)
            TargetLost();
    }

    void Update()
    {
        if (_target)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            TargetLost();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
            return;

        AbstractEnemy enemyController = other.gameObject.GetComponentInParent<AbstractEnemy>();
        if(enemyController && enemyController == _target)
        {
            _target.TakeDamage(_damageType, _damage);
            _isActive = false;
            Destroy(gameObject);

        }
    }
    void TargetLost()
    {
        Destroy(gameObject);
    }

    //protected void OnEnemyDie(AbstractEnemy Enemy)
    //{
    //    if(Enemy == _target || !_target)
    //    {
    //        TargetLost();
    //    }
    //}
}
