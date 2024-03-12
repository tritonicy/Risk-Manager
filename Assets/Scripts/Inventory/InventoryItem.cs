using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public int itemId;
    public string itemName;
    [TextArea(4,4)] public string description;
    public int maxStackSize;
    public Sprite Icon;
}
