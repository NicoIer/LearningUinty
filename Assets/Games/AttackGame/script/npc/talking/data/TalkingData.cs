using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace AttackGame.Talking
{
    [Serializable]
    public class TalkingStruct
    {
        public TalkingStruct(string personName, string text)
        {
            this.personName = personName;
            this.text = text;
        }

        public int styleIdx;
        public string personName;
        public string text;
    }
    [DataContract]
    public class TalkingData
    {
        [DataMember]protected List<TalkingStruct> dataList = new();
        private bool _initialized;

        protected void CopyFrom(TalkingData data)
        {
            dataList = data.dataList;
            _initialized = data._initialized;
        }

        public int Count
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }

                return dataList.Count;
            }
        }

        public TalkingStruct this[int i]
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }

                return dataList[i];
            }
        }

        public virtual void Init()
        {
        }
    }
}