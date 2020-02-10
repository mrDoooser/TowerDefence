using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [Inject]
    UIGameInfoController _gameInfoController;
    [Inject]
    UIBuildTowerDialogueController _buildTowerDialogueController;
    [Inject]
    UISellTowerDialogueController _sellTowerDialogueController;
    [Inject]
    UIWinDialogueController _winDialogueController;
    [Inject]
    UILooseDialogueController _looseDialogueController;

    UIAbstractInterfaseController _currentDialogue;

    [SerializeField]
    bool ShowLog;

    [SerializeField]
    Text logText;


    public void Print(string message)
    {
        if(ShowLog)
            logText.text += " || " + message;
    }

    private void Start()
    {
        _gameInfoController.SetVisibility(true);
        _buildTowerDialogueController.SetVisibility(false);
        _sellTowerDialogueController.SetVisibility(false);
        _winDialogueController.SetVisibility(false);
        _looseDialogueController.SetVisibility(false);
    }

    public void ShowBuildDialogue(TowerSlot SelectedTowerSlot)
    {
        OpenDialogue(_buildTowerDialogueController);
        _buildTowerDialogueController.TowerSlot = SelectedTowerSlot;
        _buildTowerDialogueController.MoveToObject(SelectedTowerSlot.gameObject);
    }

    public void ShowSellDialogue(TowerSlot SelectedTowerSlot)
    {
        OpenDialogue(_sellTowerDialogueController);
        _sellTowerDialogueController.TowerSlot = SelectedTowerSlot;
        _sellTowerDialogueController.MoveToObject(SelectedTowerSlot.gameObject);
        _sellTowerDialogueController.Refresh();
    }

    public void HideCurrentDialogue()
    {
        if(_currentDialogue)
            _currentDialogue.SetVisibility(false);
    }

    public void ShowWinDialogue()
    {
        OpenDialogue(_winDialogueController);
    }

    public void ShowLooseDialogue()
    {
        OpenDialogue(_looseDialogueController);
    }

    void OpenDialogue(UIAbstractInterfaseController dialogue)
    {
        HideCurrentDialogue();
        _currentDialogue = dialogue;
        dialogue.SetVisibility(true);
    }
}
