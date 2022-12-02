using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("References")]
    public new Rigidbody2D rigidbody;
    public float speed = 4f;
    public float controlSeconds = 3f;
    public float secondsUntilKill = 5f;
    public float radius = 2f;
    public LayerMask groundLayerMask;
    public LayerMask enemyLayerMask;
    public GameObject fireBallExplosion;
    
    private float _damage;
    
    public float Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    private bool _shouldFollowMouse;
    private Vector2 _lastPosition;
    private Vector2 _mousePos;
    private ParticleSystem ps;
    

    private void Start()
    {
        // Initialize boolean and start coroutine to kill fireball
        _shouldFollowMouse = true;
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(FollowMouse(controlSeconds));
    }
    
    

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Cancel control manually
        /*if (Input.GetButton("Fire1"))
            _shouldFollowMouse = false;*/
        
        CheckCollisions();
    }
    
    

    private void FixedUpdate()
    {
        MoveFireBall();
    }

    IEnumerator FollowMouse(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartCoroutine(KillAfter(secondsUntilKill));
        _shouldFollowMouse = false;
    }

    IEnumerator KillAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Death();
    }

    private void Death()
    {
        // Create explosion effect
        Instantiate(fireBallExplosion, transform.position, Quaternion.identity);
        
        // Destroy the game object
        Destroy(gameObject);
    }

    private void MoveFireBall()
    {
        if (_shouldFollowMouse)
        {
            rigidbody.velocity = (_mousePos - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
        }
    }

    private void CheckCollisions()
    {
        Collider2D ground = Physics2D.OverlapCircle(transform.position, radius, groundLayerMask);
        if (ground != null) {
            Death();
        }
        
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, radius, enemyLayerMask);
        if (enemy != null)
        {
            print("Should Work");
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.Damage(_damage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
    
}
