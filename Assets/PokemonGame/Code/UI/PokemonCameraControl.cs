using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PokemonGame.Code.UI
{
    public class PokemonCameraControl : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public bool viewing = false;

        private void Awake()
        {
            if (_camera == null)
            {
                _camera = transform.Find("camera").GetComponent<Camera>();
            }
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {//ToDo 将这里的内容更新到 UIManager
                //点击后
                GameObject obj = GetFirstPickGameObject(Input.mousePosition);
                if (obj == gameObject)
                {
                    viewing = true;
                }
            }else if (Input.GetMouseButtonUp(0))
            {
                GameObject obj = GetFirstPickGameObject(Input.mousePosition);
                if (obj == gameObject)
                {
                    viewing = false;
                }
            }

            if (viewing)
            {
                var q = Quaternion.identity;
                q.SetLookRotation(_camera.transform.forward,_camera.transform.up);
                var h = Input.GetAxis("Mouse X");
                var v = Input.GetAxis("Mouse Y");
                var forward = Quaternion.Euler(-v, h, 0) * _camera.transform.forward;
                _camera.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

            }
        }

        /// <summary>
        /// 点击屏幕坐标
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject GetFirstPickGameObject(Vector2 position)
        {
            EventSystem eventSystem = EventSystem.current;
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;
            //射线检测ui
            List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, uiRaycastResultCache);
            if (uiRaycastResultCache.Count > 0)
                return uiRaycastResultCache[0].gameObject;
            return null;
        }
    }
}