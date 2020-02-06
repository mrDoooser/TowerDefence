using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesSpawnerController : MonoBehaviour
{
    public delegate void AllSpawnedEnemiesDestroyedHandler();
    public event AllSpawnedEnemiesDestroyedHandler OnAllSpawnedEnemiesDestroyed;

    
    [Inject]
    EnemiesGroup _enemiesGroup;

    [SerializeField]
    GameObject StartOfTheRoad;

    [SerializeField]
    GameObject EnemyPrefab;

    [Inject]
    protected RoadController _roadController;

    WaveParams _waveParams;

    protected Dictionary<int, AbstractEnemy> _spawnedEnemies = new Dictionary<int, AbstractEnemy>();

    bool _spawning;

    private void Start()
    {
        //AbstractEnemy.OnEnemyDestroy += OnEnemyDestroyed;
    }

    private void OnDestroy()
    {
        //AbstractEnemy.OnEnemyDestroy -= OnEnemyDestroyed;
    }

    public void OnEnemyDied(GameObject DiedEnemy)
    {
        if (!DiedEnemy)
            return;

        int id = DiedEnemy.GetInstanceID();
        if (_spawnedEnemies.ContainsKey(id))
        {
            _spawnedEnemies.Remove(id);

            if(_spawnedEnemies.Count == 0 && OnAllSpawnedEnemiesDestroyed != null)
            {
                OnAllSpawnedEnemiesDestroyed();
            }

        }
    }

    public void SpawnWave(WaveParams waveParams)
    {
        _waveParams = waveParams;

        if (_spawning)
            StopCoroutine(AutoSpawn());

        _spawning = true;
       StartCoroutine(AutoSpawn());
    }


    IEnumerator AutoSpawn()
    {
        for (int i = 0; i < _waveParams.EnemyNum; i++)
        {
            GameObject newEnemy = Instantiate(_waveParams.EnemyParams.Prefab, StartOfTheRoad.transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemiesGroup.transform;
            AbstractEnemy enemyController = newEnemy.GetComponent<AbstractEnemy>();
            if (enemyController)
            {
                enemyController.Params = _waveParams.EnemyParams;
                enemyController.GoToNextPoint(_roadController.GetStartCheckpoint());

                _spawnedEnemies.Add(newEnemy.GetInstanceID(), enemyController);
                yield return new WaitForSeconds(_waveParams.InWaveDelay);
            }
        }

        _spawning = false; 
    }

}
