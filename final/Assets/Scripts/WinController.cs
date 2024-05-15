using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{

    Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.winCanvas = GetComponent<Canvas>();
        gameObject.SetActive(false);
        
        buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            if (button.name == "ResetButton")
            {

                button.onClick.AddListener(GameManager.Instance.ResetGame);
            }
            else
            {
                button.onClick.AddListener(GameManager.Instance.ExitGame);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
