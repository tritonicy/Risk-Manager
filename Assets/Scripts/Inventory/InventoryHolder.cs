using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] public int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem{get; private set;}

    public static Action<InventorySystem> onDynamicInventorySystemRequested; 

    private void Awake() {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
