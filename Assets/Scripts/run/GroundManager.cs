using System;
using components;
using core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace run
{
    public class GroundManager : MonoBehaviour
    {
        public float behindOffset;
        public ObjectPooler groundPool;
        public ObjectPooler landObstaclePool;
        public ObjectPooler airObstaclePool;
        public ObjectPooler itemPool;

        [Header("Distance")] public int distanceFromStart;
        public int currentDistance;
        public int distanceMultiplier;

        [Header("Obstacle Spawn Probability")] public int landObstacleMaxCount;
        public int airObstacleMaxCount;
        public int landObstacleMinCount;
        public int airObstacleMinCount;

        [Header("Spawn Points")] public Transform landSpawnPoint;
        public Transform airSpawnPointMin;
        public Transform airSpawnPointMax;

        [Header("Event")] public GameEvent onProgressDistanceUpdated;


        private float _groundSpeed;
        private int _groundLastIndex;

        private void Awake()
        {
            airObstaclePool.pooledAmount = airObstacleMaxCount;
            landObstaclePool.pooledAmount = landObstacleMaxCount;
            distanceMultiplier = 100;
            distanceFromStart *= distanceMultiplier;
        }

        private void Start()
        {
            InitGround();
        }

        private void Update()
        {
            // if player is moving, move grounds
            foreach (GameObject ground in groundPool.pooledObjects)
            {
                ground.transform.position += new Vector3(-GameManager.instance.playerSpeed * Time.deltaTime, 0, 0);

                UpdateGround(ground);
            }

            currentDistance += (int)GameManager.instance.playerSpeed;
            onProgressDistanceUpdated.Raise(this, currentDistance / (float)distanceFromStart);

            var activatedChildCount = 0;
            foreach (GameObject ob in landObstaclePool.pooledObjects)
            {
                if (ob is null) continue;
                if (CheckActivation(ob)) activatedChildCount++;
            }

            TrySpawnLandObstacle(activatedChildCount);

            activatedChildCount = 0;
            foreach (GameObject ob in airObstaclePool.pooledObjects)
            {
                if (CheckActivation(ob)) activatedChildCount++;
            }

            TrySpawnAirObstacle(activatedChildCount);
        }


        private void TrySpawnLandObstacle(int currentObstacle)
        {
            if (currentObstacle < landObstacleMinCount)
            {
                SpawnLandObstacle();
            }
            else if (currentObstacle > landObstacleMaxCount)
            {
                SpawnLandObstacle(1 - (currentObstacle - landObstacleMaxCount) /
                    (float)(landObstacleMaxCount - landObstacleMinCount));
            }
        }

        private void SpawnLandObstacle(float probability = 1F)
        {
            if (Random.Range(0F, 1F) > probability) return;
            var obstacle = landObstaclePool.GetPooledObject(groundPool.pooledObjects[_groundLastIndex]);
            var position = obstacle.transform.position;
            var virtualCamera = GameManager.instance.virtualCamera;
            Vector3 finalCameraPosition = virtualCamera.State.FinalPosition;
            float aspectRatio = Screen.width / (float)Screen.height;
            float worldScreenWidth = 2 * virtualCamera.m_Lens.OrthographicSize * aspectRatio;

            float guaranteedRightPositionX =
                finalCameraPosition.x + worldScreenWidth / 2 + Random.Range(1, behindOffset);

            position = new Vector3(guaranteedRightPositionX, landSpawnPoint.position.y, position.z);
            obstacle.transform.position = position;
            obstacle.SetActive(true);
        }

        private void TrySpawnAirObstacle(int currentObstacle)
        {
            if (currentObstacle < airObstacleMinCount)
            {
                SpawnAirObstacle();
            }
            else if (currentObstacle > airObstacleMaxCount)
            {
                SpawnAirObstacle(1 - (currentObstacle - airObstacleMaxCount) /
                    (float)(airObstacleMaxCount - airObstacleMinCount));
            }
        }

        private void SpawnAirObstacle(float probability = 1F)
        {
            if (Random.Range(0F, 1F) > probability) return;
            var obstacle = airObstaclePool.GetPooledObject(groundPool.pooledObjects[_groundLastIndex]);
            var position = obstacle.transform.position;
            var virtualCamera = GameManager.instance.virtualCamera;
            Vector3 finalCameraPosition = virtualCamera.State.FinalPosition;
            float aspectRatio = Screen.width / (float)Screen.height;
            float worldScreenWidth = 2 * virtualCamera.m_Lens.OrthographicSize * aspectRatio;

            float guaranteedRightPositionX =
                finalCameraPosition.x + worldScreenWidth / 2 + Random.Range(1, behindOffset);

            float randomPositionY = Random.Range(airSpawnPointMin.position.y, airSpawnPointMax.position.y);
            position = new Vector3(guaranteedRightPositionX, randomPositionY, position.z);
            obstacle.transform.position = position;
            obstacle.SetActive(true);
        }

        private void InitGround()
        {
            Vector3 floor = Vector3.zero;
            // make grounds line up in order by their floor object and spawn them
            foreach (GameObject ground in groundPool.pooledObjects)
            {
                ground.transform.position = floor;
                floor += new Vector3(ground.transform.Find("floor").GetComponent<SpriteRenderer>().bounds.size.x,
                    floor.y, floor.z);
                ground.SetActive(true);
            }

            _groundLastIndex = groundPool.pooledObjects.Count - 1;
        }


        private void UpdateGround(GameObject ground)
        {
            var groundPositionOfRightEnd = ground.transform.position.x +
                                           ground.transform.Find("floor").GetComponent<SpriteRenderer>().bounds
                                               .size.x / 2;
            // if ground is out of screen, move it to the end of the line
            if (groundPositionOfRightEnd < GameManager.instance.player.transform.position.x - behindOffset)
            {
                ground.SetActive(false);

                SpawnGround(groundPool.pooledObjects[_groundLastIndex].transform.position.x +
                            groundPool.pooledObjects[_groundLastIndex].transform.Find("floor")
                                .GetComponent<SpriteRenderer>().bounds.size.x);

                _groundLastIndex = (_groundLastIndex + 1) % groundPool.pooledObjects.Count;
            }
        }

        private bool CheckActivation(GameObject obj)
        {
            if (obj.transform.position.x < GameManager.instance.player.transform.position.x - behindOffset)
            {
                obj.SetActive(false);
            }

            return obj.activeInHierarchy;
        }

        private void SpawnGround(float xPos)
        {
            GameObject newGround = groundPool.GetPooledObject();
            newGround.transform.position = new Vector3(xPos, 0, 0);
            newGround.SetActive(true);
        }
    }
}