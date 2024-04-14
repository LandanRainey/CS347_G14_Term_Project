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
    public int currentMoney = 1000;
    public int currentHealth = 100;

    public Image healthBar;
    public TextMeshProUGUI  uiText;

    // Start is called before the first frame update
    void Start()
    {
        // Script to find healthBar? Would only be necessary in prefab, think we're good?
        // Also can't be bothered to figure out how to find money lol
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
        uiText.text = currentMoney.ToString("#,0");
    }

    public void DecreaseMoney(int money)
    {
        currentMoney -= money;
        uiText.text = currentMoney.ToString("#,0");
    }
}
