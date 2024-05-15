using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 80f;
    [SerializeField] Transform playerBody;
    [SerializeField] GameObject player;
    [SerializeField] float xRotation = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        playerBody = player.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        CameraMove();
    }

    void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 30f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }


    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }

}
