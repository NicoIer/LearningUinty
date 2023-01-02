using System;
using PackageGame.Talking;
using UnityEngine;

namespace PackageGame.NPC
{
    [Serializable]
    public class NpcData
    {
        public string name;
        public Sprite sprite;
        public int talkingID = -1;

        private TalkingData _talking_data;
        public TalkingData talkingData
        {
            get
            {
                if (talkingID != -1)
                    return _talking_data ??= new JsonTalkingData(talkingID);
                return null;
            }
        }

        public float health;
    }
}