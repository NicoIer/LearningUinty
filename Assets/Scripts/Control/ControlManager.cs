using System;
using System.Collections;
using System.Collections.Generic;
using Control;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    private CameraControl _cameraControl;
    private MoveControl _move_control;
    private AnimationControl _animationControl;
    private void Awake()
    {
        _cameraControl = GetComponent<CameraControl>();
        if (_cameraControl == null)
        {
            _cameraControl = gameObject.AddComponent<CameraControl>();
            
        }
        _move_control = GetComponent<MoveControl>();
        if (_move_control == null)
        {
            _move_control = gameObject.AddComponent<MoveControl>();
        }
        _animationControl = GetComponent<AnimationControl>();
        if (_animationControl == null)
        {
            _animationControl = gameObject.AddComponent<AnimationControl>();
        }
    }
    /// <summary>
    /// 用于初始化一个游戏物体上的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T InitComponent<T>() where T : Component
    {
        var component = GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }

        return component;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
