using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIGameInfoController : UIAbstractInterfaseController
{
    [Inject]
    PlayerController _playerController;

    [Inject]
    WavesController _wavesController;

    [SerializeField]
    TextMeshProUGUI _textMeshPlayerHealth;

    [SerializeField]
    TextMeshProUGUI _textMeshPlayerCoins;

    [SerializeField]
    TextMeshProUGUI _textMeshPlayerWaves;

    void Start()
    {
        PlayerController.OnHealthChanged += OnPlayerHealhChanged;
        PlayerController.OnCoinsChanged += OnPlayerCoinsChanged;
        _wavesController.OnNewWaveStarted += OnWavesCountChanged;

        OnPlayerHealhChanged();
        OnPlayerCoinsChanged();
        OnWavesCountChanged();
    }

    private void OnDestroy()
    {
        PlayerController.OnHealthChanged -= OnPlayerHealhChanged;
        PlayerController.OnCoinsChanged -= OnPlayerCoinsChanged;
        _wavesController.OnNewWaveStarted -= OnWavesCountChanged;
    }

    void OnPlayerHealhChanged()
    {
        _textMeshPlayerHealth.text = _playerController.CurrentHealth.ToString();
    }

    void OnPlayerCoinsChanged()
    {
        _textMeshPlayerCoins.text = _playerController.CurrentCoins.ToString();
    }

    void OnWavesCountChanged()
    {
        _textMeshPlayerWaves.text = (_wavesController.CurrentWave+1).ToString() + "/" + _wavesController.TotalWaves.ToString();
    }
}
