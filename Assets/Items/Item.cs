using UnityEngine;


[CreateAssetMenu(fileName ="Newitem", menuName ="Inventory/item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public bool isDefault;
}
