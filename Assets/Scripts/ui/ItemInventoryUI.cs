using System.Collections.Generic;
using item;
using UnityEngine;

namespace ui
{
    public class ItemInventoryUI : MonoBehaviour
    {
        public List<ItemSlotUI> items;

        private void Awake()
        {
            items = new List<ItemSlotUI>();
            foreach (Transform child in transform)
            {
                var itemSlot = child.GetComponent<ItemSlotUI>();
                if (itemSlot != null)
                {
                    items.Add(itemSlot);
                }
            }
        }

        public void AddItem(Component sender, object data)
        {
            if (data is not Item item) return;
            AddItem(item);
        }

        private void AddItem(Item item)
        {
            foreach (var slot in items)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(item);
                    break;
                }
            }
        }

        private void Update()
        {
            // use item by pressing 1-9
            for (var i = 0; i < items.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    Use(i);
                }
            }
        }

        private void Use(int index)
        {
            if (index < 0 || index >= items.Count) return;
            items[index].UseItem();
        }
    }
}