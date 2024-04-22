using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [Header("Inscribed")]
    public int level = 1;
    public float range = 10f; // placeholder, what are the units?
    public float damage = 50f; // placeholder, what are the units?
    public float fireRate = 1f; // placeholder, units per second?
    public List<Enemy> enemiesInRange; // This should be populated by the parent object
    public GameObject cannonBall; // placeholder for the projectile prefab
    //public GameObject ShootingSound; // placeholder for the shooting SFX
    public float projectileSpeed = 100f; // You can adjust this value as needed
	public AudioSource audioSource;
	public AudioClip shootingAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        // Start the Attack coroutine
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        // loop forever
        while (true)
        {
            //if there are enemies in range\
            if (enemiesInRange.Count > 0)
            {
                //get the position to instantiate the projectile
                Vector3 spawnPosition = transform.GetChild(0).position;

                spawnPosition.y += transform.position.y; // adjust the y position to be at the top of the ally

                // Instantiate a new projectile
                GameObject projectile = Instantiate(cannonBall, spawnPosition, Quaternion.identity);

                // Get the direction to the enemy
                Vector3 direction = enemiesInRange[0].transform.position - spawnPosition;


                // Launch the projectile towards the enemy
                projectile.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;

				//Play SFX
				audioSource.PlayOneShot(shootingAudioClip);

                // set the enemy health to 0
                enemiesInRange[0].health -= damage;
            }

            // wait for the fire rate
            yield return new WaitForSeconds(1/fireRate);
        }
    }

    // Update is called once per frame
    void Update()
    {

    // Detect all colliders within the range
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

    // make a list of all the enemies in the range
    enemiesInRange = new List<Enemy>();

    // loop through all the colliders detected to find the enemies
    foreach (Collider hitCollider in hitColliders)
    {
        // check if the collider nhas tag Enemy
        if (hitCollider.tag == "Enemy")
        {
            // get the Enemy component from the collider
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            // add the enemy to the list of enemies in range
            enemiesInRange.Add(enemy);
        }
    }
    
    // Sort the enemies in range based on their health
    enemiesInRange.Sort((a, b) => b.distanceTravelled.CompareTo(a.distanceTravelled));


    // rotate the ally to face the first enemy in the list but only rotate on the y axis
    if (enemiesInRange.Count > 0)
    {
        // get a reference to the transform component of the first child in the list
        Transform childTransform = transform.GetChild(0);
        
        //rotate the ally to face the first enemy in the list but only rotate on the y axis
        Vector3 direction = enemiesInRange[0].transform.position - childTransform.position;
        direction.y = 0; // keep the rotation only on the y axis
        Quaternion rotationToLookAtEnemy = Quaternion.LookRotation(direction);
        childTransform.localRotation = Quaternion.Slerp(childTransform.localRotation, rotationToLookAtEnemy, 0.1f);

    }
    
    
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked on: " + gameObject.GetInstanceID());
    }
}
