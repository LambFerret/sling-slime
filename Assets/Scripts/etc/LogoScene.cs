using System.Collections;
using UnityEngine;

namespace etc
{
    public class LogoScene : MonoBehaviour
    {
        private void Start()
        {
            GameObject.Find("Loading").SetActive(false);
            StartCoroutine(LoadTitleScene());
        }

        private static IEnumerator LoadTitleScene()
        {
            yield return new WaitForSeconds(1.5F);
            LoadingScreen.Instance.LoadScene("TitleScene");
        }
    }
}