using System;
using UnityEngine;

namespace Tetris
{
    /// <summary>
    /// 俄罗斯方块的砖块 一次
    /// </summary>
    public class Block : MonoBehaviour
    {
        public float fall_internal = 0.6f;
        public bool over;
        private float previous_fall_time;

        private void Start()
        {
            previous_fall_time = Time.time;
        }

        private void Update()
        {
            if (over)
            {
                return;
            }

            if (!valid())
            {
                over = true;
                return;
            }

            move_input();
        }

        private bool valid()
        {
            //是否下落到最底端
            if (transform.position.y <= -9.5)
                return false;
            else
                return true;
        }

        private void move_input()
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("rotate!!");
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= Vector3.right;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right;
            }

            if (Time.time - previous_fall_time > (Input.GetKey(KeyCode.S) ? fall_internal / 10 : fall_internal))
            {
                transform.position += Vector3.down;
                previous_fall_time = Time.time;
            }
        }
    }
}