using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemRotation : MonoBehaviour
{
    Vector3 rotationVector;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
    }

    void LookAtCamera()
    {
        Vector3 lookDirection = Camera.main.transform.position - transform.position;

        // Mantener solo la rotación en el eje Y para que el objeto mire hacia la cámara horizontalmente
        lookDirection.y = 0f;
        // Debug.Log(lookDirection);

        // Rotar este objeto para que mire hacia la cámara
        transform.rotation = Quaternion.LookRotation(-lookDirection);
    }
}
