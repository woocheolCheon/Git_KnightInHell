using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameSceen : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI waveText;

    GameManager gameManager;
    Player player;

    
    public Image[] UpgradeIcons;

    public TextMeshProUGUI[] UpgradeTitleTexts;

    public TextMeshProUGUI[] UpgradeValueTexts;

    public TextMeshProUGUI[] UpgradePriceTexts;

    public Button[] UpgradeButtons;


    //Json으로 변경 추천
    public int[] UpgradePriceData;

    public string[] UpgradeTitleData;

    public string[] UpgradeIconData;

    public float[] UpgradeValueData;



    private void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        InitUpgradeUI();
        UpdateCoinAddText(0);

    }

    public void ButtonClick(string type)
    {
        switch(type)
        {
            case "UpgradeDamage":

                UpdateCoinDecreaseText(UpgradePriceData[0]);
                player.UpgradeDamage += UpgradeValueData[0];

            break;
            case "UpgradeHp":

                UpdateCoinDecreaseText(UpgradePriceData[1]);
                player.UpgradeHealth += UpgradeValueData[1];

                break;
            case "UpgradeAttackSpeed":

                UpdateCoinDecreaseText(UpgradePriceData[2]);
                player.UpgradeAttackSpeed += UpgradeValueData[2];

                break;
            case "UpgradeCritical":

                UpdateCoinDecreaseText(UpgradePriceData[3]);
                player.UpgradeCritical += UpgradeValueData[3];

                break;
        }
    }

    public void UpdateCoinDecreaseText(int coinValue)
    {
        gameManager.coin = gameManager.coin - coinValue;
        coinText.text = gameManager.coin.ToString();
        UpdateButtonStatus();
    }

    public void UpdateCoinAddText(int coinValue)
    {
        gameManager.coin = gameManager.coin + coinValue;
        coinText.text = gameManager.coin.ToString();
        UpdateButtonStatus();
    }

    public void UpdateWaveText(string textValue)
    {
        waveText.text = textValue;
    }

    private void InitUpgradeUI()
    {
        for(int i = 0; i < UpgradeIcons.Length; i++)
        {
            Sprite loadSprite = Resources.Load<Sprite>(UpgradeIconData[i]);

            if(loadSprite != null)
            {
                UpgradeIcons[i].sprite = loadSprite;
            }

            UpgradeTitleTexts[i].text = UpgradeTitleData[i];

            UpgradeValueTexts[i].text = "+" + UpgradeValueData[i].ToString();

            UpgradePriceTexts[i].text = UpgradePriceData[i].ToString();
        }
    }

    private void UpdateButtonStatus()
    {
        for(int i = 0; i < UpgradeButtons.Length; i++)
        {
            if(UpgradePriceData[i] <= gameManager.coin)
            {
                UpgradeButtons[i].interactable = true;
            }
            else
            {
                UpgradeButtons[i].interactable = false;
            }
        }
    }
}
