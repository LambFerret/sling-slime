using System;
using ScriptableObjects;
using sling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class DebugSlimeBuilderUI : MonoBehaviour
    {
        public PlayerContainer playerContainer;

        public TMP_InputField status1;
        public TMP_InputField status2;
        public TMP_InputField status3;
        public TMP_Dropdown type;

        public Button button;

        private void Awake()
        {
            foreach (Slime.SlimeType slimeType in Enum.GetValues(typeof(Slime.SlimeType)))
            {
                type.options.Add(new TMP_Dropdown.OptionData(slimeType.ToString()));
            }

            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            var slime = ScriptableObject.CreateInstance<Slime>();
            slime.slimeType = (Slime.SlimeType)type.value;
            slime.power = float.Parse(status1.text);
            slime.speed = float.Parse(status2.text);
            slime.health = float.Parse(status3.text);
            playerContainer.AddSlime(slime);
            gameObject.SetActive(false);
        }

    }
}