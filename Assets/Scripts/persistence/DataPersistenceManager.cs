using System.Collections.Generic;
using System.Linq;
using persistence.data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace persistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [SerializeField] private bool initializeDataIfNull;

        [Header("File Storage Config")] public string fileName;
        public bool useEncryption;
        private FileDataHandler _dataHandler;
        private List<IDataPersistence> _dataPersistenceObjects;

        private GameData _gameData;
        public static DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            SaveGame();
        }

        public void NewGame()
        {
            _gameData = new GameData();
        }

        public void LoadGame()
        {
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
            // load any saved data from a file using the data handler
            _gameData = _dataHandler.Load();

            // start a new game if the data is null and we're configured to initialize data for debugging purposes
            if (_gameData == null && initializeDataIfNull) NewGame();

            // if no data can be loaded, don't continue
            if (_gameData == null)
            {
                Debug.Log("No data was found. A New Game needs to be started before data can be loaded. LOAD GAME");
                return;
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
                dataPersistenceObj.LoadData(_gameData);
        }

        public void SaveGame()
        {
            if (_gameData == null)
            {
                Debug.LogWarning(
                    "No data was found. A New Game needs to be started before data can be saved. SAVE GAME");
                return;
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
                dataPersistenceObj.SaveData(_gameData);

            Debug.Log("SAVE CALLED");
            _dataHandler.Save(_gameData);
        }

        private static List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }

        public bool HasGameData()
        {
            return _gameData != null;
        }
    }
}