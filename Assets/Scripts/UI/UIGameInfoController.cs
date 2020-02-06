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
        OnPlayerHealhChanged();
        OnPlayerCoinsChanged();
        OnWavesCountChanged();
    }

    private void OnDestroy()
    {
    }

    public void OnPlayerHealhChanged()
    {
        _textMeshPlayerHealth.text = _playerController.CurrentHealth.ToString();
    }

    public void OnPlayerCoinsChanged()
    {
        _textMeshPlayerCoins.text = _playerController.CurrentCoins.ToString();
    }

    public void OnWavesCountChanged()
    {
        _textMeshPlayerWaves.text = (_wavesController.CurrentWave+1).ToString() + "/" + _wavesController.TotalWaves.ToString();
    }
}
