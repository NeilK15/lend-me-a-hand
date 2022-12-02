using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();

    }

    private void PickUp()
    {
        // Add to inventory

        Debug.Log("Picking Up " + item.name);
        if (Inventory.instance.Add(item))
        {
            Destroy(gameObject);
        }


    }


}
