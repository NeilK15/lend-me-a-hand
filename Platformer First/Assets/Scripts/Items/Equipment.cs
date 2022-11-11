using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public Sprite equipmentSprite;

    public int armorModifier;
    public int damageModifier;

    public GameObject inGameItem;

    public override bool Use()
    {
        base.Use();
        // Equip the item
        return EquipmentManager.instance.Equip(this);
    }

    public override void UnUse()
    {
        base.UnUse();

        // Unequip the item

        EquipmentManager.instance.UnEquip(this);
    }

}

public enum EquipmentSlot { Head, Chest, Back, Legs, Hand, Feed }
