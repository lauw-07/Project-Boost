using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using UnityEngine;

namespace Movement { 
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] public float mainThrust = 100;
        [SerializeField] public float rotationThrust = 100;
        [SerializeField] AudioClip flyingSound;

        [SerializeField] ParticleSystem mainBoosterEffect;
        [SerializeField] ParticleSystem leftBoosterEffect;
        [SerializeField] ParticleSystem rightBoosterEffect;

        Rigidbody rb;
        AudioSource audioSource;

        // Start is called before the first frame update
        void Start() {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update() {
            processThrust();
            processRotation();
        }

        private void processThrust() {
            if (Input.GetKey(KeyCode.Space)) {
                startThrusting();
            } else {
                stopThrusting();
            }
        }

        private void processRotation() {
            if (Input.GetKey(KeyCode.A)) {
                startRotating(rightBoosterEffect, 1);

            } else if (Input.GetKey(KeyCode.D)) {
                startRotating(leftBoosterEffect, -1);

            } else {
                stopRotating();
            }
        }

        private void startThrusting() {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(flyingSound);
            }
            if (!mainBoosterEffect.isPlaying) {
                mainBoosterEffect.Play();
            }
        }

        private void stopThrusting() {
            audioSource.Stop();
            mainBoosterEffect.Stop();
        }

        private void rotate(float rotationThisFrame) {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

            rb.freezeRotation = false;
        }

        private void startRotating(ParticleSystem particleSystem, int dir) {
            rotate(dir * rotationThrust);

            if (!particleSystem.isPlaying) {
                particleSystem.Play();
            }
        }

        private void stopRotating() {
            rightBoosterEffect.Stop();
            leftBoosterEffect.Stop();
        }
    }
}
