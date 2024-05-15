using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPackage : MonoBehaviour
{
    public float heal = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            // AudioManager.Instance.PlaySFX(10);
            PlayerController player = other.GetComponent<PlayerController>();
            // player.Heal(heal);
            Destroy(gameObject);
        }
    }

}
