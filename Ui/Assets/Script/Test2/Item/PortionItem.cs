using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary> ���� ������ - ���� ������ </summary>
public class PortionItem : CountableItem, IUsableItem
{
    public PortionItem(PortionItemData data, int amount = 1) : base(data, amount) { }

    public bool Use()
    {
        // �ӽ� : ���� �ϳ� ����
        Amount--;

        return true;
    }

    protected override CountableItem Clone(int amount)
    {
        return new PortionItem(CountableData as PortionItemData, amount);
    }
    
}
