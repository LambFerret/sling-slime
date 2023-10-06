using System;
using persistence;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class NewGameButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }


        private void OnClick()
        {
            DataPersistenceManager.Instance.NewGame();
            DataPersistenceManager.Instance.SaveGame();
        }
    }
}