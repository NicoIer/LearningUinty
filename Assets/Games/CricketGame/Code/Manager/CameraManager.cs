using System;
using Cinemachine;
using UnityEngine;

namespace Games.CricketGame.Manager
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance;
        public Camera mainCamera;
        public CinemachineVirtualCamera attackCamera;
        public CinemachineVirtualCamera playerCamera;
        public CinemachineBrain cinemachineBrain;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }


        public void SwitchToPlayerCamera(bool animation)
        {
            // cinemachineBrain.IsBlending;

            _change_bland(animation);
            attackCamera.gameObject.SetActive(false);
            mainCamera.orthographic = true;
            playerCamera.gameObject.SetActive(true);
        }

        public void SwitchToAttackCamera(bool animation)
        {
            
            // cinemachineBrain.IsBlending;
            _change_bland(animation);
            playerCamera.gameObject.SetActive(false);
            attackCamera.gameObject.SetActive(true);
            mainCamera.orthographic = false;
        }

        private void _change_bland(bool cut)
        {
            if (!cut)
            {
                cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
            }
            else
            {
                cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            }
        }
        public void SwitchCamera()
        {
        }
    }
}