using UnityEngine.EventSystems;
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
    public Equipment _staff;
    public delegate void OnEquipmentChange();
    public OnEquipmentChange OnEquipmentChangeCallback;

    private void Start()
    {
        inventory = Inventory.instance;
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numberOfSlots];
        inGameEquipment = new GameObject[numberOfSlots];
        Equip(_staff);
    }


    private void Update()
    {
        
        /*
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            */

        if (Input.GetButtonDown("Fire1"))
        {
        print("Here");

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

            

            GameObject inGameObject = Instantiate(newItem.inGameItem);
            inGameObject.transform.parent = this.pickUpPoint.transform;
            inGameObject.transform.position = pickUpPoint.position;
            inGameObject.transform.rotation = pickUpPoint.rotation;
            inGameEquipment[slotIndex] = inGameObject;
            
            if (OnEquipmentChangeCallback != null)
                            OnEquipmentChangeCallback.Invoke();
            
            
            return true;
        }



    }

    public void UnEquip(Equipment item)
    {
        int slotIndex = (int)item.equipSlot;

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

    public void DropItem()
    {
        if (inGameEquipment[4] != null)
        {
            Equipment toRemove = currentEquipment[4];
            GameObject droppedItem = Instantiate(toRemove.groundObject, inGameEquipment[4].transform.position, inGameEquipment[4].transform.rotation);
            droppedItem.transform.parent = null;

            inventory.Remove(toRemove);
            UnEquip(toRemove);
        }
    }

}
