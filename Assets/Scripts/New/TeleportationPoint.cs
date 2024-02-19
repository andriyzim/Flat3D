using System;
using UnityEngine;

namespace Teleportation
{
    public class TeleportationPoint : MonoBehaviour
    {
        [SerializeField] private Transform _targetPoint;

        public Vector3 Point => _targetPoint.position;

        private readonly int _fadeStartParam = Shader.PropertyToID("_FadeStart");

        private const float FADE_START = 0.1f;
        private const float FADE_END = 0.6f;

        private MeshRenderer _meshRenderer;
        private bool _isFocused;
        private float _currentFadeFocus;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _currentFadeFocus = _meshRenderer.material.GetFloat(_fadeStartParam);
        }

        public void OnGetFocus()
        {
            _isFocused = true;
        }

        public void OnLostFocus()
        {
            _isFocused = false;
        }

        private void Update()
        {
            if (_isFocused && _currentFadeFocus < FADE_END)
            {
                _currentFadeFocus += Time.fixedDeltaTime;
                _meshRenderer.material.SetFloat(_fadeStartParam, _currentFadeFocus);
            }
            else if (!_isFocused && _currentFadeFocus > FADE_START)
            {
                _currentFadeFocus -= Time.fixedDeltaTime;
                _meshRenderer.material.SetFloat(_fadeStartParam, _currentFadeFocus);
            }
        }
    }
}
