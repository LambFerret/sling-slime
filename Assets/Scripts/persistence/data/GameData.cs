using System;
using System.Collections.Generic;
using core;
using UnityEngine;

namespace persistence.data
{
    [Serializable]
    public class GameData
    {
        // player data
        public long lastUpdated;
        public string playerName;
        public int money;
        public int playerLevel;
        public List<string> slimeIdList;

        // game settings
        public int language;
        public float volume;

        // DEBUG
        // ground
        public int behindOffset;
        public int distanceFromStart;
        public int distanceMultiplier;
        public int landObstacleMaxCount;
        public int airObstacleMaxCount;
        public int landObstacleMinCount;
        public int airObstacleMinCount;

        // item
        public int itemSpawnChance;

        // hook
        public float hookSpeed;
        public float pullingSpeed;
        public float maxLength;

        // wall
        public float wallBreakForce;

        // enemy
        public float enemyMaxSpeed;
        public float enemyMinSpeed;

        // player
        public float speedMultiplier;
        public float healthMultiplier;
        public float powerMultiplier;
        public float powerCorrectionIntoObstacle;
        public float speedDownByGround;
        public float speedDownBySize;
        public float speedDownByTime;
        public float healthDownByTime;
        public float empowerMultiplier;
        public float defensePower;

        public GameData()
        {
            playerName = "Player";
            playerLevel = 1;
            money = 0;
            slimeIdList = new List<string>();
            foreach (var slime in GameDataManager.Instance.slimeDictionary)
            {
                slimeIdList.Add(slime.Key);
            }

            // debug
            behindOffset = 100;
            distanceFromStart = 10000;
            distanceMultiplier = 100;
            landObstacleMaxCount = 50;
            airObstacleMaxCount = 100;
            landObstacleMinCount = 25;
            airObstacleMinCount = 50;
            itemSpawnChance = 50;
            hookSpeed = 10F;
            pullingSpeed = 30F;
            maxLength = 30F;
            wallBreakForce = 10F;
            enemyMaxSpeed = 20;
            enemyMinSpeed = 5;
            speedMultiplier = 3F;
            healthMultiplier = 3F;
            powerMultiplier = 3F;
            speedDownByGround = 0.1F;
            speedDownBySize = 0.1F;
            speedDownByTime = 0.1F;
            healthDownByTime = 0.1F;
            empowerMultiplier = 1F;
            defensePower = 1F;
        }

        public GameData Clone()
        {
            return (GameData)MemberwiseClone();
        }
    }
}