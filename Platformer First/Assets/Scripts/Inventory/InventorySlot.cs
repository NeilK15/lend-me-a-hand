using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image highlight;
    bool selected = false;

    Item item;


    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        UnHighlightSlot();
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (selected)
            {
                UnHighlightSlot();
                item.UnUse();
            }
            else
            {
                if (item.Use())
                {
                    HighlightSlot();
                }
            }

        }
    }

    public void HighlightSlot()
    {
        highlight.enabled = true;
        selected = true;
    }

    public void UnHighlightSlot()
    {
        highlight.enabled = false;
        selected = false;
    }

}
