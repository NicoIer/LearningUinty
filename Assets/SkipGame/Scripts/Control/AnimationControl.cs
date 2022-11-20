using System;

namespace Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AnimationControl : MonoBehaviour
    {

        [Header("粒子效果")] [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles, _landParticles;
        [Header("动画效果")]
        [SerializeField] private Animator _animator;

        [Header("Render效果")] [SerializeField] private TrailRenderer _trail_renderer;
        [SerializeField] private SpriteRenderer _renderer;

        [Header("Audio")] [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip[] _footsteps;
        //ToDo 后期取消对移动控制脚本的持有 改为其他方式
        private MoveControl _moveControl;
        //Animation中的参数 字符串
        private static readonly int _drop = Animator.StringToHash("drop");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                _animator = gameObject.AddComponent<Animator>();
            }

            _moveControl = GetComponent<MoveControl>();
            _moveParticles.Play();
            
            _renderer = GetComponent<SpriteRenderer>();
            _trail_renderer = GetComponent<TrailRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            //落地动画相关
            var drop = _moveControl.GetDropState();
            _animator.SetBool(_drop, drop);
            if (drop)
            {//落地
                _renderer.color = RandomColor();
                _landParticles.Play();
                _audioSource.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
            }
            else
            {
                _landParticles.Stop();
            }
            
            //落地的粒子效果
            var onGround = _moveControl.OnGround();
            if (onGround)
            {
                _trail_renderer.emitting = false;
                if (!_moveParticles.isPlaying)
                {//移动
                    _moveParticles.Play();
                }
            }
            else
            {
                _trail_renderer.emitting = true;
                _moveParticles.Stop();
            }
            //跳跃的粒子效果
            var jumpingThisFrame = _moveControl.JumpingThisFrame();
            if (jumpingThisFrame)
            {//跳跃
                _launchParticles.Play();
            }
            else
            {
                _launchParticles.Stop();
            }
        }

        /// <summary>
        /// 生成一个随机颜色
        /// </summary>
        /// <returns></returns>
        private static Color RandomColor()
        {
            //随机颜色的HSV值,饱和度不变，只改变H值
            //H、S、V三个值的范围都是在0~1之间
            var h = Random.Range(0f, 1f); //随机值
            const float s = 0.3f; //设置饱和度为定值
            var color = Color.HSVToRGB(h, s, 1);
            return color;
        }
    }
}