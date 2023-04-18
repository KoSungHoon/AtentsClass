using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem=> inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary=> slotDictionary;

    protected virtual void Start()
    {

    }
    public abstract void AssignSlot(InventorySystem invToDisplay);
    protected virtual void UpdateSlot(InventorySlot updateSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if (slot.Value == updateSlot)
            {
                slot.Key.UpdateUISlot(updateSlot);
            }
        }
    }
    public void SlotClicked(InventorySlot_UI clickedSlot) {
        Debug.Log("Slot Clicked");
    }
}
