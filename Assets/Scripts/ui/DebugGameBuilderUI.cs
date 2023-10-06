using System;
using persistence;
using persistence.data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class DebugGameBuilderUI : MonoBehaviour, IDataPersistence
    {
        public TMP_InputField behindOffsetInputField;
        public TMP_InputField distanceFromStartInputField;
        public TMP_InputField distanceMultiplierInputField;
        public TMP_InputField landObstacleMaxCountInputField;
        public TMP_InputField airObstacleMaxCountInputField;
        public TMP_InputField landObstacleMinCountInputField;
        public TMP_InputField airObstacleMinCountInputField;
        public TMP_InputField itemSpawnChanceInputField;
        public TMP_InputField hookSpeedInputField;
        public TMP_InputField pullingSpeedInputField;
        public TMP_InputField maxLengthInputField;
        public TMP_InputField wallBreakForceInputField;
        public TMP_InputField enemyMaxSpeedInputField;
        public TMP_InputField enemyMinSpeedInputField;
        public TMP_InputField speedMultiplierInputField;
        public TMP_InputField healthMultiplierInputField;
        public TMP_InputField powerMultiplierInputField;
        public TMP_InputField speedDownByGroundInputField;
        public TMP_InputField speedDownByTimeInputField;
        public TMP_InputField healthDownByTimeInputField;
        public TMP_InputField empowerMultiplierInputField;
        public TMP_InputField defensePowerInputField;

        public Button button;

        private GameData _tempGameData;
        private GameData _gameData;

        public void LoadData(GameData data)
        {
            Debug.Log("when did i load this?");
            _gameData = data;
            Debug.Log(data);
            behindOffsetInputField.text = data.behindOffset.ToString();
            distanceFromStartInputField.text = data.distanceFromStart.ToString();
            distanceMultiplierInputField.text = data.distanceMultiplier.ToString();
            landObstacleMaxCountInputField.text = data.landObstacleMaxCount.ToString();
            airObstacleMaxCountInputField.text = data.airObstacleMaxCount.ToString();
            landObstacleMinCountInputField.text = data.landObstacleMinCount.ToString();
            airObstacleMinCountInputField.text = data.airObstacleMinCount.ToString();
            itemSpawnChanceInputField.text = data.itemSpawnChance.ToString();
            hookSpeedInputField.text = data.hookSpeed.ToString("00");
            pullingSpeedInputField.text = data.pullingSpeed.ToString("00");
            maxLengthInputField.text = data.maxLength.ToString("00");
            wallBreakForceInputField.text = data.wallBreakForce.ToString("00");
            enemyMaxSpeedInputField.text = data.enemyMaxSpeed.ToString("00");
            enemyMinSpeedInputField.text = data.enemyMinSpeed.ToString("00");
            speedMultiplierInputField.text = data.speedMultiplier.ToString("00");
            healthMultiplierInputField.text = data.healthMultiplier.ToString("00");
            powerMultiplierInputField.text = data.powerMultiplier.ToString("00");
            speedDownByGroundInputField.text = data.speedDownByGround.ToString("00");
            speedDownByTimeInputField.text = data.speedDownByTime.ToString("00");
            healthDownByTimeInputField.text = data.healthDownByTime.ToString("00");
            empowerMultiplierInputField.text = data.empowerMultiplier.ToString("00");
            defensePowerInputField.text = data.defensePower.ToString("00");
        }

        public void SaveData(GameData data)
        {
            data.behindOffset = int.Parse(behindOffsetInputField.text);
            data.distanceFromStart = int.Parse(distanceFromStartInputField.text);
            data.distanceMultiplier = int.Parse(distanceMultiplierInputField.text);
            data.landObstacleMaxCount = int.Parse(landObstacleMaxCountInputField.text);
            data.airObstacleMaxCount = int.Parse(airObstacleMaxCountInputField.text);
            data.landObstacleMinCount = int.Parse(landObstacleMinCountInputField.text);
            data.airObstacleMinCount = int.Parse(airObstacleMinCountInputField.text);
            data.itemSpawnChance = int.Parse(itemSpawnChanceInputField.text);
            data.hookSpeed = float.Parse(hookSpeedInputField.text);
            data.pullingSpeed = float.Parse(pullingSpeedInputField.text);
            data.maxLength = float.Parse(maxLengthInputField.text);
            data.wallBreakForce = float.Parse(wallBreakForceInputField.text);
            data.enemyMaxSpeed = float.Parse(enemyMaxSpeedInputField.text);
            data.enemyMinSpeed = float.Parse(enemyMinSpeedInputField.text);
            data.speedMultiplier = float.Parse(speedMultiplierInputField.text);
            data.healthMultiplier = float.Parse(healthMultiplierInputField.text);
            data.powerMultiplier = float.Parse(powerMultiplierInputField.text);
            data.speedDownByGround = float.Parse(speedDownByGroundInputField.text);
            data.speedDownByTime = float.Parse(speedDownByTimeInputField.text);
            data.healthDownByTime = float.Parse(healthDownByTimeInputField.text);
            data.empowerMultiplier = float.Parse(empowerMultiplierInputField.text);
            data.defensePower = float.Parse(defensePowerInputField.text);
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void StartEdit()
        {
            gameObject.SetActive(true);
            // _tempGameData = _gameData.Clone();
            // LoadData(_tempGameData);
        }

        public void Confirm()
        {
            SaveData(_gameData);
            DataPersistenceManager.Instance.SaveGame();
            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            LoadData(_gameData);
            gameObject.SetActive(false);
        }
    }
}