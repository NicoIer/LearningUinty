using System;
using Cinemachine;
using UnityEngine;

namespace Nico.Scene
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;
        [field: SerializeField]public Camera mainCamera;
        [field: SerializeField]public CinemachineVirtualCamera playerCamera;
        [field: SerializeField]public CinemachineVirtualCamera attackCamera;


        private void Awake()
        {//简单单例 之后再修改
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void ToPlayer()
        {
            attackCamera.enabled = false;
            playerCamera.enabled = true;
            mainCamera.orthographic = true;
        }

        public void ToAttack()
        {
            playerCamera.enabled = false;
            attackCamera.enabled = true;
            mainCamera.orthographic=false;
        }
    }
}