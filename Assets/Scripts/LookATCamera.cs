using UnityEngine;
//using Valve.VR.InteractionSystem; // todo uncomment for using vr
//[ExecuteInEditMode]
public class LookATCamera : MonoBehaviour
{
    [SerializeField]private Transform _targetObject;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        if (_targetObject == null)
        {
           // if (Player.instance != null) _targetObject = Player.instance.hmdTransforms[0];
            //else
            _targetObject = Camera.main.transform;
        }
    }


    private void Update()
    {
        if (_targetObject!=null)
        {
            Vector3 forw = transform.position - _targetObject.position;
            forw.y = 0;
            transform.forward = forw;

        }
    }
}
