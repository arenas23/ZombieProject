using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        transform.LookAt(camera.transform.position);
    }
}
