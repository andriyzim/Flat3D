using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoysticManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static JoysticManager Instance;
    public Image backImage;
    public Image joystickImg;
    private Vector3 inputVector;
    private bool _paused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        if (_paused) return;
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backImage.rectTransform, ped.position, ped.pressEventCamera, out localPoint))
        {
            localPoint.x = (localPoint.x / backImage.rectTransform.sizeDelta.x);
            localPoint.y = (localPoint.y / backImage.rectTransform.sizeDelta.y);
            
            inputVector = new Vector3(localPoint.x, 0f, localPoint.y);
            inputVector = new Vector3(localPoint.x*2f, 0f, localPoint.y*2f);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition = 
                new Vector3(inputVector.x * (backImage.rectTransform.sizeDelta.x / 2), inputVector.z * (backImage.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped) 
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        return inputVector.x != 0 ? inputVector.x : Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        return inputVector.z != 0 ? inputVector.z :  Input.GetAxis("Vertical");
    }
}
