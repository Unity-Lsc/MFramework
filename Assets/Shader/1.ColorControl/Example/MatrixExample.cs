using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework.Example {

    public class MatrixExample : MonoBehaviour {

        private void Start() {

            Debug.Log("世界坐标:" + transform.position);

            var camera = FindObjectOfType<Camera>();
            var viewMatrix = camera.worldToCameraMatrix;

            var cameraPosition = viewMatrix.MultiplyPoint(transform.position);
            Debug.Log("摄像机坐标:" + cameraPosition);

            var projectionMatrix = camera.projectionMatrix;
            var screenPosition = projectionMatrix.MultiplyPoint(cameraPosition);
            Debug.Log("屏幕坐标:" + screenPosition);

            var screenPos = camera.previousViewProjectionMatrix.MultiplyPoint(transform.position);
            Debug.Log(screenPos);

        }

    }

}