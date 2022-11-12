using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{

    public Equipment equipment;

    public virtual void UseHoldable()
    {
        // MEANT TO BE OVERRIDEN

    }

    public void ShowHoldable()
    {
        // CALLED WHEN YOU EQUIP AN ITEM IN THE INVENTORY
        // Show the item, put it in your hand


    }



}
