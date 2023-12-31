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
        [Header("DISTANCE")] public TMP_InputField behindOffsetInputField;
        public TMP_InputField distanceFromStartInputField;
        public TMP_InputField distanceMultiplierInputField;

        [Header("ENEMY")] public TMP_InputField landObstacleMaxCountInputField;
        public TMP_InputField airObstacleMaxCountInputField;
        public TMP_InputField landObstacleMinCountInputField;
        public TMP_InputField airObstacleMinCountInputField;
        public TMP_InputField enemyMaxSpeedInputField;
        public TMP_InputField enemyMinSpeedInputField;

        [Header("ITEM")] public TMP_InputField itemSpawnChanceInputField;

        [Header("HOOK")] public TMP_InputField hookSpeedInputField;
        public TMP_InputField pullingSpeedInputField;
        public TMP_InputField maxLengthInputField;

        [Header("WALL")] public TMP_InputField wallBreakForceInputField;

        [Header("BASIC STAT")] public TMP_InputField speedMultiplierInputField;
        public TMP_InputField healthMultiplierInputField;
        public TMP_InputField powerMultiplierInputField;

        [Header("ADVANCED STAT")] public TMP_InputField powerCorrectionIntoObstacleInputField;
        public TMP_InputField speedDownByGroundInputField;
        public TMP_InputField speedDownBySizeInputField;
        public TMP_InputField speedDownByTimeInputField;
        public TMP_InputField healthDownByTimeInputField;
        public TMP_InputField empowerMultiplierInputField;
        public TMP_InputField defensePowerInputField;

        private GameData _tempGameData;
        private GameData _gameData;

        public void LoadData(GameData data)
        {
            _gameData = data;
            behindOffsetInputField.text = data.behindOffset.ToString();
            distanceFromStartInputField.text = data.distanceFromStart.ToString();
            distanceMultiplierInputField.text = data.distanceMultiplier.ToString();

            landObstacleMaxCountInputField.text = data.landObstacleMaxCount.ToString();
            airObstacleMaxCountInputField.text = data.airObstacleMaxCount.ToString();
            landObstacleMinCountInputField.text = data.landObstacleMinCount.ToString();
            airObstacleMinCountInputField.text = data.airObstacleMinCount.ToString();

            itemSpawnChanceInputField.text = data.itemSpawnChance.ToString();

            hookSpeedInputField.text = data.hookSpeed.ToString("F3");
            pullingSpeedInputField.text = data.pullingSpeed.ToString("F3");
            maxLengthInputField.text = data.maxLength.ToString("F3");

            wallBreakForceInputField.text = data.wallBreakForce.ToString("F3");
            enemyMaxSpeedInputField.text = data.enemyMaxSpeed.ToString("F3");
            enemyMinSpeedInputField.text = data.enemyMinSpeed.ToString("F3");
            speedMultiplierInputField.text = data.speedMultiplier.ToString("F3");
            healthMultiplierInputField.text = data.healthMultiplier.ToString("F3");
            powerMultiplierInputField.text = data.powerMultiplier.ToString("F3");
            powerCorrectionIntoObstacleInputField.text = data.powerCorrectionIntoObstacle.ToString("F3");
            speedDownByGroundInputField.text = data.speedDownByGround.ToString("F3");
            speedDownBySizeInputField.text = data.speedDownBySize.ToString("F3");
            speedDownByTimeInputField.text = data.speedDownByTime.ToString("F3");
            healthDownByTimeInputField.text = data.healthDownByTime.ToString("F3");
            empowerMultiplierInputField.text = data.empowerMultiplier.ToString("F3");
            defensePowerInputField.text = data.defensePower.ToString("F3");
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
            data.powerCorrectionIntoObstacle = float.Parse(powerCorrectionIntoObstacleInputField.text);
            data.speedDownByGround = float.Parse(speedDownByGroundInputField.text);
            data.speedDownBySize = float.Parse(speedDownBySizeInputField.text);
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
            DataPersistenceManager.Instance.LoadGame();
            _tempGameData = _gameData.Clone();
            LoadData(_tempGameData);
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