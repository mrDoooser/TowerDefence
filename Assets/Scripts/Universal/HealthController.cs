using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected GameEvent OnTakeDamageEvent;
    [SerializeField]
    protected GameEvent OnDieEvent;

    protected float _currentHealth;
    protected float _maxHealth;

    public float Health { get { return _currentHealth; } set { _currentHealth = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    public void Die()
    {
        if (OnDieEvent)
            OnDieEvent.Raise(gameObject);
    }

    public void Initialize(float MaxHealth)
    {
        _maxHealth = MaxHealth;
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(DamageType DamageType, float Damage)
    {
        _currentHealth = _currentHealth - (int)Damage;

        if (_currentHealth <= 0)
        {
            Die();
        }

        if (OnTakeDamageEvent)
            OnTakeDamageEvent.Raise(gameObject);
    }
}
