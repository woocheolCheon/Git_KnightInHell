using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    private Vector3 playerOldPos;

    public float maxHealth;
    public float currentHealth;

    private Coroutine findEnemyCoroutine;

    public Animator am;

    public GameObject bg;


    public float UpgradeDamage;
    public float UpgradeHealth;
    public float UpgradeAttackSpeed;
    public float UpgradeCritical;

    //체력바
    public HpBar hpBar;

    private void Start()
    {
        currentHealth = maxHealth;

        UpdateHp();

        playerOldPos = transform.position;

        am = GetComponent<Animator>();
    }

    public void RunPlayer()
    {
        if (findEnemyCoroutine != null)
        {
            StopCoroutine(findEnemyCoroutine);

            findEnemyCoroutine = null;
        }

        if(transform.position != playerOldPos)
        {
            am.SetBool("isRun", false);
        }
        else
        {
            am.SetBool("isRun", true);

            SoundManager.instance.Play("Run");
        }
        

        findEnemyCoroutine = StartCoroutine(FindEnemy());
    }


    IEnumerator FindEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            GameObject closetEnemy = null;

            float maxDistance = 100f;

            for (int i = 0; i < enemies.Length; i++)
            {
                // 플레이어와 적의 거리
                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);

                if (distance < maxDistance)
                {
                    closetEnemy = enemies[i];
                    // 처음 적을 넣어가며 비교..
                    maxDistance = distance;
                }
            }

            //가까운 적을 찾음
            if (closetEnemy != null)
            {
                am.SetBool("isRun", true);

                transform.DOMove(closetEnemy.transform.position - new Vector3(0.2f, 0, 0), 0.4f)
                    .OnComplete(() =>
                    {
                        am.SetBool("EndTrigger", false);
                        am.SetTrigger("doAttack");
                    });

                yield break;
            }
        }
    }


    public void ResetPosition()
    {
        am.SetTrigger("doJump");

        transform.DOJump(playerOldPos, 0.2f, 1, 0.3f)
            .OnComplete(() =>
            {
                am.SetBool("isRun", false);
            });
    }

    public void GetDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;

        UpdateHp();


        //이펙트 효과

        StartCoroutine(DamageEffect());

        SoundManager.instance.Play("PlayerHit");


        if (currentHealth <= 0)
        {

        }
    }

    IEnumerator DamageEffect()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void UpdateHp()
    {
        if (hpBar != null)
        {
            hpBar.UpdateHPBar(currentHealth, maxHealth);
        }
    }

}
