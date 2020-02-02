using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    public delegate void TowerSlotSelectionHandler(TowerSlot SelectedTowerSlot);
    public event TowerSlotSelectionHandler OnTowerSlotSelected;

    Camera _camera;

    [Inject]
    GameLevelConfig _gameLevelConfig;

    [Inject]
    WavesController _wavesController;

    [Inject]
    TimeController _timeController;

    [Inject]
    UIController _UIController;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        StartCoroutine(StartWithDelay());
    }

    void Initialize()
    {
        _camera = Camera.main;
        PlayerController.OnDie += PlayerLoose;
        _wavesController.OnWavesEnded += PlayerWin;
    }

    private void OnDestroy()
    {
        PlayerController.OnDie -= PlayerLoose;
        _wavesController.OnWavesEnded -= PlayerWin;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        _wavesController.Activate();
        _timeController.Play();
    }

    public void PlayerWin()
    {
        _timeController.Pause();
        _UIController.ShowWinDialogue();
    }

    public void PlayerLoose()
    {
        _timeController.Pause();
        _UIController.ShowLooseDialogue();
    }

    public void Replay()
    {
        Application.LoadLevel(_gameLevelConfig.LevelName);
    }

    IEnumerator StartWithDelay()
    {
        _timeController.Play();
        yield return new WaitForSeconds(0.1f);
        StartGame();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //CheckTowerSlotSelection(); // TODO: Fix bug with button clicks
            StartCoroutine(CheckTowerSlotSelectionWithDelay());
        }
    }

    IEnumerator CheckTowerSlotSelectionWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        CheckTowerSlotSelection();
    }

    void CheckTowerSlotSelection()
    {
        RaycastHit[] hits;
        int layerMask = 1 << 9;
        hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition), 100f, layerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            TowerSlot towerSlot = hits[i].collider.transform.GetComponentInParent<TowerSlot>();
            if (towerSlot && OnTowerSlotSelected != null)
            {
                OnTowerSlotSelected(towerSlot);
                return;
            }
        }
        OnTowerSlotSelected(null);
    }
}




