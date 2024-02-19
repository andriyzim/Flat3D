using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    private float speed=15f;
    private float gravity = -9.8f;
    private Vector3 velosity;
    [SerializeField]private Camera cameraMain;
    void Start()
    {
        if (cameraMain==null)cameraMain = Camera.main;
        velosity = new Vector3(0,gravity,0);
    }

    // Update is called once per frame
    void Update()
    {

        float y= JoysticManager.Instance.Vertical() * speed * Time.deltaTime;
        Vector3 move = transform.forward * y;
        _characterController.Move(move * Time.deltaTime * speed);
        _characterController.Move(velosity * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
               // Debug.Log(hit.transform.name);
                IInteractive ii = hit.transform.GetComponent<IInteractive>();

                if (ii!=null) ii.Interact();
                IClickable i = hit.transform.GetComponent<IClickable>();

                if (i != null) i.Click();
            }
        }

    }
}
