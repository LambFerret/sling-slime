using item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    [RequireComponent(typeof(Button))]
    public class ItemSlotUI : MonoBehaviour
    {
        // public Image icon;

        private Item _item;
        private TextMeshProUGUI _text;

        public bool IsEmpty => _item == null;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetItem(Item item)
        {
            _item = item;
            // icon.sprite = _item.sprite;
            // icon.enabled = true;
            _text.text = _item.itemName;
        }

        private void ClearItem()
        {
            _item = null;
            // icon.sprite = null;
            // icon.enabled = false;
            _text.text = "";
        }

        public void UseItem()
        {
            if (_item is null) return;
            _item.Use();
            ClearItem();
        }
    }
}