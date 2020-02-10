using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIActionTowerButton : MonoBehaviour
{
    [Inject]
    UIBuildTowerDialogueController _buildTowerDialogueController;

    [SerializeField]
    TowerParams TowerParams;

    [SerializeField]
    Image Image;

    [SerializeField]
    TextMeshProUGUI Price;

    [SerializeField]
    Button _button;


    // Start is called before the first frame update
    void Start()
    {
        Image.sprite = TowerParams.Sprite;
        Price.SetText(TowerParams.BuildPrice.ToString());
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        _buildTowerDialogueController.BuildTower(TowerParams);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
