using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public int coin;

    private Player player;
    private Vector3 bgOldPos;
    public GameObject bg;


    public void Start()
    {
        bgOldPos = bg.transform.localPosition;

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);

        Run();
    }

    public void Run()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            player.RunPlayer();
        }

        bg.transform.DOLocalMoveX(-0.95f, 5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                bg.transform.localPosition = bgOldPos;

                player.am.SetBool("isRun", false);

                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

                if (spawnManager != null)
                {
                    spawnManager.StartSpawn();
                }
            });
    }

}
