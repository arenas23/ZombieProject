using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimVertically : MonoBehaviour
{
    [SerializeField] Camera cameraAim;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse X") * 0.1f * Time.deltaTime;
        Vector3 position = target.position;
        position.z = target.position.z;
        position.x = target.position.x;
        position.y += mouseY; 
        target.position = position;
    }
}
