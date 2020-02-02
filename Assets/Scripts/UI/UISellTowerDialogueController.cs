using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISellTowerDialogueController : UIAbstractInterfaseController
{
    public TowerSlot TowerSlot;

    [SerializeField]
    TextMeshProUGUI Price;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SellTower()
    {
        TowerSlot.SellTower();
        _UIController.HideCurrentDialogue();
    }

    public void Refresh()
    {
        Price.SetText(TowerSlot.GetSellPrice().ToString());
    }
}
