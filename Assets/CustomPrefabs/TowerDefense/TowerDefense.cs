using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TowerDefense : MonoBehaviour
{
    [Header("Inscribed")]
    public int currentRound = 1;
    public int maxRounds = 3;
    public int currentMoney = 1000;
    public int currentHealth = 100;

    public Image healthBar;
    public TextMeshProUGUI  uiMoneyValue;
    public TextMeshProUGUI  uiRoundValue;

    // Start is called before the first frame update
    void Start()
    {
        uiMoneyValue.text = currentMoney.ToString("#,0");
        uiRoundValue.text = $"Round | {currentRound}/{maxRounds}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100f;
    }

    public void IncreaseMoney(int money)
    {
        currentMoney += money;
        uiMoneyValue.text = currentMoney.ToString("#,0");
    }

    public void DecreaseMoney(int money)
    {
        currentMoney -= money;
        uiMoneyValue.text = currentMoney.ToString("#,0");
    }
}
