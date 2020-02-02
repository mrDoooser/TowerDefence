using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameParamsInstaller", menuName = "TowerDefence/Installer/Create game params installer")]
public class GameParamsInataller : ScriptableObjectInstaller
{
    [SerializeField]
    private GameLevelConfig _gameConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameConfig);
        Debug.Log("--InstallBindings--");
    }
}
