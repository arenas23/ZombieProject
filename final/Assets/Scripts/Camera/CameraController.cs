using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("MoveCamera")]
    private float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] float xRotation = 0;
    private float elapsedTime;

    [Header("Aim")]
    [SerializeField] Transform aimPoint;
    [SerializeField]Transform player;
    bool isAiming = false; 
    Vector3 aimPosition = new(-0.220799997f, -0.0147000002f, 0.350699991f);
    Vector3 startPosition;
    Vector3 currentPosition;
    Quaternion initialRotation;
    Quaternion currentRotation;

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startPosition = player.localPosition;
        initialRotation = player.transform.localRotation;
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
            currentPosition = player.transform.localPosition;
            currentRotation = player.transform.localRotation;


        }

        if (isAiming)
        {
            if (player.transform.localPosition != aimPosition)
            {
                elapsedTime += Time.deltaTime;
                float perccentageComplete = elapsedTime / 0.2f;
                player.transform.localPosition = Vector3.Lerp(startPosition, aimPosition, perccentageComplete);
                player.transform.localRotation = Quaternion.Slerp(initialRotation, aimPoint.localRotation, perccentageComplete) ;
            }else {
                elapsedTime = 0;
            }
           

        }else {
            if(player.transform.localPosition != startPosition )
            {
                elapsedTime += Time.deltaTime;
                float perccentageComplete = elapsedTime / 0.3f;
                player.transform.localPosition = Vector3.Lerp(currentPosition, startPosition, perccentageComplete);
                player.transform.localRotation = Quaternion.Slerp(currentRotation, initialRotation, perccentageComplete);
            }
            else{
                elapsedTime = 0;
            }
            
        }

    }

    void CameraMove()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime ;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

        if(mouseY != 0)
        {
            xRotation -= mouseY * mouseSensitivity;

            xRotation = math.clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }

        if(mouseX != 0)
        {
            playerBody.Rotate(Vector3.up * mouseX * mouseSensitivity);
        }


   
    }


    // public void SetMouseSensitivity(float sensitivity)
    // {
    //     mouseSensitivity = sensitivity;
    // }

}
