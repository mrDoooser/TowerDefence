using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AbstractEnemy : MonoBehaviour, IDamagable
{
    public delegate void DieHandler(AbstractEnemy Died);
    public static event DieHandler OnDie;
    public static event DieHandler OnEnemyDestroy;

    [SerializeField]
    protected NavMeshAgent _navMeshAgent;

    protected EnemyParams _params;

    float _timeStamp;
    public float TimeStamp { get { return _timeStamp; } }

    public int GetCoinsForKilling()
    {
        return Random.Range(_params.CoinsForKillingMin, _params.CoinsForKillingMax);
    } 

    public float Damage { get { return _params.Damage; } }

    public EnemyParams Params
    {
        get
        {
            return _params;
        }
        set {
            _params = value;

            if (!_navMeshAgent)
                Initialize();

            _navMeshAgent.speed = _params.MovingSpeed;
            MaxHealth = _params.Health;
            Health = MaxHealth;
        }
    }

    public float Health { get; set; }
    public float MaxHealth { get; set; }

    protected void Initialize()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _timeStamp = Time.timeSinceLevelLoad;
    }

    
    public void GoToNextPoint(Vector3 NextPoint)
    {
        if (!_navMeshAgent)
            Initialize();

        _navMeshAgent.SetDestination(NextPoint);
    }

    public virtual void TakeDamage(DamageType DamageType, float Damage)
    {
        Health -= Damage;

        // TODO: Show damage

        if(Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        if (OnDie!=null)
            OnDie(this);

        // TODO: Show coins num

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (OnEnemyDestroy != null)
            OnEnemyDestroy(this);
    }
}
