using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUIBody;

    private Inventory inventory;
    [SerializeField] private Transform parentForItemCells;
    private InventorySlot[] slots;

    private void Start()
    {
        // get acces to inventory using singlton
        inventory = Inventory.instance;
        // add update on event of modify inventory
        inventory.onModifyInventory += UpdateUI;

        // get all slots ui objects
        slots = parentForItemCells.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUIBody.SetActive(!inventoryUIBody.activeSelf);
        }
    }

    // update inventory elements state
    private void UpdateUI()
    {
        Debug.Log("Update inventory");

        for (int i = 0; i < slots.Length; i++)
        {
            // if in range of element we have then we can put item in slot
            if ( i < inventory.itemsList.Count)
            {
                slots[i].AddItem(inventory.itemsList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

}
