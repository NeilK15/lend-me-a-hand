using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.Rendering.Universal.Internal;

public class Staff : Weapon
{
    public State _weaponState;
    
    // Start is called before the first frame update
    void Start()
    {
        _weaponState = State.Idle;
    }

    public override void UseHoldable()
    {
        base.UseHoldable();
        ActivateAbility();
    }
    
    private void ActivateAbility()
    {
        switch (_weaponState)
        {
            case(State.Idle):
                print("Staff Is Idle");
                break;
            case(State.Fire):
                print("Staff is on Fire?");
                break;
        }
    }
    
    
}

    


public enum State
{
    Idle,
    Fire
}