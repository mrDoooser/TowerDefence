using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public delegate void DieHandler();
    public static event DieHandler OnDie;

    public delegate void DamageTaked();
    public static event DamageTaked OnHealthChanged;

    public delegate void CoinsChanged();
    public static event CoinsChanged OnCoinsChanged;


    [Inject]
    protected GameLevelConfig _gameConfig;
    [Inject]
    UIController _UIController;
    [Inject]
    GameController _gameController;

    protected int _currentHealth;
    protected int _currentCoins;

    public int CurrentHealth { get { return _currentHealth; } }
    public int CurrentCoins { get { return _currentCoins; } }

    void Start()
    {
        Initialize();
        AbstractEnemy.OnDie += KillEnemy;
    }

    void Initialize()
    {
        _currentHealth = _gameConfig.PlayerParams.StartHealth;
        _currentCoins = _gameConfig.PlayerParams.StartCoins;
        if(OnCoinsChanged != null)
            OnCoinsChanged();
        if(OnCoinsChanged != null)
            OnHealthChanged();
    }

    private void OnDestroy()
    {
        AbstractEnemy.OnDie -= KillEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = _currentHealth - (int)damage;
        if (OnHealthChanged != null)
            OnHealthChanged();

        if(_currentHealth <= 0)
        {
            if(OnDie != null)
                OnDie();
        }
    }

    public void AddCoins(int AddiditionalCoins)
    {
        _currentCoins += AddiditionalCoins;
        OnCoinsChanged();
    }

    public void SpendCoins(int Price)
    {
        _currentCoins -= Price;
        OnCoinsChanged();
    }

    public bool IsEnoughCoins(int NeedCoins)
    {
        return NeedCoins <= _currentCoins;
    }

    void KillEnemy(AbstractEnemy Enemy)
    {
        AddCoins(Enemy.GetCoinsForKilling());
    }
}
