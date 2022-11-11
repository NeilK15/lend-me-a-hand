using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon
{

    public GameObject throwablePrefab;

    public override void UseHoldable()
    {
        base.UseHoldable();

        Throw();



    }

    private void Throw()
    {
        // Throw Item
        Instantiate(throwablePrefab, transform.position, transform.rotation);


        // Remove the item from hand
        //EquipmentManager.instance.UnEquip(equipment);

        // Remove the item from inventory
        //Inventory.instance.Remove(equipment);

    }

}
