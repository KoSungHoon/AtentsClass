using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragItem newItem = eventData.pointerDrag.GetComponent<DragItem>();
        DragItem curItem = GetComponentInChildren<DragItem>();
        if(curItem != null)
        {
            curItem.ChageParent(newItem.orgParent, true);
        }
        newItem.ChageParent(transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
