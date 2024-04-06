using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float health = 100f;
    public float speed = 5f;
    public int moneyOnKill = 10;
    public int healthLostOnExit = 1;
    public int distanceTravelled = 0;
    //display the enemy's health
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        //healthText = GameObject.Find("HealthText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
