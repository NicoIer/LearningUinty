using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PokemonGame.Code.UI
{
    public class PokemonCameraControl : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private Vector3 _BeginCameraPosition;
        [SerializeField] private NicoButton _button;
        private GameObject pokemon_model;
        private void Awake()
        {
            if (_camera == null)
            {
                _camera = transform.Find("camera").GetComponent<Camera>();
                _BeginCameraPosition = _camera.transform.position;
            }

            if (_button == null)
            {
                _button = GetComponent<NicoButton>();
            }

        }

        private void Update()
        {
            
            if (_button.longPressed)
            {
                var q = Quaternion.identity;
                q.SetLookRotation(_camera.transform.forward,_camera.transform.up);
                var h = Input.GetAxis("Mouse X");
                var v = Input.GetAxis("Mouse Y");
                var forward = Quaternion.Euler(-v, h, 0) * _camera.transform.forward;
                _camera.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            }
        }

        public void set_pokemon_model(GameObject pokemonModel)
        {
            _camera.transform.position = _BeginCameraPosition;
            pokemon_model = pokemonModel;
        }


    }
}