using System;
using persistence;
using UnityEngine;

namespace sling
{
    public class TitleScene : MonoBehaviour
    {

        private void Start()
        {
            DataPersistenceManager.Instance.LoadGame();
        }
    }
}