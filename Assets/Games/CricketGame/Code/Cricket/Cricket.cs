using System;
using System.Collections.Generic;
using Games.CricketGame.Cricket_;
using Nico.Common;
using Nico.Interface;
using UnityEngine;

namespace Games.CricketGame.Code.Cricket_
{
    public class Cricket : MonoBehaviour
    {
        [field: SerializeField] public CricketData data { get; private set; }
        [field: SerializeField] public InputHandler handler { get; private set; }
        private readonly List<ICoreComponent> _components = new();

        #region Unity LifeTime

        private void Awake()
        {
            if (handler == null)
            {
                handler = transform.GetChild(0).GetComponent<InputHandler>(); 
            }
            var controller = new Controller(this);
            _components.Add(controller);
        }

        private void Start()
        {
            foreach (var coreComponent in _components)
            {
                coreComponent.Start();
            }
        }

        private void Update()
        {
            foreach (var coreComponent in _components)
            {
                coreComponent.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var coreComponent in _components)
            {
                coreComponent.FixedUpdate();
            }
        }

        #endregion

        public void ReSetData(CricketData data)
        {
            this.data = data;
        }
        

        #region Event

        public void Dead()
        {
            print($"啊!!!{data.name}陷入危险境地了,播放战败动画,并且.....");
            data.stateEnum = StateEnum.陷入困境;

        }

        #endregion
    }
}