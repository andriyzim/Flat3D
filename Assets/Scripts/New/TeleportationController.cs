/*
using Teleportation;
using UnityEngine;

namespace Scripts.Teleportation
{
    public class TeleportationController : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _centerEyeTransform;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask _teleportAreaLayer;
        [SerializeField] private GameObject _teleportDestinationPrefab;
        [SerializeField] private float _maxDistanceToDestination;
        
        
        
        [SerializeField]private ovr.Button _buttonForTeleport = OVRInput.Button.PrimaryIndexTrigger;
        [SerializeField]private OVRInput.Controller _controller = OVRInput.Controller.LTouch;
        
        private GameObject TeleportDestinationGameObject
        {
            get
            {
                if (_teleportDestinationGameObject == null)
                {
                    _teleportDestinationGameObject = Instantiate(_teleportDestinationPrefab);
                }
            
                return _teleportDestinationGameObject;
            }
        }
    
        private GameObject _teleportDestinationGameObject;
        private Transform _rayOriginTransform;
        private bool _isButtonDown;
        private DestinationInfo? _validDestinationInfo;

        private void TeleportAtPoint(Vector3 destinationPoint)
        {
            var offset = _playerTransform.position - _centerEyeTransform.position;
            offset.y = 0f;
            _playerTransform.position = destinationPoint + offset;
        }

        public void SetActivity(bool isActive)
        {
            this.enabled = isActive;
        
            _isButtonDown = false;
            _lineRenderer.gameObject.SetActive(false);
            TeleportDestinationGameObject.SetActive(false);
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
            //if (SimulatorController.CurrentState == GameState.Pause)
            //{
            //    return;
            //}
            
            _isButtonDown = true;
            _lineRenderer.gameObject.SetActive(true);
        }
    
        private void OnButtonUp()
        {
            _isButtonDown = false;
            _lineRenderer.gameObject.SetActive(false);
            TeleportDestinationGameObject.SetActive(false);
            if (_validDestinationInfo.HasValue)
            {
                var destination = _validDestinationInfo.Value;
                TeleportAtPoint(destination.Position);
                destination.SetFocusState(false);
                
                _validDestinationInfo = null;
            }
        }

        private void FixedUpdate()
        {
            if (!_isButtonDown)
            {
                return;
            }
            
            //if (SimulatorController.CurrentState == GameState.Pause)
            //{
            //    return;
            //}
            
            if(Physics.Raycast(new Ray(_rayOriginTransform.position,_rayOriginTransform.forward), out var hit, _maxDistanceToDestination, _teleportAreaLayer))
            {
                
                var teleportationPointComponent = hit.collider.GetComponent<TeleportationPoint>();
                if (teleportationPointComponent != null)
                {
                    if (!_validDestinationInfo.HasValue ||
                        !_validDestinationInfo.Value.Equals(teleportationPointComponent.Point))
                    {
                        _validDestinationInfo?.SetFocusState(false);
                        _validDestinationInfo = new DestinationInfo(teleportationPointComponent);
                    }
                }
                else if(!_validDestinationInfo.HasValue || !_validDestinationInfo.Value.Equals(hit.point))
                {
                    _validDestinationInfo  = new DestinationInfo(hit.point);
                }
                _validDestinationInfo?.SetFocusState(true);
                
            }
            else
            {
                _validDestinationInfo?.SetFocusState(false);
                _validDestinationInfo = null;
            }
        }

        private void Update()
        {
            if (_isButtonDown && _validDestinationInfo.HasValue && _validDestinationInfo.Value.IsShowTeleportPoint)
            {
                if (!TeleportDestinationGameObject.activeInHierarchy)
                {
                    TeleportDestinationGameObject.SetActive(true);
                }

                TeleportDestinationGameObject.transform.position = _validDestinationInfo.Value.Position;
            }
            else if (TeleportDestinationGameObject.activeInHierarchy)
            {
                TeleportDestinationGameObject.SetActive(false);
            }
            
#if !UNITY_EDITOR
            if (_isButtonDown && OVRInput.GetUp(_buttonForTeleport, _controller))
            {
                OnButtonUp();
            }
            else if(!_isButtonDown && OVRInput.GetDown(_buttonForTeleport, _controller))
            {
                OnButtonDown();
            }
#else
            if (_isButtonDown && Input.GetMouseButtonUp(0))
            {
                OnButtonUp();
            }
            else if(!_isButtonDown && Input.GetMouseButtonDown(0))
            {
                OnButtonDown();
            }
#endif
        }
        
        private struct DestinationInfo
        {
            public bool IsShowTeleportPoint { get; }
            public Vector3 Position => _pointPosition;

            private readonly Vector3 _pointPosition;
            private readonly TeleportationPoint _teleportationPoint;

            public DestinationInfo(Vector3 point)
            {
                _pointPosition = point;
                _teleportationPoint = null;
                IsShowTeleportPoint = true;
            }

            public DestinationInfo(TeleportationPoint point)
            {
                _teleportationPoint = point;
                _pointPosition = _teleportationPoint.Point;
                IsShowTeleportPoint = false;
            }
            
            public void SetFocusState(bool isFocused)
            {
                if (_teleportationPoint == null)
                {
                    return;
                }

                if (isFocused)
                {
                    _teleportationPoint.OnGetFocus();
                }
                else
                {
                    _teleportationPoint.OnLostFocus();
                }
            }

            public bool Equals(Vector3 point)
            {
                return _pointPosition == point;
            }
        }
    }
}
*/
