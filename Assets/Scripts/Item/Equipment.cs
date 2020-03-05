using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New equip", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int damageModifier;
    public int armorModifier;
    public EquipmentSlot slotType;
    public SkinnedMeshRenderer equipMesh;
    // araray have info about what part pf body this mesh cover
    public MeshRegion[] meshRegions;
    public override void Use()
    {
        base.Use();
        // equipe item using equipmentManager
        EquipmentManager.instance.Equip(this);
        // after equipe item in equipment we remove it from inventory
        base.RemoveItem();
    }

}

public enum EquipmentSlot {Head, Chest, Legs, Feet, Weapon, Shield };
public enum MeshRegion {Legs, Arms, Torso }
