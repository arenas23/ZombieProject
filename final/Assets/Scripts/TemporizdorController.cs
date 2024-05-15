using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemporizdorController : MonoBehaviour
{
    float seconds = 0;
    float minutes = 0;
    int minutesInSeconds = 420;
    // int minutesInSeconds = 5;
    readonly int interval = 1;
    TextMeshProUGUI tiempo;
    // Start is called before the first frame update
    void Start()
    {
        tiempo = GameObject.Find("TextTiempo").GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountBackSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountBackSeconds()
    {
        yield return new WaitForSeconds(interval);
        minutesInSeconds -= interval;

        minutes = minutesInSeconds / 60;
        seconds = minutesInSeconds % 60;

        tiempo.text = "Time: " + minutes.ToString("00") + " : " + seconds.ToString("00");

        if (minutesInSeconds == 0){
            GameManager.Instance.LosePlayer();
            yield return null;
        }
            
        else
            StartCoroutine(CountBackSeconds());

    }
}
