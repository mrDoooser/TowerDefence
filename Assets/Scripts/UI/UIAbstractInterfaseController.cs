using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIAbstractInterfaseController : MonoBehaviour
{

    protected RectTransform _rectTransform;
    protected Camera _camera;

    [Inject]
    protected UIController _UIController;

    public void Initialize()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _camera = Camera.main;
    }

    public void SetVisibility(bool NewVisibility)
    {
        gameObject.SetActive(NewVisibility);
    }

    public void MoveToObject(GameObject SelectedObject)
    {
        if (!_rectTransform || !_camera)
            Initialize();

        _rectTransform.position = _camera.WorldToScreenPoint(SelectedObject.transform.position);
    }
}
