using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //on start add ,ethods to eauipment event
    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // this method will be invoke when in equipmant managers something changes
    // when equipmant managers chaning we modifi our states using info about what items go and leave
    public void OnEquipmentChanged(Equipment oldItem, Equipment newItem)
    {
        // add modifier to our states
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
        
    }


}
