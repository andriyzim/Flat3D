using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OpenDoorsSlide : MonoBehaviour, IClickable
{
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private bool _isOpen;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private GameObject[] _insideGameObjects;
    [SerializeField] private float _duration = 0.5f;
    private AudioSource _audioSource;
    private Vector3 _closePosition, _openPosition;
    private Transform _transform;


    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("UI");
        if (_insideGameObjects != null)
        {
            foreach (var gObject in _insideGameObjects) gObject.SetActive(_isOpen);
        }

        _transform = transform;
        if (_audioClip != null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = _audioClip;
        }

        if (_isOpen)
        {
            _openPosition = _transform.localPosition;
            _closePosition = _openPosition;
            _closePosition -= _moveDirection;
        }
        else
        {
            _closePosition = _transform.localPosition;
            _openPosition = _closePosition;
            _openPosition += _moveDirection;
        }

    }

    public void Click()
    {
        _isOpen = !_isOpen;

        if (_insideGameObjects != null)
        {
            foreach (var gObject in _insideGameObjects) gObject.SetActive(_isOpen);
        }

        Vector3 targetPos = _isOpen ? _openPosition : _closePosition;
        _transform.DOLocalMove(targetPos, _duration);

        if (_audioClip)
        {
            _audioSource.Play();
        }

    }
}

