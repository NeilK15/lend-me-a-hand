using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon
{

    public GameObject throwablePrefab;
    public Transform throwPoint;
    public float throwSpeed = 50f;

    private void Start()
    {

    }

    public override void UseHoldable()
    {
        base.UseHoldable();

        Throw();



    }

    private void Throw()
    {
        // Calculating the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculating the click relative to the player
        Vector2 clickRelativeToPlayer = mousePos - transform.position;

        // Throw Item, adding force in direction of throw
        GameObject throwable = Instantiate(throwablePrefab, transform.position, transform.rotation);
        throwable.GetComponent<Rigidbody2D>().AddForce(clickRelativeToPlayer.normalized * throwSpeed, ForceMode2D.Impulse);

        // Remove the item from hand
        //EquipmentManager.instance.UnEquip(equipment);

        // Remove the item from inventory
        //Inventory.instance.Remove(equipment);

    }

}
