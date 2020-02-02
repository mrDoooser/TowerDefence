using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WavesController : MonoBehaviour
{
    public delegate void WavesEventdHandler();
    public event WavesEventdHandler OnWavesEnded;
    public event WavesEventdHandler OnNewWaveStarted;

    [Inject]
    EnemiesSpawnerController _enemiesSpawnerController;

    [Inject]
    GameLevelConfig gameConfig;
        
    WavesParams _wavesParams;
    
    int _currentWaveIndex = 0;

    public int CurrentWave { get { return _currentWaveIndex; } }
    public int TotalWaves { get { return _wavesParams.Waves.Length; } }
    //public int TotalWaves { get { return 0; } }


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
        if(_wavesParams.Waves.Length <= _currentWaveIndex && OnWavesEnded != null)
        {
            OnWavesEnded();
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
        if (OnNewWaveStarted != null)
            OnNewWaveStarted();

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
            // TODO: End game event or signal
            Debug.Log("End game after " + _currentWaveIndex + " waves");
        }
    }

    IEnumerator EndWaveByTimer(float time)
    {
        yield return new WaitForSeconds(time);

        EndWave();
    }
}
