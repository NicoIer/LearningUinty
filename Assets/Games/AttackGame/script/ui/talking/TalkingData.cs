using System;
using System.Collections.Generic;
using UnityEngine;

namespace AttackGame.Data.Talking
{
    public abstract class TalkingData : ScriptableObject
    {
        [SerializeField] protected uint uid;
        protected readonly List<TalkingStruct> dataList = new();
        protected bool inited = false;

        public int Count
        {
            get
            {
                Debug.Log(inited);
                if (!inited)
                {
                    Init();
                }
        
                return dataList.Count;
            }
        }

        public virtual TalkingStruct this[int i]
        {
            get
            {
                if (!inited)
                {
                    Init();
                }

                return dataList[i];
            }
        }

        public abstract void Init();
    }
}