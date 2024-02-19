using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataPoint : MonoBehaviour, IClickable
{
    private Transform _transform;
    private Vector3 _target;
    private float _duration=0.5f;
    public static UnityAction<string> OnClickAction;

    [SerializeField]private Transform _InfoPanel;
    [SerializeField] private Text _text;

    public string Name { get; private set; }
    public int Num { get; private set; }

    private void Start()
    {
        if (_transform == null) _transform = transform;
        _InfoPanel.gameObject.SetActive(false);
    }

    public void SetName(string setName)
    {
        Name = setName;
        _text.text = setName;
    }

    public void SetNum(int setNum)
    {
        Num = setNum;
    }

    public void SetNewPosition(Vector3 newPosition)
    {
        if (_transform == null) _transform = transform;
        DOTween.Kill(_transform);
        _transform.DOLocalMove(newPosition, _duration).SetId(_transform).SetEase(Ease.InSine);
    }

    public void SetNewPositionOnStart(Vector3 newPosition)
    {
        if (_transform == null) _transform = transform;
        _target = newPosition;
        Invoke(nameof(StartAnim), Random.Range(.5f,1f));
    }

    private void StartAnim()
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 range = new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));

        sequence.Append(_transform.DOLocalMove((Vector3.one*5f)+range, Random.Range(1.5f, 2f)));
        sequence.Append(_transform.DOLocalMove(_target, _duration).SetEase(Ease.OutBack));
        sequence.SetId(_transform);
        sequence.Play();
    }


    //public void OnPointerEnter()
    //{
    //    _InfoPanel.gameObject.SetActive(true);
    //}

    //public void OnPointerExit()
    //{
    //    _InfoPanel.gameObject.SetActive(false);
    //}

    public void Click()
    {
        OnClickAction?.Invoke(Name);
    }
}
