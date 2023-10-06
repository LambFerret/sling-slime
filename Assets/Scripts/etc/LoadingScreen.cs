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
            gameObject.SetActive(true);
            var operation = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneAsyncRoutine(sceneName));
        }

        public void LoadScene(int index)
        {
            gameObject.SetActive(true);
            StartCoroutine(LoadSceneAsyncRoutine(index));
        }

        private IEnumerator LoadSceneAsyncRoutine(int index)
        {
            // Start the fade in
            _blackScreenImage.DOFade(1, 0.3f);
            yield return new WaitForSeconds(0.3f);
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
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
            yield return new WaitForSeconds(0.3f);

            gameObject.SetActive(false);
        }

        private IEnumerator LoadSceneAsyncRoutine(string sceneName)
        {
            // Start the fade in
            _blackScreenImage.DOFade(1, 0.3f);
            yield return new WaitForSeconds(0.3f);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
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
            yield return new WaitForSeconds(0.3f);

            gameObject.SetActive(false);
        }
    }
}
