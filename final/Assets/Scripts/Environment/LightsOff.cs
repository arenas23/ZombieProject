using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOff : MonoBehaviour
{
    public Material emissiveMaterial;
    public Material basicMaterial;
    private MeshRenderer meshRenderer;
    bool lightsOn = false;
    public LightsOff_Area lightsOff_Area;
    private float timer = 0f;
    private float timeOut = 2f;
    private float flickerTimer = 0f;
    private float flickerTimeOut = 0.1f;
    private bool isFlickering = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= timeOut)
        {
            isFlickering = Random.Range(0, 2) == 0;
            timer = 0;
            if (isFlickering) timeOut = Random.Range(0.1f, 0.3f);
            else timeOut = Random.Range(0.5f, 2f);
        }
        else
        {
            if (isFlickering)
            {
                if (flickerTimer >= flickerTimeOut)
                {
                    toggleLights();
                    lightsOff_Area.toggleLights();
                    flickerTimer = 0f;
                    flickerTimeOut = Random.Range(0.01f, 0.05f);
                }
                else
                {
                    flickerTimer += Time.deltaTime;
                }
            }
            timer += Time.deltaTime;
        }

    }

    private void toggleLights()
    {
        if (lightsOn) meshRenderer.material = emissiveMaterial;
        else meshRenderer.material = basicMaterial;
        lightsOn = !lightsOn;
    }


}
