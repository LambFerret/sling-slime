using Cinemachine;
using components;
using UnityEngine;

namespace run
{
    public class GroundController : MonoBehaviour
    {
        public ObjectPooler groundPool;
        public ObjectPooler flyingCoinPool;
        public ObjectPooler groundCoinPool;
        private float _groundWidth;
        public PlayerBehavior player;
        private Camera _cam;
        private float lastGroundX;
        private Transform _floor;

        [Header("Camera")] public CinemachineVirtualCamera virtualCamera;
        public float minZoom = 1f;
        public float maxZoom = 5f;
        public float zoomFactor = 1f; // 속도에 따른 줌 인/아웃 비율
        public float screenYSpeedAdjustment = 0.1f; // 이 값을 조절하여 ScreenY 변화 속도 조절 가능
        private Vector3 playerPosition; // Store the player's position here
        private float currentZoom;
        private float targetZoom;


        private void Start()
        {
            currentZoom = virtualCamera.m_Lens.OrthographicSize;

            // Initialize the ground position
            _floor = groundPool.pooledObject.transform.Find("floor");
            _groundWidth = _floor.GetComponent<BoxCollider2D>().size.x * _floor.transform.localScale.x;
            lastGroundX = transform.position.x;
            _cam = Camera.main;
        }

        private void Update()
        {
            playerPosition = player.rb.transform.position;

            // Check for the player's position and instantiate new ground tiles accordingly
            if (playerPosition.x >= lastGroundX - _groundWidth)
            {
                CreateGround();
            }

            ReturnOldGround();

            if (virtualCamera.LookAt is not null)
            {
                float verticalSpeed = player.rb.velocity.y;

                // Calculate the desired zoom based on speed and zoomFactor
                targetZoom = Mathf.Clamp(currentZoom + -verticalSpeed * zoomFactor, minZoom, maxZoom);

                // Use Lerp to smoothly transition from current to target zoom. The 0.1f is the smoothing factor.
                currentZoom = Mathf.Lerp(currentZoom, targetZoom, 0.01f);

                virtualCamera.m_Lens.OrthographicSize = currentZoom;
            }
        }

        private void CreateGround()
        {
            GameObject ground = groundPool.GetPooledObject();
            ground.transform.position = new Vector3(lastGroundX + _groundWidth, 0, 0);
            ground.SetActive(true);

            var lastGround = ground.transform.position;

            // Spawn 5 to 10 coins at random positions on the ground
            int numberOfCoins = Random.Range(5, 11); // Random between 5 and 10, 11 is exclusive
            for (int i = 0; i < numberOfCoins; i++)
            {
                GameObject coin = flyingCoinPool.GetPooledObject();
                float randomX = Random.Range(lastGround.x - _groundWidth / 2, lastGround.x + _groundWidth / 2);
                float randomY = Random.Range(lastGround.y + 1f, lastGround.y + 3f);
                coin.transform.position = new Vector3(randomX, randomY, 0);

                coin.SetActive(true);
            }

            int numberOfGroundCoin = Random.Range(1, 3);
            for (int i = 0; i < numberOfGroundCoin; i++)
            {
                GameObject coin = groundCoinPool.GetPooledObject();
                float randomX = Random.Range(lastGround.x - _groundWidth / 2,
                    lastGround.x + _groundWidth / 2);
                coin.transform.position = new Vector3(randomX, _floor.transform.position.y + 0.5F, lastGround.z);
                coin.SetActive(true);
            }

            lastGroundX = lastGround.x;
        }

        private void ReturnOldGround()
        {
            foreach (GameObject ground in groundPool.pooledObjects)
            {
                if (ground.activeInHierarchy && ground.transform.position.x < playerPosition.x - 2 * _groundWidth)
                {
                    ground.SetActive(false);
                }
            }

            foreach (GameObject flyingCoin in flyingCoinPool.pooledObjects)
            {
                if (flyingCoin.activeInHierarchy &&
                    flyingCoin.transform.position.x < playerPosition.x - 2 * _groundWidth)
                {
                    flyingCoin.SetActive(false);
                }
            }

            foreach (GameObject groundCoin in groundCoinPool.pooledObjects)
            {
                if (groundCoin.activeInHierarchy &&
                    groundCoin.transform.position.x < playerPosition.x - 2 * _groundWidth)
                {
                    groundCoin.SetActive(false);
                }
            }
        }
    }
}