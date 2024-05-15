using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaMenu : MonoBehaviour
{
    Canvas pausa;
    // Start is called before the first frame update
    void Start()
    {
        pausa = GetComponent<Canvas>();
        GameManager.Instance.pauseCanvas = pausa;
        pausa.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
