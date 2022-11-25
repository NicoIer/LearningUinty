using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private Camera _main;
    [SerializeField] private bool viewingMode;
    private void Awake()
    {
     _main = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        var q = Quaternion.identity;
        q.SetLookRotation(_main.transform.forward,_main.transform.up);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.rotation = q;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            viewingMode = true;
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            viewingMode = false;
        }
        
        if (viewingMode)
        {//观察模式
            var h = Input.GetAxis("Mouse X");
            var v = Input.GetAxis("Mouse Y");
            var forward = Quaternion.Euler(-v, h, 0) * _main.transform.forward;
            _main.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
    }
}
