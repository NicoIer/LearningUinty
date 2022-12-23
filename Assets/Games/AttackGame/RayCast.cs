using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackGame
{
    public class RayCast : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private int castMethodId = 0;

        private void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                switch (castMethodId)
                {
                    case 0:
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
                        {
                            //
                            Debug.Log($"hit_position:{hit.point} gameObject:{hit.collider.gameObject}");
                        }

                        break;
                    case 1:
                        if (Physics.SphereCast(ray, 0.5f, out hit, Mathf.Infinity))
                        {
                            Debug.Log($"hit_position:{hit.point} gameObject:{hit.collider.gameObject}");
                        }

                        break;
                }

                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1000f);
            }
        }
    }
}