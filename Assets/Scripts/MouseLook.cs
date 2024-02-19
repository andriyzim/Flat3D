using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public  class MouseLook : MonoBehaviour
//{
//    public Transform playerBody;
//    private Transform _camera;
//    private float mouseSensitivity = 100f;
//    private float xRotation=0;

////    void Start()
////    {
////      //  Cursor.lockState = CursorLockMode.Locked;
////        _camera = transform;
////    }

////    // Update is called once per frame
////    void Update()
////    {
////#if UNITY_EDITOR
////        if (Input.GetMouseButton(0))
////        {
////            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
////            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

////            xRotation = -mouseY;
////            float x = _camera.localEulerAngles.x;
////            x += xRotation;
////            // x= Mathf.Clamp(x, -90, 90f);
////            _camera.localEulerAngles = new Vector3(x, 0, 0);

////            playerBody.Rotate(Vector3.up * mouseX);
////        }
////#else

////        float mouseX =JoysticManager.Instance.Horizontal() * mouseSensitivity * Time.deltaTime;
////        //float mouseY = JoysticManager.Instance.Vertical() * mouseSensitivity * Time.deltaTime;

////        xRotation = -0;
////        float x = _camera.localEulerAngles.x;
////        x += xRotation;
////        // x= Mathf.Clamp(x, -90, 90f);
////        _camera.localEulerAngles = new Vector3(x, 0, 0);

////        playerBody.Rotate(Vector3.up * mouseX);


////#endif

//    }
//}
