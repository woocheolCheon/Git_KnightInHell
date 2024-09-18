using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if(enemy != null)
            {
                float totalDamage = damage + player.UpgradeDamage;

                if(Random.Range(0,1f) < player.UpgradeCritical)
                {
                    totalDamage = totalDamage * 3f;
                }

                enemy.GetDamage(totalDamage);
            }
        }
    }
}
