using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Doors
{
    public class PersistentStats : MonoBehaviour
    {
        public GameObject PlayerPrefab;

        [HideInInspector]
        public WallSide ExitSide;

        private static PersistentStats instance = null;
        public static PersistentStats Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
            transform.parent = null;

        }

        void OnEnable()
        {
            //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
            // SceneManager.sceneLoaded += OnLevelFinishedLoading;
            GameObject player = Instantiate(PlayerPrefab);
            player.name = "Player";
        }
    }
}
