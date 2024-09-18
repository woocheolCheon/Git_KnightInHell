using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public enum Type
    {
        Melee,
        Range,
        Boss
    }

    public Type enemyType;

    public float maxHealth;
    public float currentHealth;

    public float damage;

    public float attackDistance;

    private Player player;

    private bool isAttack;

    private Animator am;

    public GameObject enemyBullet;

    private PoolManager poolManager;

    public GameObject coin;

    public HpBar hpBar;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHp();

        player = FindObjectOfType<Player>();
        am = GetComponent<Animator>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= attackDistance)
        {
            if(isAttack == false)
            {
                isAttack = true;

                am.SetBool("isAttack", true);

                if(enemyType == Type.Range)
                {
                    StartCoroutine(CreateBullet());
                }
            }
        }else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.03f * Time.deltaTime);
        }
    }

    IEnumerator CreateBullet()
    {
        while(true)
        {
            Instantiate(enemyBullet, transform.position, transform.rotation);

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            player.GetDamage(damage);
        }
    }


    public void GetDamage(float playerDamage)
    {
        currentHealth -= playerDamage;

        UpdateHp();

        SoundManager.instance.Play("Sword");

        //이펙트 효과

        StartCoroutine(DamageEffect());

        //텍스트
        GameObject damageTextObject = poolManager.GetObject();

        Vector3 newPos = damageTextObject.transform.position = transform.position;

        damageTextObject.transform.DOMove(
            new Vector3(
            Random.Range(newPos.x - 0.12f, newPos.x + 0.12f),
            newPos.y + 0.16f, 0)
            , 0.3f);

        DamageText damageText = damageTextObject.GetComponent<DamageText>();
        damageText.TextValue(playerDamage);

        if (currentHealth <= 0)
        {
            SpreadCoin();
            StartCoroutine(Dead());
        }
    }

    IEnumerator DamageEffect()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void SpreadCoin()
    {
        SoundManager.instance.Play("Coin");

        for (int i = 0; i< 10; i++)
        {
            Vector3 newPos = new Vector3(
                Random.Range(transform.position.x - 0.15f, transform.position.x + 0.15f),
                Random.Range(transform.position.y - 0.15f, transform.position.y + 0.15f),
                0
                );

            GameObject coinObejct = Instantiate(coin, transform.position, transform.rotation);

            coinObejct.transform.DOMove(newPos, 0.3f);
        }
    }

    IEnumerator Dead()
    {
        am.SetBool("isDead", true);

        yield return new WaitForSeconds(0.3f);

        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            player.am.SetBool("EndTrigger", true);

            player.RunPlayer();

        }

        Destroy(gameObject);
    }

    public void UpdateHp()
    {
        if (hpBar != null)
        {
            hpBar.UpdateHPBar(currentHealth, maxHealth);
        }
    }
}
