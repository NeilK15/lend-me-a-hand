using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100;
    public float damage = 12;
    public float speed = 5;

    public GameObject deathEffect;

    private void Start()
    {

    }

    // Maybe change type to bool to confirm kill
    public void Damage(float hitpoints)
    {
        health -= hitpoints;
        if (health <= 0)
            Kill();
    }

    // Public so player can kill everyone HEHEHEHEHEHAAAAAA
    public void Kill()
    {
        // Do some cool stuff

        Instantiate(deathEffect, transform.position, transform.rotation);

        Spawner.instance.Spawn();

        Destroy(gameObject);
        

    }


}
