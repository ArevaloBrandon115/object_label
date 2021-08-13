using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraController {
    public class CameraController : MonoBehaviour {
        [SerializeField] private Camera movingCamera;
        [SerializeField] private float ZoomSpeed = 3;
        [SerializeField] private float MinZoom = 3.0f;
        [SerializeField] private float MaxZoom = 20.0f;

        [SerializeField] private float PanSpeed = 10;

        //option one
        //public Vector2 MinXPan = new Vector2(-300, -50);
        //public Vector2 MinYPan = new Vector2(-50, 150);

        [SerializeField] private Vector2 MinXPan = new Vector2(-100, 100);
        [SerializeField] private Vector2 MinYPan = new Vector2(-100, 100);

        [SerializeField] private GameObject cameraContainer;

        private Vector3 newPosition;
        private Vector3 dragStartPosition;
        private Vector3 dragCurrentPosition;

        private Plane _Plane;
        void Start() {
            //plane to check center
            _Plane = new Plane(Vector3.up, Vector3.zero);

            //center of map
            Vector3 mapCenter = Map.Instance.Center;
            //look at center
            transform.LookAt(mapCenter);

        }

        void Update() {
            HandleZoomAndRotation();
            HandlePan();
        }

        //handle panning on map
        private void HandlePan() {

            //when pressed
            if (Input.GetMouseButtonDown(0)) {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = movingCamera.ScreenPointToRay(Input.mousePosition);

                float entry;
                if (plane.Raycast(ray, out entry)) {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }
            // when held down
            if (Input.GetMouseButton(0)) {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = movingCamera.ScreenPointToRay(Input.mousePosition);

                float entry;
                if (plane.Raycast(ray, out entry)) {
                    dragCurrentPosition = ray.GetPoint(entry);

                    //setting new position of movement
                    newPosition = cameraContainer.transform.position + dragStartPosition - dragCurrentPosition;

                    //clamps to our limits
                    //option one but no rotation
                    //newPosition.x = Mathf.Clamp(newPosition.x, MinXPan.x, MinXPan.y);
                    //newPosition.z = Mathf.Clamp(newPosition.z, MinYPan.x, MinYPan.y);

                    //with camera container
                    newPosition.x = Mathf.Clamp(newPosition.x, MinXPan.x, MinXPan.y);
                    newPosition.z = Mathf.Clamp(newPosition.z, MinYPan.x, MinYPan.y);

                    //update position
                    cameraContainer.transform.position = Vector3.Lerp(cameraContainer.transform.position, newPosition, PanSpeed * Time.deltaTime);
                    Map.Instance.Center = GetCenter();
                }
            }
        }

        //handle zoom and rataionn when holding ctrl
        private void HandleZoomAndRotation() {
            //scroll value
            float scrollValue = Input.mouseScrollDelta.y * ZoomSpeed;

            if (scrollValue != 0.0) {
                // windows
                if (Input.GetKey(KeyCode.LeftControl)) {
                    //Rotation
                    Vector3 center = GetCenter();
                    //from center to camera
                    Vector3 centerDirection = transform.position - center;
                    //get scroll angle
                    Vector3 angles = new Vector3(0, scrollValue, 0);
                    //create new quaternion 
                    Quaternion newRotation = Quaternion.Euler(angles);
                    //new direction
                    Vector3 newDirection = newRotation * centerDirection;
                    //apply rotation and look at center
                    transform.position = center + newDirection;
                    transform.LookAt(center);
                    //transform.forward = -newDirection;
                }
                else {
                    //control projection size and clamp the zoom
                    float newSize = movingCamera.orthographicSize - scrollValue;
                    movingCamera.orthographicSize = Mathf.Clamp(newSize, MinZoom, MaxZoom);
                }
            }
        }

        //find new center based on camera
        private Vector3 GetCenter() {
            Ray ray = new Ray(transform.position, transform.forward);

            float distance = 0.0f;

            if (_Plane.Raycast(ray, out distance)) {
                return ray.GetPoint(distance);
            }

            return Vector3.zero;
        }
    }
}