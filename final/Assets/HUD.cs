using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.uiPlayer = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
