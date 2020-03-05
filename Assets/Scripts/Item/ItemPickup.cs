using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item item;

    // rewritable methos for Interaction
    public override void Interact()
    {
        base.Interact();
        // add remove object after click
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("We get item: "+ item.name);
        //
        bool wasPickedUp = Inventory.instance.AddItem(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
       
    }
}
