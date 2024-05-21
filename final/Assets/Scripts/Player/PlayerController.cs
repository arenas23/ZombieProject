using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed;
    CharacterController characterController;

    [Tooltip("Sensitivity multiplier for moving the camera around")]
    public float LookSensitivity = 1f;

    [Header("Rotation")]
    [Tooltip("Rotation speed for moving the camera")]
    public float RotationSpeed = 200f;

    [Range(0.1f, 1f)]
    [Tooltip("Rotation speed multiplier when aiming")]
    public float AimingRotationMultiplier = 0.4f;

    [Header("References")]
    [Tooltip("Reference to the main camera used for the player")]
    public Camera PlayerCamera;

    public bool IsAiming { get; private set; }

    [Header("Misc")]
    [Tooltip("Speed at which the aiming animatoin is played")]
    public float AimingAnimationSpeed = 10f;

    [SerializeField] Transform aimPoint;
    [SerializeField] Transform defaultPosition;
    [SerializeField] Transform weapon;
    public WeaponSway weaponSway;

    float m_CameraVerticalAngle = 0f;

    public float RotationMultiplier
    {
        get
        {
            // if (m_WeaponsManager.IsAiming)
            // {
            //     return AimingRotationMultiplier;
            // }

            return 1f;
        }
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(1)) 
        {
            IsAiming = true;
            weaponSway.smoothAmount = 0;
            weaponSway.rotationZ = false;
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            IsAiming = false;
            weaponSway.smoothAmount = 1.3f;
            weaponSway.rotationZ = true;
            // currentPosition = weapon.localPosition;
            // currentRotation = weapon.localRotation;
        }

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        characterController.Move(movementSpeed * Time.deltaTime * move);

        HandleCharacterMovement();


    }

    void LateUpdate()
    {

        if (IsAiming) 
        {
            weapon.localPosition = Vector3.Lerp(weapon.localPosition, aimPoint.localPosition, AimingAnimationSpeed * Time.deltaTime);   
            // weapon.localRotation = Quaternion.Slerp(initialRotation, aimPoint.localRotation, AimingAnimationSpeed * Time.deltaTime);
        }
        else 
        {

            weapon.localPosition = Vector3.Lerp(weapon.localPosition, defaultPosition.localPosition , AimingAnimationSpeed * Time.deltaTime);
            // weapon.localRotation = Quaternion.Slerp(currentRotation, initialRotation, AimingAnimationSpeed * Time.deltaTime);
        }
    }


    private void HandleCharacterMovement()
    {
        // horizontal character rotation
        {
            // rotate the transform with the input speed around its local Y axis
            transform.Rotate( new Vector3(0f, GetLookInputsMouse("Mouse X") * RotationSpeed * RotationMultiplier,
                    0f), Space.Self);
        }

        // vertical camera rotation
        {
            // add vertical inputs to the camera's vertical angle
            m_CameraVerticalAngle += GetLookInputsMouse("Mouse Y") * RotationSpeed * RotationMultiplier;

            // limit the camera's vertical angle to min/max
            m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -89f, 89f);

            // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
            PlayerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);
        }
    }

    private float GetLookInputsMouse(string inputMouse)
    {
        // Check if this look input is coming from the mouse

        float i = Input.GetAxisRaw(inputMouse);

        // handle inverting vertical input
        if(inputMouse == "Mouse Y")
        i *= -1f;

        // apply sensitivity multiplier
        i *= LookSensitivity;

        // reduce mouse input amount to be equivalent to stick movement
        i *= 0.01f;
        return i;
    }



}