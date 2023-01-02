using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackageGame
{
    public class EnvironmentManager : MonoBehaviour
    {
        private Camera _main;

        private void Awake()
        {
            _main = Camera.main;
        }

        private void Start()
        {
            var facingCamera = transform.Find("FacingCamera");
            for (var i = 0; i < facingCamera.childCount; i++)
            {
                facingCamera.GetChild(i).Rotate(45,0,0);
            }

        }
    }
}
