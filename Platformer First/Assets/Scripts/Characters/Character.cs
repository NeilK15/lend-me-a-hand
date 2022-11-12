using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float health = 100;


    public GameObject deathEffect;


    // Maybe change type to bool to confirm kill
    public void Damage(float hitpoints)
    {
        health -= hitpoints;
        if (health <= 0)
            Kill();
    }

    // Public so player can kill everyone HEHEHEHEHEHAAAAAA
    public virtual void Kill()
    {
        // Do some cool stuff


    }

}
