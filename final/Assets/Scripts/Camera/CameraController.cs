using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseSensitivity = 120f;
    [SerializeField] Transform playerBody;
    [SerializeField] float xRotation = 0;
    [SerializeField] Transform hip;
    private float elapsedTime;

    bool isAiming = false; 
    Vector3 aimPosition = new Vector3(0.219128042f, 0.00321622379f, -0.407871693f);
    Vector3 startPosition = new Vector3(0.0970000327f, 0.0286163092f, -0.531724453f);
    Vector3 currentPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        CameraMove();

        if (Input.GetMouseButtonDown(1)) // Comienza el movimiento al presionar el botón derecho
        {
            isAiming = true;
        }
        else if (Input.GetMouseButtonUp(1)) // Detiene el movimiento al soltar el botón derecho
        {
            isAiming = false;
            elapsedTime = 0;
            currentPosition = transform.localPosition;

        }


        if (isAiming)
        {
            if (transform.localPosition != aimPosition)
            {
                elapsedTime += Time.deltaTime;
                float perccentageComplete = elapsedTime / 0.5f;
                transform.localPosition = Vector3.Lerp(startPosition, aimPosition, perccentageComplete);
            }else {
                elapsedTime = 0;
            }
           

        }else {
            if(transform.localPosition != startPosition )
            {
                elapsedTime += Time.deltaTime;
                float perccentageComplete = elapsedTime / 0.3f;
                transform.localPosition = Vector3.Lerp(currentPosition, startPosition, perccentageComplete);
            }else{
                elapsedTime = 0;
            }
            
        }

    }

    void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = math.clamp(xRotation, -90f, 90f);

        // transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
         hip.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }


    // public void SetMouseSensitivity(float sensitivity)
    // {
    //     mouseSensitivity = sensitivity;
    // }

}
