using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public float bulletSpeed = 80f;

    public PlayerController playerController;
    public GameObject bullet;
    public Transform firePoint;
    public Transform bulletPrefabPos;
    public void Bullet()
    {
        GameObject bulletPrefab = Instantiate(bullet, firePoint.position, Quaternion.identity);
        bulletPrefab.transform.parent = bulletPrefabPos;

        Rigidbody2D bulletRb = bulletPrefab.GetComponent<Rigidbody2D>();

        if (playerController.isFacingRight)
        {
            bulletRb.MovePosition(bulletRb.position + new Vector2(bulletSpeed * Time.deltaTime, 0f));
        }
        else
        {
            bulletRb.MovePosition(bulletRb.position - new Vector2(bulletSpeed * Time.deltaTime, 0f));
        }
        Debug.Log(bulletRb);
    }

}
