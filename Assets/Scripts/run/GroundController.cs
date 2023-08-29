using Cinemachine;
using components;
using UnityEngine;

namespace run
{
    public class GroundController : MonoBehaviour
    {
        public ObjectPooler groundPool;
        public float groundWidth;
        public PlayerBehavior player;
        private Camera _cam;
        private float lastGroundX;

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
            lastGroundX = transform.position.x;
            _cam = Camera.main;
        }

        private void Update()
        {

            playerPosition = player.rb.transform.position;

            // Check for the player's position and instantiate new ground tiles accordingly
            if (playerPosition.x >= lastGroundX - groundWidth)
            {
                CreateGround();
            }

            if (virtualCamera.LookAt is not null)
            {
                float verticalSpeed = player.rb.velocity.y;

                // Calculate the desired zoom based on speed and zoomFactor
                targetZoom = Mathf.Clamp(currentZoom + -verticalSpeed * zoomFactor, minZoom, maxZoom);

                // Use Lerp to smoothly transition from current to target zoom. The 0.1f is the smoothing factor.
                currentZoom = Mathf.Lerp(currentZoom, targetZoom, 0.01f);

                virtualCamera.m_Lens.OrthographicSize = currentZoom;
            }


            ReturnOldGround();
        }

        private void CreateGround()
        {
            GameObject ground = groundPool.GetPooledObject();
            ground.transform.position = new Vector3(lastGroundX + groundWidth, ground.transform.position.y,
                ground.transform.position.z);
            ground.SetActive(true);

            // Update lastGroundX
            lastGroundX = ground.transform.position.x;
        }

        private void ReturnOldGround()
        {
            // Loop through all the pooled ground objects
            foreach (GameObject ground in groundPool.pooledObjects)
            {
                // Deactivate the ground if it's too far to the left of the player
                if (ground.activeInHierarchy && ground.transform.position.x < playerPosition.x - 2 * groundWidth)
                {
                    ground.SetActive(false);
                }
            }
        }
    }
}