using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private float borderSize = 100f;

    // local variables
    private static bool _isPointerActive = false;
    private Camera _camera;
    private static Transform _pointerTransform;
    private static Vector3 _toPosition;
    private Vector3 _direction;
    private float _angle;
    private Vector3 target2ScreenPoint;
    private bool _isOffScreen;
    private Vector3 cappedTargetScreenPosition;

    private void Awake()
    {
        _pointerTransform = transform.Find("Pointer").GetComponent<Transform>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_isPointerActive)
            SetPointerPosition();

        void SetPointerPosition()
        {
            target2ScreenPoint = _camera.WorldToScreenPoint(_toPosition);
            _isOffScreen = target2ScreenPoint.x <= borderSize ||
                           target2ScreenPoint.x >= Screen.width - borderSize ||
                           target2ScreenPoint.y <= borderSize ||
                           target2ScreenPoint.y >= Screen.height - borderSize;

            if (_isOffScreen)
            {
                if (_pointerTransform.gameObject.activeSelf == false)
                    _pointerTransform.gameObject.SetActive(true);

                cappedTargetScreenPosition = target2ScreenPoint;

                if (target2ScreenPoint.x <= borderSize)
                    cappedTargetScreenPosition.x = borderSize;
                if (target2ScreenPoint.x >= Screen.width - borderSize)
                    cappedTargetScreenPosition.x = Screen.width - borderSize;
                if (target2ScreenPoint.y <= borderSize)
                    cappedTargetScreenPosition.y = borderSize;
                if (target2ScreenPoint.y >= Screen.height - borderSize)
                    cappedTargetScreenPosition.y = Screen.height - borderSize;

                _pointerTransform.position = cappedTargetScreenPosition;
            }
            else
            {
                if (_pointerTransform.gameObject.activeSelf)
                    _pointerTransform.gameObject.SetActive(false);
            }

            _direction = (_toPosition - _camera.transform.position);
            _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;
            _pointerTransform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        }
    }

    public static void EnablePointer(Vector3 target)
    {
        _isPointerActive = true;
        
        _toPosition = target;
        _pointerTransform.gameObject.SetActive(true);
    }

    public static void DisablePointer()
    {
        _isPointerActive = false;
        
        _pointerTransform.gameObject.SetActive(false);
    }
}