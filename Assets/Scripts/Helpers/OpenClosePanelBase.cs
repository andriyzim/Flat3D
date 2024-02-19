using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

// slides panel to sides. for init state needs to bee in "showed" position
[RequireComponent(typeof(CanvasGroup))]
public class OpenClosePanelBase : MonoBehaviour
{
    private RectTransform _rectTransform; // animated
    protected CanvasGroup _canvasGroup;

    private static float _showHideDuration = 0.5f;
    private static Ease _ease = Ease.OutBack;
    private static Ease _outEase = Ease.InBack;

    private Vector2 _visiblePosition;
    private Vector2 _hidenPosition;

    public bool IsActive { get; private set; }
    [SerializeField]private bool _leftSided;
    private bool _inited;


    public void Init()
    {
       // Debug.Log($"[{name}] init ");
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        _visiblePosition = _rectTransform.anchoredPosition;
        if (!_leftSided)
        {
            _hidenPosition = new Vector2(_visiblePosition.x - _rectTransform.rect.width, _visiblePosition.y);
        }
        else
        {
            _hidenPosition = new Vector2(_visiblePosition.x + _rectTransform.rect.width, _visiblePosition.y);
        }

        _inited = true;
    }

    public virtual void SetActiveImmediate(bool active) // immediately shows hides panel
    {
        if (!_inited) Init();
        DOTween.Kill(_rectTransform);
        _rectTransform.SetAsFirstSibling();
        _rectTransform.anchoredPosition = active ? _visiblePosition : _hidenPosition;
        SetActiveCanvas(active);
        IsActive = active;
    }

    public virtual void SetActive(bool active, UnityAction callback = null)
    {
        if (!_inited) Init();
        DOTween.Kill(_rectTransform);
        IsActive = active;
        if (active)
        {
            Show(_visiblePosition, _rectTransform, callback);
        }
        else
        {
            Hide(_hidenPosition, _rectTransform, callback);
        }
    }

    protected virtual void Hide(Vector2 hidenPosition, RectTransform rectTransform,
            UnityAction callback = null) //  shows /hides panel using animation
    {
        rectTransform.DOAnchorPos(hidenPosition, _showHideDuration)
            .SetId(_rectTransform)
            .SetEase(_outEase)
            .OnComplete(() =>
            {
                SetActiveCanvas(false);
                callback?.Invoke();
            });
    }

    protected virtual void Show(Vector2 visiblePosition, RectTransform rectTransform, UnityAction callback = null)
    {
        SetActiveCanvas(true);
        rectTransform.DOAnchorPos(visiblePosition, _showHideDuration)
            .SetEase(_ease)
            .SetId(_rectTransform)
            .OnComplete(() =>
            {
                callback?.Invoke();
                rectTransform.SetAsLastSibling();
            });
    }

    protected void SetActiveCanvas(bool active)
    {
        _canvasGroup.alpha = active ? 1 : 0;
        _canvasGroup.blocksRaycasts = active;
        _canvasGroup.interactable = active;
    }

}
