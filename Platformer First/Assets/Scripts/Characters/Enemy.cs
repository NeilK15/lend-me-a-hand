using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public float damage = 12;
    public float speed = 5;

    public override void Kill()
    {
        base.Kill();

        Instantiate(deathEffect, transform.position, transform.rotation);

        Spawner.instance.Spawn();

        Destroy(gameObject);
    }

}
