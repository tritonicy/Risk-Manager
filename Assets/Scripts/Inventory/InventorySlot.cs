using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public int amount;


    public InventorySlot() {
        ClearSlot();
    }

    public InventorySlot(InventoryItem item, int amount) {
        this.item = item;
        this.amount = amount;
    }

    public void ClearSlot() {
        item = null;
        amount = -1;
    }

    public void AddToStack(int amount) {
        this.amount += amount; 
    }

    public void RemoveFromStack(int amount) {
        this.amount -= amount;
    }

    public bool AmountRemainingInStack(int amountToadd) {
        return this.amount + amountToadd <= item.maxStackSize;
    }

    public bool AmountRemainingInStack(int amountToadd, out int remainingStackSize) {
        remainingStackSize = item.maxStackSize - this.amount;
        return AmountRemainingInStack(amountToadd);
    }
}
