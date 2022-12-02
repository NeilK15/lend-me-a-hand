using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : Character
{

    public EnemyData enemy;
    public float damageRate;

    private PlayerController _player;
    private float _timeSinceLastDamage;
    private float _speed;

    private bool CanDamage() => _timeSinceLastDamage > damageRate;
    
    private void Start()
    {
        _player = PlayerController.instance;
        Random rand = new Random();

        _speed = enemy.speed + (2*(float)rand.NextDouble() - 1f);
    }

    private void LateUpdate()
    {
        _timeSinceLastDamage += Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, _player.transform.position) > 0.3f)
            transform.position = Vector2.Lerp(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

    public override void Damage(float damage)
    {
        if (CanDamage())
        {
            base.Damage(damage);
            _timeSinceLastDamage = 0;
        }
    }

    public override void Kill()
    {
        base.Kill();

        Instantiate(deathEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
    
}
