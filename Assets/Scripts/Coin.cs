using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    InGameSceen inGameSceen;

    void Start()
    {
        inGameSceen = FindObjectOfType<InGameSceen>();

        StartCoroutine(MoveCoin());
    }

    IEnumerator MoveCoin()
    {
        yield return new WaitForSeconds(0.4f);

        transform.DOMove(new Vector3(-8.52f, 1.8f, 0),
            Random.Range(0.5f, 1f))
            .SetEase(Ease.InOutExpo)
            .OnComplete(() =>
            {
                inGameSceen.UpdateCoinAddText(1);
                Destroy(gameObject);
            });
    }

}
