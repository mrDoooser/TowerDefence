using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildTowerDialogueController : UIAbstractInterfaseController
{
    public TowerSlot TowerSlot;

    public void BuildTower(TowerParams NewTowerParams)
    {
        TowerSlot.BuildTower(NewTowerParams);
        _UIController.HideCurrentDialogue();
    }
}
