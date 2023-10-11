using item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    [RequireComponent(typeof(Button))]
    public class ItemSlotUI : MonoBehaviour
    {
        private Image _icon;
        private Item _item;
        private TextMeshProUGUI _text;
        private float _duration;
        private bool _isNowUsing;

        public bool IsEmpty => _item == null;

        private void Awake()
        {
            _icon = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetItem(Item item)
        {
            _item = item;
            _icon.sprite = _item.sprite;
            _icon.enabled = true;
            _text.text = _item.itemName;
            _isNowUsing = false;
        }

        private void Update()
        {
            if (_item is null) return;

            if (_isNowUsing)
            {
                _duration -= Time.deltaTime;
                _icon.fillAmount = _duration / _item.duration;
                if (_duration <= 0)
                {
                    ClearItem();
                }
            }
        }

        private void ClearItem()
        {
            _item.OnEnd();
            _isNowUsing = false;
            _item.gameObject.SetActive(false);
            _item = null;
            _icon.sprite = null;
            _icon.enabled = false;
            _text.text = "";
            _icon.fillAmount = 1;
        }

        public void UseItem()
        {
            if (_item is null) return;
            if (_isNowUsing) return;
            _item.OnUse();
            _duration = _item.duration;
            _isNowUsing = true;
        }
    }
}