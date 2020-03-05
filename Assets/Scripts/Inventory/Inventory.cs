using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singlton
    public static Inventory instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.Log("Try make inventory ut instance already exist!");
            return;
        }
        instance = this;
    }

    #endregion

    // event for changin inventory
    // initsialize neww delegate type
    public delegate void OnModifyInventory();
    // create new delegate
    public OnModifyInventory onModifyInventory;


    public int slotCount;
    public List<Item> itemsList = new List<Item>();



    public bool AddItem(Item item)
    {
        if (!item.isDefault)
        {
            if(itemsList.Count >= slotCount)
            {
                Debug.Log("No more space!");
                return false;
            }
            itemsList.Add(item);
            if (onModifyInventory!=null)
                onModifyInventory.Invoke();
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        itemsList.Remove(item);
        if (onModifyInventory != null)
            onModifyInventory.Invoke();
    }
}
