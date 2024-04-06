using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy the projectile after 1 second
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Called when the projectile collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Destroy the projectile when it collides with anything
        Destroy(gameObject);
    }
}
