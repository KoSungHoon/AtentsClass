using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventor System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisPlayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
}
