using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _centerEyeTransform;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _teleportAreaLayer;
    [SerializeField] private GameObject _gazeObjectPrefab;
    [SerializeField] private float _maxDistanceToDestination;

    private IClickable currentIClickable;
    /*[SerializeField] private OVRInput.Button _buttonForTeleport = OVRInput.Button.PrimaryIndexTrigger;
    [SerializeField] private OVRInput.Controller _controller = OVRInput.Controller.RTouch;*/

    private GameObject GazeObject
    {
        get
        {
            if (_gazeObject == null)
            {
                _gazeObject = Instantiate(_gazeObjectPrefab);
            }

            return _gazeObject;
        }
    }

    private GameObject _gazeObject;
    private Transform _rayOriginTransform;
    private bool _isButtonDown;


    public void SetActivity(bool isActive)
    {
        this.enabled = isActive;

        _isButtonDown = false;
        _lineRenderer.gameObject.SetActive(false);
        GazeObject.SetActive(false);
    }

    private void Awake()
    {
        _rayOriginTransform = _lineRenderer.transform;
        _lineRenderer.endWidth = 0.00001f;

#if UNITY_EDITOR
        _lineRenderer.transform.SetParent(_centerEyeTransform);
#endif
    }

    private void OnButtonDown()
    {
        _isButtonDown = true;
        _lineRenderer.gameObject.SetActive(true);
    }

    private void OnButtonUp()
    {
        _isButtonDown = false;
        _lineRenderer.gameObject.SetActive(false);
        GazeObject.SetActive(false);
        if (currentIClickable!=null)
        {
            currentIClickable.Click();
        }
    }

    public bool IsFocussed()
    {
        //OVRInputModule inputModule = EventSystem.current.currentInputModule as OVRInputModule;
        return false; //inputModule && inputModule.activeGraphicRaycaster == this;
    }


    private void FixedUpdate()
    {
        if (!_isButtonDown)
        {
            return;
        }

        if (Physics.Raycast(new Ray(_rayOriginTransform.position, _rayOriginTransform.forward), out var hit, _maxDistanceToDestination, _teleportAreaLayer))
        {
             IClickable newClickable = hit.collider.GetComponent<IClickable>();

             if (newClickable != currentIClickable)
             {
               // newClickable.OnPointerEnter();
               // currentIClickable.OnPointerExit();
                currentIClickable = newClickable;
             }
             
             GazeObject.transform.position = hit.point;
             _lineRenderer.SetPosition(1, _lineRenderer.transform.InverseTransformPoint(hit.point));
        }
        else
        {
            _lineRenderer.SetPosition(1, _lineRenderer.transform.InverseTransformPoint(Vector3.one));
            currentIClickable = null;
        }
    }

    private void Update()
    {
        if (_isButtonDown && currentIClickable!=null)
        {
            if (!GazeObject.activeInHierarchy)
            {
                GazeObject.SetActive(true);
            }
        }
        else if (GazeObject.activeInHierarchy)
        {
            GazeObject.SetActive(false);
        }

#if !UNITY_EDITOR
            /*if (_isButtonDown && OVRInput.GetUp(_buttonForTeleport, _controller))
            {
                OnButtonUp();
            }
            else if(!_isButtonDown && OVRInput.GetDown(_buttonForTeleport, _controller))
            {
                OnButtonDown();
            }*/
#else
        if (_isButtonDown && Input.GetMouseButtonUp(0))
        {
            OnButtonUp();
        }
        else if (!_isButtonDown && Input.GetMouseButtonDown(0))
        {
            OnButtonDown();
        }
#endif
    }

}
