using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Collision_Handler {
    public class CollisionHandler : MonoBehaviour {

        [SerializeField] public float levelLoadDelay = 2f;
        [SerializeField] AudioClip collisionSound;
        [SerializeField] AudioClip successSound;

        [SerializeField] ParticleSystem collisionParticlesEffect;
        [SerializeField] ParticleSystem successParticlesEffect;

        AudioSource audioSource;
        PlayerMovement movement;

        private bool isTransitioning = false;
        public bool collisionDisable = false;

        private void Start() {
            audioSource = GetComponent<AudioSource>();
            movement = GetComponent<PlayerMovement>();
        }

        private void OnCollisionEnter(Collision collision) {

            if (isTransitioning || collisionDisable) return;

            switch (collision.gameObject.tag) {
                case "Friendly":
                    Debug.Log("This is friendly"); break;
                case "Fuel":
                    Debug.Log("You have picked up fuel"); break;
                case "Finish":
                    startSuccessSequence();
                    break;
                default:
                    startCrashSequence();
                    break;
            }
        }

        private void startCrashSequence() {
            isTransitioning = true;
            audioSource.Stop();

            audioSource.PlayOneShot(collisionSound);
            movement.enabled = false;

            collisionParticlesEffect.Play();

            Invoke("reloadLevel", levelLoadDelay);
        }

        private void startSuccessSequence() {
            isTransitioning = true;
            audioSource.Stop();

            audioSource.PlayOneShot(successSound);
            movement.enabled = false;

            successParticlesEffect.Play();

            Invoke("loadNextLevel", levelLoadDelay);

        }

        public void loadNextLevel() {
            int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = curSceneIndex + 1;

            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
                nextSceneIndex = 0;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }

        public void reloadLevel() {
            int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(curSceneIndex);
        }
    }
}

