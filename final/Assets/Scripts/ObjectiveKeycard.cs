using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveKeycard : MonoBehaviour
{
    bool isFirstTime = true;
    Objetivos objetivos;
    // Start is called before the first frame update
    void Start()
    {
        objetivos = GameObject.Find("UIObjectives").GetComponent<Objetivos>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isFirstTime && objetivos.currentObjective == (int)Objetivos.ObjectivesEnum.GO_CONTROL_ROOM)
        {
            objetivos.ActivateObjective(Objetivos.ObjectivesEnum.SEARCH_KEYCARD);
            isFirstTime = false;
        }
    }
}
