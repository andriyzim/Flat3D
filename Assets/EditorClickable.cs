using UnityEngine;

public class EditorClickable : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);


            if (Physics.Raycast(ray, out var hitInfo, 1000))
            {

                IClickable iClickable = hitInfo.transform.GetComponent<IClickable>();

                if (iClickable!=null)
                {
                    iClickable.Click();
                }
            }
        }
    }
#endif

}
