using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public float delay;

    public EnemySpawnData(GameObject prefab, float delay)
    {
        this.enemyPrefab = prefab;
        this.delay = delay;
    } 
}

public class TowerDefense : MonoBehaviour
{
    [Header("Inscribed")]
    public int currentRound = 0;
    public int maxRounds = 3;
    public int currentMoney = 1000;
    public int currentHealth = 100;

    public Image healthBar;
    public TextMeshProUGUI  uiMoneyValue;
    public TextMeshProUGUI  uiRoundValue;

    public GameObject enemyTierOne;
    public GameObject enemyTierTwo;
    public GameObject enemyTierThree;

    private StartNode startNode;

    List<EnemySpawnData> wave = new List<EnemySpawnData>();
    List<List<EnemySpawnData>> waves = new List<List<EnemySpawnData>>();

    private bool roundActiveBool = false;
    private bool allEnemiesSpawned = false;
    public int numEnemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiMoneyValue.text = currentMoney.ToString("#,0");
        uiRoundValue.text = $"Round | {currentRound}/{maxRounds}";
        startNode = GameObject.Find("StartNode").GetComponent<StartNode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Check if the 's' key is pressed
        {
            SpawnPresetEnemy(); // Call the function to spawn the preset enemy
        }

        if (allEnemiesSpawned && numEnemiesAlive == 0)
        {
            roundActiveBool = false;
        }
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

    public void SpawnWave()
    {
        // Make sure a round is not currently active
        if (!roundActiveBool)
        {
            // If not, then mark a round is active
            roundActiveBool = true;
            // Note that all enemies have yet to be spawned
            allEnemiesSpawned = false;
            // Increment round counter
            currentRound++;
            // Update round display
            uiRoundValue.text = $"Round | {currentRound}/{maxRounds}";
            
            SpawnSpecificWave(waves[currentRound - 1]);
        }
    }

    private IEnumerator SpawnSpecificWave(List<EnemySpawnData> WaveData)
    {
        foreach (var data in wave)
            {
                yield return new WaitForSeconds(data.delay);
                startNode.Spawn(data.enemyPrefab);
            }

        allEnemiesSpawned = true;
    }

    public void SpawnPresetEnemy()
    {
        if (enemyTierOne != null && startNode != null) // Check if the preset enemy prefab and startNode are assigned
        {
            startNode.Spawn(enemyTierOne); // Spawn the preset enemy using the startNode's Spawn function
        }
        else if (enemyTierOne == null)
        {
            if (startNode == null)
            {
                Debug.LogError("Preset enemy prefab or startNode is not assigned.");
            } else
            {
                Debug.LogError("Preset enemy prefab is not assigned.");
            }
        } else 
        {
            Debug.LogError("startNode is not assigned.");
        }
    }
}
