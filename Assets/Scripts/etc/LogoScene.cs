using System.Collections;
using UnityEngine;

namespace etc
{
    public class LogoScene : MonoBehaviour
    {

        public GameObject loadingScreen;

        private void Start()
        {
            loadingScreen = GameObject.Find("Loading");
            loadingScreen.SetActive(false);
            StartCoroutine(LoadTitleScene());
        }

        private IEnumerator LoadTitleScene()
        {
            yield return new WaitForSeconds(1.5F);
            loadingScreen.SetActive(true);
            LoadingScreen.Instance.LoadScene("TitleScene");
        }
    }
}