using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary> ������ ���� �ѵ� </summary>
    public int Capacity { get; private set; }

    // �ʱ� ���� �ѵ�
    [SerializeField, Range(8, 64)]
    private int _initalCapacity = 32;

    // �ִ� ���� �ѵ�(������ �迭 ũ��)
    [SerializeField, Range(8, 64)]
    private int _maxCapacity = 64;

    [SerializeField]
    private InventoryUI _inventoryUI; // ����� �κ��丮 UI

    /// <summary> ������ ��� </summary>
    [SerializeField]
    private Item[] _items;

    private void Awake()
    {
        _items = new Item[_maxCapacity];
        Capacity = _initalCapacity;
    }

    private void Start()
    {
        UpdateAccessibleStatesAll();
    }

    /// <summary> �ε����� ���� ���� ���� �ִ��� �˻� </summary>
    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < Capacity;
    }

    /// <summary> �տ������� ����ִ� ���� �ε��� Ž�� </summary>
    private int FindEmptySlotIndex(int startIndex = 0)
    {
        for (int i = startIndex; i < Capacity; i++)
            if (_items[i] == null)
                return i;
        return -1;
    }


    /// <summary> ��� ���� UI�� ���� ���� ���� ������Ʈ </summary>
    public void UpdateAccessibleStatesAll()
    {
        _inventoryUI.SetAccessibleSlotRange(Capacity);
    }

    /// <summary> �ش� ������ �������� ���� �ִ��� ���� </summary>
    public bool HasItem(int index)
    {
        return IsValidIndex(index) && _items[index] != null;
    }

    /// <summary> �ش� ������ �� �� �ִ� ���������� ���� </summary>
    public bool IsCountableItem(int index)
    {
        return HasItem(index) && _items[index] is CountableItem;
    }

    /// <summary>
    /// �ش� ������ ���� ������ ���� ����
    /// <para/> - �߸��� �ε��� : -1 ����
    /// <para/> - �� ���� : 0 ����
    /// <para/> - �� �� ���� ������ : 1 ����
    /// </summary>
    public int GetCurrentAmount(int index)
    {
        if (!IsValidIndex(index)) return -1;
        if (_items[index] == null) return 0;

        CountableItem ci = _items[index] as CountableItem;
        if (ci == null)
            return 1;

        return ci.Amount;
    }

    /// <summary> �ش� ������ ������ ���� ���� </summary>
    public ItemData GetItemData(int index)
    {
        if (!IsValidIndex(index)) return null;
        if (_items[index] == null) return null;

        return _items[index].Data;
    }

    /// <summary> �ش� ������ ������ �̸� ���� </summary>
    public string GetItemName(int index)
    {
        if (!IsValidIndex(index)) return "";
        if (_items[index] == null) return "";

        return _items[index].Data.Name;
    }

    /// <summary> �ش��ϴ� �ε����� ���� ���� �� UI ���� </summary>
    public void UpdateSlot(int index)
    {
        if (!IsValidIndex(index)) return;

        Item item = _items[index];

        // 1. �������� ���Կ� �����ϴ� ���
        if (item != null)
        {
            // ������ ���
            _inventoryUI.SetItemIcon(index, item.Data.IconSprite);

            // 1-1. �� �� �ִ� ������
            if (item is CountableItem ci)
            {
                // 1-1-1. ������ 0�� ���, ������ ����
                if (ci.IsEmpty)
                {
                    _items[index] = null;
                    RemoveIcon();
                    return;
                }
                // 1-1-2. ���� �ؽ�Ʈ ǥ��
                else
                {
                    _inventoryUI.SetItemAmountText(index, ci.Amount);
                }
            }
            // 1-2. �� �� ���� �������� ��� ���� �ؽ�Ʈ ����
            else
            {
                _inventoryUI.HideItemAmountText(index);
            }
        }
        // 2. �� ������ ��� : ������ ����
        else
        {
            RemoveIcon();
        }

        // ���� : ������ �����ϱ�
        void RemoveIcon()
        {
            _inventoryUI.RemoveItem(index);
            _inventoryUI.HideItemAmountText(index); // ���� �ؽ�Ʈ �����
        }
    }

    public void Swap(int indexA, int indexB)
    {
        if (!IsValidIndex(indexA)) return;
        if (!IsValidIndex(indexB)) return;

        Item itemA = _items[indexA];
        Item itemB = _items[indexB];

        // 1. �� �� �ִ� �������̰�, ������ �������� ���
        //    indexA -> indexB�� ���� ��ġ��
        if (itemA != null && itemB != null &&
            itemA.Data == itemB.Data &&
            itemA is CountableItem ciA && itemB is CountableItem ciB)
        {
            int maxAmount = ciB.MaxAmount;
            int sum = ciA.Amount + ciB.Amount;

            if (sum <= maxAmount)
            {
                ciA.SetAmount(0);
                ciB.SetAmount(sum);
            }
            else
            {
                ciA.SetAmount(sum - maxAmount);
                ciB.SetAmount(maxAmount);
            }
        }
        // 2. �Ϲ����� ��� : ���� ��ü
        else
        {
            _items[indexA] = itemB;
            _items[indexB] = itemA;
        }

        // �� ���� ���� ����
        UpdateSlot(indexA, indexB);
    }

    /// <summary> �κ��丮�� ������ �߰�
    /// <para/> �ִ� �� ������ �׿� ������ ���� ����
    /// <para/> ������ 0�̸� �ִµ� ��� �����ߴٴ� �ǹ�
    /// </summary>
    public int Add(ItemData itemData, int amount = 1)
    {
        int index;

        // 1. ������ �ִ� ������
        if (itemData is CountableItemData ciData)
        {
            bool findNextCountable = true;
            index = -1;

            while (amount > 0)
            {
                // 1-1. �̹� �ش� �������� �κ��丮 ���� �����ϰ�, ���� ���� �ִ��� �˻�
                if (findNextCountable)
                {
                    index = FindCountableItemSlotIndex(ciData, index + 1);

                    // ���� �����ִ� ������ ������ ���̻� ���ٰ� �Ǵܵ� ���, �� ���Ժ��� Ž�� ����
                    if (index == -1)
                    {
                        findNextCountable = false;
                    }
                    // ������ ������ ã�� ���, �� ������Ű�� �ʰ��� ���� �� amount�� �ʱ�ȭ
                    else
                    {
                        CountableItem ci = _items[index] as CountableItem;
                        amount = ci.AddAmountAndGetExcess(amount);

                        UpdateSlot(index);
                    }
                }
                // 1-2. �� ���� Ž��
                else
                {
                    index = FindEmptySlotIndex(index + 1);

                    // �� �������� ���� ��� ����
                    if (index == -1)
                    {
                        break;
                    }
                    // �� ���� �߰� ��, ���Կ� ������ �߰� �� �׿��� ���
                    else
                    {
                        // ���ο� ������ ����
                        CountableItem ci = ciData.CreateItem() as CountableItem;
                        ci.SetAmount(amount);

                        // ���Կ� �߰�
                        _items[index] = ci;

                        // ���� ���� ���
                        amount = (amount > ciData.MaxAmount) ? (amount - ciData.MaxAmount) : 0;

                        UpdateSlot(index);
                    }
                }
            }
        }
        // 2. ������ ���� ������
        else
        {
            // 2-1. 1���� �ִ� ���, ������ ����
            if (amount == 1)
            {
                index = FindEmptySlotIndex();
                if (index != -1)
                {
                    // �������� �����Ͽ� ���Կ� �߰�
                    _items[index] = itemData.CreateItem();
                    amount = 0;

                    UpdateSlot(index);
                }
            }

            // 2-2. 2�� �̻��� ���� ���� �������� ���ÿ� �߰��ϴ� ���
            index = -1;
            for (; amount > 0; amount--)
            {
                // ������ ���� �ε����� ���� �ε������� ���� Ž��
                index = FindEmptySlotIndex(index + 1);

                // �� ���� ���� ��� ���� ����
                if (index == -1)
                {
                    break;
                }

                // �������� �����Ͽ� ���Կ� �߰�
                _items[index] = itemData.CreateItem();

                UpdateSlot(index);
            }
        }

        return amount;
    }
    public void Use(int index)
    {
        if (_items[index] == null) return;

        // ��� ������ �������� ���
        if (_items[index] is IUsableItem uItem)
        {
            // ������ ���
            bool succeeded = uItem.Use();

            if (succeeded)
            {
                UpdateSlot(index);
            }
        }
    }
}