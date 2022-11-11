using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    // Singleton for equipment manager
    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Bro, how I have multiple equipment managers :0");
            return;
        }
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;
    GameObject[] inGameEquipment;

    public Transform pickUpPoint;

    Inventory inventory;

    public delegate void OnEquipmentChange();
    public OnEquipmentChange OnEquipmentChangeCallback;

    private void Start()
    {
        inventory = Inventory.instance;
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numberOfSlots];
        inGameEquipment = new GameObject[numberOfSlots];
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            // SHOOT RAY IN DIRECTION OF CLICK

            if (inGameEquipment[4] != null)
            {
                Holdable holdable = inGameEquipment[4].GetComponent<Holdable>();
                if (holdable != null)
                {
                    holdable.UseHoldable();
                }
            }

        }
    }


    public bool Equip(Equipment newItem)
    {

        int slotIndex = (int)newItem.equipSlot;

        if (currentEquipment[slotIndex] != null)
        {
            Debug.Log("Equipment slot is full");
            return false;
        }
        else
        {
            Debug.Log("Equipped " + newItem.name);
            currentEquipment[slotIndex] = newItem;

            if (OnEquipmentChangeCallback != null)
                OnEquipmentChangeCallback.Invoke();

            GameObject inGameObject = Instantiate(newItem.inGameItem);
            inGameObject.transform.parent = this.pickUpPoint.transform;
            inGameObject.transform.position = pickUpPoint.position;
            inGameObject.transform.rotation = pickUpPoint.rotation;
            inGameEquipment[slotIndex] = inGameObject;
            
            return true;
        }

        

    }

    public void UnEquip(Equipment item)
    {
        int slotIndex = (int) item.equipSlot;

        if (inGameEquipment[slotIndex] != null)
        {
            Destroy(inGameEquipment[slotIndex]);
            inGameEquipment[slotIndex] = null;
        }

        currentEquipment[slotIndex] = null;

        if (OnEquipmentChangeCallback != null)
            OnEquipmentChangeCallback.Invoke();

        Debug.Log("Unequipped " + item.name);
    }

}
