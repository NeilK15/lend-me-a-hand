using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Holdable
{

    public LayerMask enemyCheck;

    public override void UseHoldable()
    {
        base.UseHoldable();

        Debug.Log("Weilding" + equipment.name);
    }

}
