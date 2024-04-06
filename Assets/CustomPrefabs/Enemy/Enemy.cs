using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float health = 100f;
    public float speed = 5f;
    public int moneyOnKill = 10;
    public int healthLostOnExit = 1;
    public NavMeshAgent agent;
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
        // Coop's simple click to move
        if(Input.GetMouseButtonDown(1))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
        // Micah's destroy when out of health       
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
