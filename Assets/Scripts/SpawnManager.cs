using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemyPrefabs;
        public int enemyCounts;
    }

    public Wave[] waves;

    private int currentWaveIndex = 0;

    public Transform[] enemySpawnPoints;

    private Coroutine CheckRemainEnemyCoroutine;

    private InGameSceen ingameScreen;

    private void Start()
    {
        ingameScreen = FindObjectOfType<InGameSceen>();

        ingameScreen.UpdateWaveText("Welcome to Knight in Hell");
    }


    public void StartSpawn()
    {
        StopSpawn();

        Wave currentWave = waves[currentWaveIndex];

        for (var i = 0; i < currentWave.enemyCounts; i++)
        {
            GameObject enemy = currentWave.enemyPrefabs[Random.Range(0, currentWave.enemyPrefabs.Length)];

            Instantiate(enemy, enemySpawnPoints[i].position,
                Quaternion.identity);
        }

        CheckRemainEnemyCoroutine = StartCoroutine(CheckRemainEnemy());


        ingameScreen.UpdateWaveText("Wave " + (currentWaveIndex +1).ToString());
    }

    private void StopSpawn()
    {
        if (CheckRemainEnemyCoroutine != null)
        {
            StopCoroutine(CheckRemainEnemyCoroutine);
            CheckRemainEnemyCoroutine = null;
        }
    }


    IEnumerator CheckRemainEnemy()
    {
        while (true)
        {
            GameObject[] remainEnemy = GameObject.FindGameObjectsWithTag("Enemy");

            if (remainEnemy.Length == 0)
            {
                currentWaveIndex++;

                Player player = FindObjectOfType<Player>();

                if(player != null)
                {
                    player.ResetPosition();
                }

                if(currentWaveIndex == waves.Length)
                {
                    StopSpawn();
                    Debug.Log("웨이브 완료");

                    ingameScreen.UpdateWaveText("Waves cleared");
                }
                else
                {
                    GameManager gameManager = FindObjectOfType<GameManager>();

                    if (gameManager != null)
                    {
                        gameManager.Start();
                    }

                }

                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

}
