using core;
using etc;
using UnityEngine;

namespace ui
{
    public class TestButton : MonoBehaviour
    {
        public void GoMain()
        {
            LoadingScreen.Instance.LoadScene(0);
        }

        private void Update()
        {
            // press y to speed up
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SpeedUp();
            }
        }

        public void SpeedUp()
        {
            GameManager.instance.ChangePlayerSpeed(30F);
            GameManager.instance.Jump();
            GameManager.instance.Jump();
            GameManager.instance.Jump();
        }
    }
}