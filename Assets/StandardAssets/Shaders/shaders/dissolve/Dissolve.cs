using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIYShader
{
    public class Dissolve : MonoBehaviour
    {
        [SerializeField] public bool enable = false;

        private Material _material;

        private bool _is_dissolving = false;

        private float _fade = 1;
        private static readonly int _fade1 = Shader.PropertyToID("_Fade");

        // Start is called before the first frame update
        void Start()
        {
            _material = GetComponent<SpriteRenderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            if (enable)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _is_dissolving = true;
                }

                if (_is_dissolving)
                {
                    _fade -= Time.deltaTime;
                    if (_fade <= 0)
                    {
                        _fade = 0f;
                        _is_dissolving = false;
                    }
                    else
                    {
                        _material.SetFloat(_fade1, _fade);
                    }
                }
            }
        }
    }
}