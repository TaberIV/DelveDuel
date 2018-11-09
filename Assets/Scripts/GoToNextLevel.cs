using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Doors
{
    public class GoToNextLevel : TriggerBehavior
    {
        public PersistentStats perStats;

        private void Start()
        {
            perStats = GameObject.Find("PersistentStats").GetComponent<PersistentStats>();
        }

        public override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                // Go to the next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                perStats.ExitSide = GetComponent<DoorInfoContainer>().wall;
            }
        }
    }
}
