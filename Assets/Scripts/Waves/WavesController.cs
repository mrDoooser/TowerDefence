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
    
    int _currentWaveIndex = 0;

    public int CurrentWave { get { return _currentWaveIndex; } }
    public int TotalWaves { get { return _wavesParams.Waves.Length; } }

    private void Start()
    {
        _wavesParams = gameConfig.WavesParams;
        _enemiesSpawnerController.OnAllSpawnedEnemiesDestroyed += OnDestroyedAllEnemiesInWave;
    }

    private void OnDestroy()
    {
        _enemiesSpawnerController.OnAllSpawnedEnemiesDestroyed -= OnDestroyedAllEnemiesInWave;
    }

    public void OnDestroyedAllEnemiesInWave()
    {
        if(_wavesParams.Waves.Length <= _currentWaveIndex && OnWavesEndedEvent != null)
        {
            OnWavesEndedEvent.Raise(gameObject);
        }
    }

    public void Activate()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        Debug.Log("Start wave " + _currentWaveIndex);
        _enemiesSpawnerController.SpawnWave(_wavesParams.Waves[_currentWaveIndex]);
        if (OnNewWaveStartedEvent != null)
            OnNewWaveStartedEvent.Raise(gameObject);

        StartCoroutine(EndWaveByTimer(_wavesParams.Waves[_currentWaveIndex].WaveTime));
    }

    void EndWave()
    {
        Debug.Log("End wave " + _currentWaveIndex);
        _currentWaveIndex++;
        if(_wavesParams.Waves.Length > _currentWaveIndex)
        {
            StartNextWave();
        }
        else
        {
            Debug.Log("End game after " + _currentWaveIndex + " waves");
        }
    }

    IEnumerator EndWaveByTimer(float time)
    {
        yield return new WaitForSeconds(time);

        EndWave();
    }
}
