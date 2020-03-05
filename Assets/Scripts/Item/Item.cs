using UnityEngine;


[CreateAssetMenu(fileName ="Newitem", menuName ="Inventory/item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public bool isDefault;

    public virtual void Use()
    {
        Debug.Log("Use item: "+name);
    }

    // remove item from inventory
    public void RemoveItem()
    {
        Inventory.instance.RemoveItem(this);
    }
}
