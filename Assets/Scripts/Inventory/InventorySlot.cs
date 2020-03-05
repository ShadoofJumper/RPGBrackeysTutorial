using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // current slot item
    private Item _item;
    // get link to slot item icon;
    public Image slotIcon;
    // get link to remove button
    public Button removeButton;

    public void AddItem(Item item)
    {
        _item = item;
        // set for current slot icon
        slotIcon.sprite = _item.icon;
        slotIcon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        slotIcon.sprite = null;
        slotIcon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(_item);
    }

    public void UseItem()
    {
        if (_item!=null)
        {
            _item.Use();
        }
    }
}
