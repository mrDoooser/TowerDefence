using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //protected GameEvent OnTakeDamageEvent;
    //[SerializeField]
    //protected GameEvent OnDieEvent;
    [SerializeField]
    protected GameEvent OnCoinsChangedEvent;

    [SerializeField]
    protected GameEventListener OnEnemyDie;

    [Inject]
    protected GameLevelConfig _gameConfig;
    [Inject]
    UIController _UIController;
    [Inject]
    GameController _gameController;

    //protected int _currentHealth;
    protected int _currentCoins;

    public int CurrentHealth { get { return (int)_healthController.Health; } }
    public int CurrentCoins { get { return _currentCoins; } }

    HealthController _healthController;

    void Start()
    {
        Initialize();
        //AbstractEnemy.OnDie += KillEnemy;
    }

    void Initialize()
    {
        _healthController = GetComponent<HealthController>();
        _healthController.Initialize(_gameConfig.PlayerParams.StartHealth);

        //_currentHealth = _gameConfig.PlayerParams.StartHealth;
        _currentCoins = _gameConfig.PlayerParams.StartCoins;

        //if(OnTakeDamageEvent)
        //    OnTakeDamageEvent.Raise(gameObject);
        //if(OnCoinsChangedEvent)
        //    OnCoinsChangedEvent.Raise(gameObject);
    }

    private void OnDestroy()
    {
        //AbstractEnemy.OnDie -= KillEnemy;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(DamageType.DT_Physical, damage);
    }

    public void AddCoins(int AddiditionalCoins)
    {
        _currentCoins += AddiditionalCoins;
        if (OnCoinsChangedEvent)
            OnCoinsChangedEvent.Raise(gameObject);
    }

    public void SpendCoins(int Price)
    {
        _currentCoins -= Price;
        if (OnCoinsChangedEvent)
            OnCoinsChangedEvent.Raise(gameObject);
    }

    public bool IsEnoughCoins(int NeedCoins)
    {
        return NeedCoins <= _currentCoins;
    }

    void KillEnemy(AbstractEnemy Enemy)
    {
        AddCoins(Enemy.GetCoinsForKilling());
    }

    public void OnEnemyKilled(GameObject KilledEnemy)
    {
        AbstractEnemy Enemy = KilledEnemy.GetComponent<AbstractEnemy>();
        AddCoins(Enemy.GetCoinsForKilling());
    }
}
