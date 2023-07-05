using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerViewCamera : MonoBehaviour
{

    float xMouse, yMouse;
    
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    public Transform player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        xMouse = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yMouse = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= +yMouse;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * xMouse);

    }
}
