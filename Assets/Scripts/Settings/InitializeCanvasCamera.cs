using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBOB
{
    public class InitializeCanvasCamera : MonoBehaviour
    {
        public SO_GameObject soCamera;
        public Canvas canvas;

        public float offsetPlaneDistance = 0.04f;

        private void Start()
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = soCamera.gameObject.GetComponent<Camera>();
            canvas.planeDistance = soCamera.gameObject.GetComponent<Camera>().nearClipPlane + offsetPlaneDistance;
        }
    }
}
