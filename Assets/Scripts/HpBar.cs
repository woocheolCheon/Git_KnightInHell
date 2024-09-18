using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    //체력바 게이지
    public Image foreground;

    public void UpdateHPBar(float currentHealth, float maxHealth)
    {
        if (foreground != null)
        {
            //현재 체력을 최대 체력으로 나누어 체력 비율 계산
            //Mathf.Clamp01은 0~1 사이의 값으로 제한하도록 하는 명령어
            float hpPercentage = Mathf.Clamp01(currentHealth / maxHealth);

            foreground.fillAmount = hpPercentage;
        }
    }
}
