using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerBody;
    float xRot = 0;

    [SerializeField] float mouseSensivity = 100;

    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseDeltaX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
        float mouseDeltaY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;

        xRot -= mouseDeltaY;
        xRot = Mathf.Clamp(xRot, -60, 60);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        playerBody.Rotate(0, mouseDeltaX, 0);
    }
}
