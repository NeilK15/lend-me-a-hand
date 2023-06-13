using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{

    private CharacterState _characterState;
    public CharacterState CharacterState
    {
        get
        {
            return _characterState;
        }
        set
        {
            _characterState = value;
            
        }
    }
    
    [Header("Preferences")]
    public float health = 100;

    [Header("References")]
    public GameObject deathEffect;

    private void Start()
    {
        _characterState = CharacterState.Idle;
    }


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


public enum CharacterState {
    Idle,
    OnFire
}