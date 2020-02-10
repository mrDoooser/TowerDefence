using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthController))]
public class AbstractEnemy : MonoBehaviour
{
    //public delegate void DieHandler(AbstractEnemy Died);
    //public static event DieHandler OnDie;
    //public static event DieHandler OnEnemyDestroy;

    [SerializeField]
    protected GameEvent OnDestroyEvent;

    [SerializeField]
    protected NavMeshAgent _navMeshAgent;

    protected EnemyParams _params;

    HealthController _healthController;

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
            _healthController.Initialize(_params.Health);
            //MaxHealth = _params.Health;
            //Health = MaxHealth;
        }
    }

    public float Health { get { return _healthController.Health; } }

    protected void Initialize()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _timeStamp = Time.timeSinceLevelLoad;
        _healthController = GetComponent<HealthController>();
        _healthController.Initialize(_params.Health);
    }

    
    public void GoToNextPoint(Vector3 NextPoint)
    {
        if (!_navMeshAgent)
            Initialize();

        _navMeshAgent.SetDestination(NextPoint);
    }

    public virtual void TakeDamage(DamageType DamageType, float Damage)
    {
        _healthController.TakeDamage(DamageType, Damage);

        //Health -= Damage;

        //// TODO: Show damage

        //if(Health <= 0)
        //{
        //    Health = 0;
        //    Die();
        //}
    }

    public virtual void Die(GameObject diedEnemy)
    {
        //if (OnDie!=null)
        //    OnDie(this);

        if(diedEnemy == gameObject)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //if (OnEnemyDestroy != null)
        //    OnEnemyDestroy(this);
        if (OnDestroyEvent)
            OnDestroyEvent.Raise(gameObject);
    }

    public void Initialize(float MaxHealth)
    {
        _healthController.Initialize(MaxHealth);
    }
}
