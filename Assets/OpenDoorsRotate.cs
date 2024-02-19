using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class OpenDoorsRotate : MonoBehaviour, IClickable
{
    [SerializeField] private Vector3 _rotateAngle;
    [SerializeField] private bool _isOpen;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private GameObject[] _insideGameObjects;
    [SerializeField] private float _duration = 0.5f;
    private AudioSource _audioSource;
    private Vector3 _closeRotation, _openRotation;
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
           _audioSource =gameObject.AddComponent<AudioSource>();
           _audioSource.clip = _audioClip;
        }

        if (_isOpen)
        {
            _openRotation = _transform.localEulerAngles;
            _closeRotation -= _rotateAngle;
        }
        else
        {
            _closeRotation = _transform.localEulerAngles;
            _openRotation += _rotateAngle;
        }

    }

    public void Click()
    {
        _isOpen = !_isOpen;

        if (_insideGameObjects != null)
        { 
             foreach (var gObject in _insideGameObjects) gObject.SetActive(_isOpen);
        }

        Vector3 targetRotation = _isOpen ? _openRotation : _closeRotation;
        _transform.DOLocalRotate(targetRotation, _duration);

        if (_audioClip)
        {
            _audioSource.Play();
        }

    }
}
