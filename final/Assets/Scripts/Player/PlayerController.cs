using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Rigidbody rigidbody;
    public float movementSpeed;
    public Vector2 sensitivity;
    public Transform cameraPlayer;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody> ();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        UpdateMovement();
        UpdateMouseLook();
    }

    private void UpdateMovement()
    {
        Vector3 velocity = Vector3.zero;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 direction = (transform.forward * vertical + transform.right * horizontal).normalized;
            rigidbody.velocity = direction * movementSpeed;
        }
        else
        {
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
    }

    private void UpdateMouseLook()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        if(horizontal != 0)
        {
            transform.Rotate(0,horizontal * sensitivity.x,0);
        }
        if(vertical != 0){
            cameraPlayer.Rotate(-vertical * sensitivity.y,0,0);
        }
    }
}