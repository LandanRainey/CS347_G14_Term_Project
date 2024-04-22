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
    public TextMeshProUGUI  gameOverRounds;

    public GameObject enemyTierOne;
    public GameObject enemyTierTwo;
    public GameObject enemyTierThree;

    private StartNode startNode;

    List<EnemySpawnData> wave = new List<EnemySpawnData>();
    List<List<EnemySpawnData>> waves = new List<List<EnemySpawnData>>();

    private bool roundActiveBool = false;
    private bool allEnemiesSpawned = true;
    public int numEnemiesAlive = 0;

    public GameObject gameOverUI; 
    public GameObject gameWinUI;

    // Start is called before the first frame update
    void Start()
    {
        uiMoneyValue.text = currentMoney.ToString("#,0");
        
        startNode = GameObject.Find("StartNode").GetComponent<StartNode>();

        List<EnemySpawnData> waveOne = new List<EnemySpawnData>();
        for (int a = 0; a < 30; a++)
        {
            // Creating new EnemySpawnData with enemyTierOne and delay 0.5
            waveOne.Add(new EnemySpawnData(enemyTierOne, 0.5f));
        }
        List<EnemySpawnData> waveTwo = new List<EnemySpawnData>();
        for (int b = 0; b < 5; b++)
        {
            for (int c = 0; c < 4; c++)
            {
                waveTwo.Add(new EnemySpawnData(enemyTierOne, 0.4f));
            }
            waveTwo.Add(new EnemySpawnData(enemyTierTwo, 0.5f));
        }

        List<EnemySpawnData> waveThree = new List<EnemySpawnData>();
        for (int d = 0; d < 7; d++)
        {
            for (int e = 0; e < 5; e++)
            {
                waveThree.Add(new EnemySpawnData(enemyTierOne, 0.3f));
            }
            waveThree.Add(new EnemySpawnData(enemyTierTwo, 0.5f));
            waveThree.Add(new EnemySpawnData(enemyTierThree, 0.1f));
        }

        waves.Add(waveOne);
        waves.Add(waveTwo);
        waves.Add(waveThree);

        uiRoundValue.text = $"Round | {currentRound}/{waves.Count}";

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Check if the 's' key is pressed
        {
            SpawnPresetEnemy(); // Call the function to spawn the preset enemy
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numEnemiesAlive = enemies.Length;

        if (currentHealth <= 0)
        {
            gameOver();
        }

        if (allEnemiesSpawned && numEnemiesAlive == 0)
        {
            roundActiveBool = false;
        }

        if (allEnemiesSpawned && !roundActiveBool && currentRound == maxRounds)
        {
            gameWin();
        }
    }

    public void gameWin()
    {
        gameWinUI.SetActive(true);
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true); 
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

    public void Button_SpawnWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            roundActiveBool = false;
        }

        // Make sure a round is not currently active
        //Debug.Log($"roundActiveBool: {roundActiveBool}, allEnemiesSpawned: {allEnemiesSpawned}");
        if (!roundActiveBool && allEnemiesSpawned)
        {
            // If not, then mark a round is active
            roundActiveBool = true;
            // Note that all enemies have yet to be spawned
            allEnemiesSpawned = false;
            // Increment round counter
            currentRound++;
            // Update round display
            uiRoundValue.text = $"Round {currentRound}/{maxRounds}";
            gameOverRounds.text = $"Round | {currentRound-1}/{maxRounds}";
            
            foreach (var data in waves[currentRound - 1])
                {
                    yield return new WaitForSecondsRealtime(data.delay);
                    startNode.Spawn(data.enemyPrefab);
                }

            allEnemiesSpawned = true;
        }
    }

    private void SpawnSpecificWave(List<EnemySpawnData> WaveData)
    {
       
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
