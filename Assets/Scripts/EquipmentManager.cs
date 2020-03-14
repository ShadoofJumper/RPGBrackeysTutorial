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

    // for set correct position of sword and shield
    public Transform SwordTransform;
    public Transform ShieldTransform;

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

    // start equip blenser shape
    private float[] startBlendShapeParam = new float[3];


    private void Start()
    {
        // get lenght of equipment
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        meshEquipment = new SkinnedMeshRenderer[numSlots];
        inventory = Inventory.instance;

        // save start blend shape parametrs
        for (int i = 0; i < startBlendShapeParam.Length; i++)
        {
            startBlendShapeParam[i] = target.GetBlendShapeWeight(i);
        }
        

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

        SetEquipmentBlendShape(item, 100, false);

        currentEquipment[itemIndex] = item;
        // create mesh on scene
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(item.equipMesh);
        meshEquipment[itemIndex] = newMesh;


        if (item != null && item.slotType == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = SwordTransform;
        }
        else if (item != null && item.slotType == EquipmentSlot.Shield)
        {
            newMesh.rootBone = ShieldTransform;
        }
        else
        {
            //newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = target.bones;
            newMesh.rootBone = target.rootBone;
        }
       
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
            // set start blend shape paramentr
            SetEquipmentBlendShape(itemInEquip, 0, true);
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



    private void SetEquipmentBlendShape(Equipment item, int weight, bool resetToStart)
    {
        foreach (MeshRegion meshRegion in item.meshRegions)
        {
            if (resetToStart)
            {
                target.SetBlendShapeWeight((int)meshRegion, startBlendShapeParam[(int)meshRegion]);
            }
            else
            {
                target.SetBlendShapeWeight((int)meshRegion, weight);
            }
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
