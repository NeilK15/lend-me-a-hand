using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    
    public float health = 100;

    public GameObject deathEffect;
    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Kill();
    }

    // Public so player can kill everyone HEHEHEHEHEHAAAAAA
    public virtual void Kill()
    {
        // Do some cool stuff


    }

}
