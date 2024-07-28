using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision_Handler;

namespace Cheats {
    public class Cheats : MonoBehaviour {

        CollisionHandler collisionHandler;
        // Start is called before the first frame update
        void Start() {
            collisionHandler = GetComponent<CollisionHandler>();
        }

        // Update is called once per frame
        void Update() {
            processInput();
        }

        private void processInput() {
            if (Input.GetKeyDown(KeyCode.L)) {
                collisionHandler.loadNextLevel();
            } else if (Input.GetKeyDown(KeyCode.C)) {
                collisionHandler.collisionDisable = !collisionHandler.collisionDisable;
            } else if (Input.GetKeyDown(KeyCode.R)) {
                collisionHandler.reloadLevel();
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("Game exited");
                Application.Quit();
            }
        }
    }
}

