using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite icon = null;

    public delegate void HighlightSlot();
    public HighlightSlot HighlightSlotCallback;

    // Returns true if to highlight the slot
    public virtual bool Use()
    {
        // MEANT TO BE OVERRIDEN

        // Use The Item
        // Something Might Happen

        Debug.Log("Using " + name);

        return true;
    }

    public virtual void UnUse()
    {
        // MEANT TO BE OVERRIDEN

        Debug.Log("Unusing " + name);
    }

    public void RemoveFromInventory()
    {
        Debug.Log("Removed " + name + " from inventory");
        Inventory.instance.Remove(this);
    }
     
}
