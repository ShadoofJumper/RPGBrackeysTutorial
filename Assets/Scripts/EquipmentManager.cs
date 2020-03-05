using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singlton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // add event, in equipment changed
    // delegate will have info about what changed in equipment
    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    // create event
    public OnEquipmentChanged onEquipmentChanged;

    // array of default equipment
    public Equipment[] defaultEquipment;
    // array of equipment
    private Equipment[] currentEquipment;
    // array of mash of equipment
    private SkinnedMeshRenderer[] meshEquipment;
    // link to target that will have this meshes on it (Player)
    public SkinnedMeshRenderer target;
    private Inventory inventory;
    private void Start()
    {
        // get lenght of equipment
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        meshEquipment = new SkinnedMeshRenderer[numSlots];
        inventory = Inventory.instance;

        DressDefaultEquipment();
    }

    public void Equip(Equipment item)
    {
        // get item index in enum
        int itemIndex = (int)item.slotType;
        //unequip old item
        Equipment oldItem = Unequip(itemIndex);
  

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(oldItem, item);

        currentEquipment[itemIndex] = item;
        // create mesh on scene
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(item.equipMesh);
        newMesh.bones = target.bones;
        newMesh.rootBone = target.rootBone;
        meshEquipment[itemIndex] = newMesh;
        SetEquipmentBlendShape(item, 100);
    }

    public Equipment Unequip(int slotId)
    {
        if (currentEquipment[slotId]!=null)
        {
            // delate mesh of equipe
            if (meshEquipment[slotId]!=null)
            {
                Destroy(meshEquipment[slotId].gameObject);
            }
            Equipment itemInEquip = currentEquipment[slotId];
            inventory.AddItem(itemInEquip);
            currentEquipment[slotId] = null;

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, itemInEquip);

            SetEquipmentBlendShape(itemInEquip, 0);
            return itemInEquip;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        // dress default dress
        DressDefaultEquipment();
    }

    private void SetEquipmentBlendShape(Equipment item, int weight)
    {
        Debug.Log("SetEquipmentBlendShape: "+item.name);
        foreach (MeshRegion meshRegion in item.meshRegions)
        {
            Debug.Log($"Set id: {(int)meshRegion} to {weight}");
            target.SetBlendShapeWeight((int)meshRegion, weight);

            Debug.Log("here: "+ target.GetBlendShapeWeight((int)meshRegion));
        }
    }

    private void DressDefaultEquipment()
    {
        foreach (Equipment item in defaultEquipment)
        {
            Equip(item);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

}
