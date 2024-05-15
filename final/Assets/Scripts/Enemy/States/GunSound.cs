using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunSound : MonoBehaviour
{

    private AudioSource audioSource; //Audio
    [SerializeField] private AudioClip colectar1; //Audio

    //[SerializeField] private AudioClip colectar2; //Audio

    // Start is called before the first frame update
    void Awake()
    {
        audioSource =  GetComponent<AudioSource>();
        audioSource.PlayOneShot(colectar1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
