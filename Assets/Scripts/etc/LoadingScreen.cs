using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace etc
{
    public class LoadingScreen : MonoBehaviour
    {
        public TextMeshProUGUI progressText;
        private Image _blackScreenImage;
        private GameObject _icon;
        public static LoadingScreen Instance { get; private set; }

        private void Awake()
        {
            _blackScreenImage = transform.Find("BlackScreen").GetComponent<Image>();
            _icon = transform.Find("BlackScreen").Find("Icon").gameObject;
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadScene(string sceneName)
        {
            LoadSceneInternal(SceneManager.LoadSceneAsync(sceneName));
        }

        public void LoadScene(int index)
        {
            LoadSceneInternal(SceneManager.LoadSceneAsync(index));
        }

        private void LoadSceneInternal(AsyncOperation operation)
        {
            gameObject.SetActive(true);
            StartCoroutine(LoadSceneAsyncRoutine(operation));
        }

        private IEnumerator LoadSceneAsyncRoutine(AsyncOperation operation)
        {
            // Start the fade in
            _blackScreenImage.DOFade(1, 0.3f);
            yield return new WaitForSeconds(0.3f);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f) * 100f;
                string dot = string.Concat(Enumerable.Repeat(".", (int)(progress % 4)));
                progressText.text = "LOADING" + dot + " " + progress + "%";
                yield return null;
            }

            // Start the fade out
            _icon.SetActive(false);
            _blackScreenImage.DOFade(0, 1f);
            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);
        }
    }
}
