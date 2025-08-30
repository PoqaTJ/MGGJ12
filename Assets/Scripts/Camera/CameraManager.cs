using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Camera
{
    public class CameraManager: MonoBehaviour
    {
        public UnityEngine.Camera Cam;

        private Transform cameraTransform;
        
        private float panSpeed = 0.1f;
        private float minDistance = 0.05f;

        private void Start()
        {
            SceneManager.sceneLoaded += (scene, loaded) =>
            {
                FindCamera();
            };
            FindCamera();
        }

        private void FindCamera()
        {
            Cam = UnityEngine.Camera.main;
            cameraTransform = Cam.transform;
        }

        public void ScrollTo(Vector2 pos, Action onFinish)
        {
            StartCoroutine(ScrollToCoroutine(pos, onFinish));
        }

        private IEnumerator ScrollToCoroutine(Vector2 pos, Action onFinish)
        {
            while (Vector2.Distance(pos, cameraTransform.position) > minDistance)
            {
                Vector2 newPos = Vector2.MoveTowards(cameraTransform.position, pos, panSpeed);
                cameraTransform.position = new Vector3(newPos.x, newPos.y, cameraTransform.position.z);

                yield return null;
            }
        }
    }
}