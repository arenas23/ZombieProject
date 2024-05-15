using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAtackPrueba : MonoBehaviour
{
    [SerializeField] Transform bulletsParent;
    public List<GameObject> bulletEnemyList;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletCartridge = 20;
    private GameObject tempBullet;

    void Start()
    {
     
        BulletInstantiate();
    }

    void Update()
    {

    }

    /// <summary>
    /// Instantiates a list of bullet game objects based on the number specified in the bulletEnemyCartridge field.
    /// Each instantiated bullet is set to be inactive and added to the bulletEnemyList.
    /// </summary>
    void BulletInstantiate()
    {
        bulletEnemyList = new List<GameObject>();

        for (int i = 0; i < bulletCartridge; i++)
        {
            tempBullet = Instantiate(bulletPrefab,bulletsParent);
            tempBullet.SetActive(false);
            bulletEnemyList.Add(tempBullet);
        }
    }

    /// <summary>
    /// Retrieves the first inactive bullet from the bulletEnemyList.
    /// </summary>
    /// <returns>The first inactive bullet game object, or null if all bullets are active.</returns>
    GameObject GetBullets()
    {
        for (int i = 0; i < bulletCartridge; i++)
        {
            if (!bulletEnemyList[i].activeInHierarchy)
            {
                return bulletEnemyList[i];
            }
        }
        return null;
    }

    public void ShootAmmo()
    {
        GameObject bulletToShoot = GetBullets();
        if (bulletToShoot != null)
        {
            bulletToShoot.transform.position = bulletsParent.transform.position;
            bulletToShoot.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            bulletToShoot.SetActive(true);
            
            // AudioManager.Instance.PlaySFX(2);
        }
        else
        {
            Debug.Log("No ammo");
        }
    }
}