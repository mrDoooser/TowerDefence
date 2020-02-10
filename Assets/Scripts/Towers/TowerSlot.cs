using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerSlot : MonoBehaviour
{
    [Inject]
    PlayerController _playerController;

    [Inject]
    GameController _gameController;

    bool HasTower { get { return _buildedTower != null; } }

    [SerializeField]
    bool _isSelected;

    [SerializeField]
    AbstractTower _buildedTower;

    public TowerParams TowerParams { get { if (_buildedTower) return _buildedTower.TowerParams; else return null; } }

    [Inject]
    UIController _UIController;

    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        _gameController.OnTowerSlotSelected -= OnTowerSlotSelection;
    }

    protected void Initialize()
    {
        _gameController.OnTowerSlotSelected += OnTowerSlotSelection;
    }

    protected void OnTowerSlotSelection(TowerSlot SelectedTowerSlot)
    {
        if(_isSelected && !SelectedTowerSlot)
        {
            Unselect(true);
        }
        else if (_isSelected && SelectedTowerSlot && SelectedTowerSlot != this)
        {
            Unselect(false);
        }
        else if (SelectedTowerSlot && !_isSelected && SelectedTowerSlot == this)
        {
            Select();
        }
    }

    public void Select()
    {
        _isSelected = true;
        if(HasTower)
        {
            // TODO: Show Sell dialogue
            _UIController.ShowSellDialogue(this);
        }
        else
        {
            // TODO: Show build dialogue
            _UIController.ShowBuildDialogue(this);
        }
    }

    public void Unselect(bool NeedCloseDialogue)
    {
        _isSelected = false;
        if(NeedCloseDialogue)
            _UIController.HideCurrentDialogue();
    }


    public void SellTower()
    {
        Destroy(_buildedTower.gameObject);
        _playerController.AddCoins(GetSellPrice());
    }

    public void BuildTower(TowerParams NewTowerParams)
    {
        // TODO: 1. Check player's coins
        if (_playerController.IsEnoughCoins(NewTowerParams.BuildPrice))
        {
            // TODO: 2. Increace player's coins
            _playerController.SpendCoins(NewTowerParams.BuildPrice);

            // TODO: 3. Spawn tower
            GameObject newTower = Instantiate(NewTowerParams.Prefab, transform.position, Quaternion.identity);
            _buildedTower = newTower.GetComponent<AbstractTower>();
            _buildedTower.Initialize(NewTowerParams);

            // TODO: 4. COnnect tower and slot
            newTower.transform.parent = transform.parent;
        }
    }

    public int GetSellPrice()
    {
        return _buildedTower.GetSellPrice();
    }

}
