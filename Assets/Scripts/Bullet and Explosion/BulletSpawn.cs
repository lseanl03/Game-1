using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform bulletPos;
    public float bulletSpeed = 80f;
    public void Bullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.parent = bulletPos;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (playerController.isFacingRight)
        {
            bulletRb.velocity = new Vector2(bulletSpeed, 0f);
        }
        else
        {
            bulletRb.velocity = new Vector2(-bulletSpeed, 0f);
        }
    }
}
