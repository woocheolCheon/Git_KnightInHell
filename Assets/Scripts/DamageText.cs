using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    private PoolManager damageTextPool;

    private void Start()
    {
        damageTextPool = FindObjectOfType<PoolManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnDamageText());
    }

    IEnumerator ReturnDamageText()
    {
        yield return new WaitForSeconds(0.7f);
        damageTextPool.ReturnObject(gameObject);
    }

    public void TextValue(float playerDamage)
    {
        TextMeshPro damageText = GetComponent<TextMeshPro>();
        damageText.text = playerDamage.ToString();   
    }

}
