using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> iventorySlots;

    public Action<InventorySlot> onInventorySlotChanged;


    public InventorySystem(int size) {
        iventorySlots = new List<InventorySlot>(size);

        for(int i = 0; i < size ;i++) {
            iventorySlots[i] = new InventorySlot();
        }
    }
}
