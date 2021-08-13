﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraController {
    public class Map : MonoBehaviour {
        public static Map Instance;

        public Vector3 Center = new Vector3(0, 0, 0);

        private void Awake() {

            // If Instance is not null (any time after the first time)
            // AND
            // If Instance is not 'this' (after the first time)
            if (Instance != null && Instance != this) {
                // ...then destroy the game object this script component is attached to.
                Destroy(gameObject);
            }
            else {
                // Tell Unity not to destory the GameObject this
                //  is attached to between scenes.
                //DontDestroyOnLoad(gameObject);
                // Save an internal reference to the first instance of this class
                Instance = this;
            }

        }
    }
}