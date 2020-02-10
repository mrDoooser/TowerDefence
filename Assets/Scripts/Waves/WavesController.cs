using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WavesController : MonoBehaviour
{
    [SerializeField]
    protected GameEvent OnWavesEndedEvent;
    [SerializeField]
    protected GameEvent OnNewWaveStartedEvent;


    [Inject]
    EnemiesSpawnerController _enemiesSpawnerController;

    [Inject]
    GameLevelConfig gameConfig;
        
    WavesParams _wavesParams;


    [Inject]
    UIController _UIController;

    int _currentWaveIndex = 0;

    public int CurrentWave { get { return _currentWaveIndex; } }
    public int TotalWaves { get { return _wavesParams.Waves.Length; } }

    private void Start()
    {
        _wavesParams = gameConfig.WavesParams;
        EnemiesSpawnerController.OnAllSpawnedEnemiesDestroyed += OnDestroyedAllEnemiesInWave;
    }

    private void OnDestroy()
    {
        EnemiesSpawnerController.OnAllSpawnedEnemiesDestroyed -= OnDestroyedAllEnemiesInWave;
    }

    public void OnDestroyedAllEnemiesInWave()
    {
        _UIController.Print("WC: Check - waves ended? [" + _wavesParams.Waves.Length + "<=" + (_currentWaveIndex+1) + "?]");
        if (_wavesParams.Waves.Length <= _currentWaveIndex && OnWavesEndedEvent != null)
        {
            OnWavesEndedEvent.Raise(gameObject);
            _UIController.Print("WC: ALL waves ended?");
        }
    }

    public void Activate()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        Debug.Log("Start wave " + _currentWaveIndex);
        _UIController.Print("WC: Start wave " + _currentWaveIndex);
        _enemiesSpawnerController.SpawnWave(_wavesParams.Waves[_currentWaveIndex]);
        if (OnNewWaveStartedEvent != null)
            OnNewWaveStartedEvent.Raise(gameObject);

        StartCoroutine(EndWaveByTimer(_wavesParams.Waves[_currentWaveIndex].WaveTime));
    }

    void EndWave()
    {
        Debug.Log("End wave " + _currentWaveIndex);
        _UIController.Print("WC: End wave " + _currentWaveIndex);
        _currentWaveIndex++;
        if(_wavesParams.Waves.Length > _currentWaveIndex)
        {
            StartNextWave();
        }
        else
        {
            Debug.Log("End game after " + _currentWaveIndex + " waves");
            _UIController.Print("WC: End game after " + _currentWaveIndex + " waves");
            OnWavesEndedEvent.Raise(gameObject);
        }
    }

    IEnumerator EndWaveByTimer(float time)
    {
        yield return new WaitForSeconds(time);

        EndWave();
    }
}
