using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Camera cameraPlayer;
    [SerializeField] PlayerController playerController;
    [SerializeField] float range = 100f;
    public float damage = 10f;

    // public GameObject bullet;
    public Transform shootPosition;
    public GameObject impactEffect;

    [Header("Gun Attributes")]
    [SerializeField] float roundsPerMinute = 750f;
    [SerializeField] float timer = 0f;
    [SerializeField] int bulletsOnMagazine = 30;
    public int ammunitionReserve = 120;
    float timeBetweenShots;

    void Start()
    {
        timeBetweenShots = 60f / roundsPerMinute;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenShots && bulletsOnMagazine > 0)
        {
            Shoot();
            bulletsOnMagazine -= 1;
            timer = 0f;
        }
   
    }

    void Shoot()
    {
        if (Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out RaycastHit hit, range))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // GameObject shoot = Instantiate(bullet, shootPosition.transform.position, shootPosition.rotation);
            // Calcula la dirección desde el punto de disparo hasta el punto de impacto
            // Vector3 direction = (hit.point - shootPosition.position).normalized;
            // shoot.GetComponent<Rigidbody>().AddForce(direction *1500f, ForceMode.Force);

        }
    }

    void Reload()
    {
        int missingBullets = 30 - bulletsOnMagazine; ;
        if(ammunitionReserve < missingBullets)
        {
            bulletsOnMagazine += ammunitionReserve;
            ammunitionReserve = 0; 

        }else 
        {
            ammunitionReserve -= missingBullets;
            bulletsOnMagazine += missingBullets;
        }
      
    }

    void SetIsReloading()
    {
        playerController.isReloading = false;
    }
}
