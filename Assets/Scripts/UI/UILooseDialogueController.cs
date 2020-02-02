using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILooseDialogueController : UIAbstractInterfaseController
{
    [Inject]
    GameController _gameController;

    private void Start()
    {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnReplay);
    }


    public void OnReplay()
    {
        _gameController.Replay();
    }
}
