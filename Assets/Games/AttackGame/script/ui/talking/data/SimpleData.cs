using System.Collections.Generic;
using UnityEngine;

namespace AttackGame.Talking
{
    /// <summary>
    /// 你一句我一句 的 简单对话数据存储 
    /// </summary>
    [CreateAssetMenu(fileName = "SimpleData", menuName = "Data/Talking", order = 0)]
    public class SimpleData : TalkingData
    {
        public Sprite right_image;
        public Sprite left_image;
        public string right_person_name;
        public string left_person_name;
        public List<string> text_list;

        public override void Init()
        {
            if (text_list.Count == dataList.Count)
            {
                inited = true;
                return;
            }
            for (var i = 0; i < text_list.Count; i++)
            {
                var data = new TalkingStruct
                {
                    right_person_name = right_person_name,
                    left_person_name = left_person_name,
                    text = text_list[i],
                    background_image = i % 2 == 0 ? right_image : left_image
                };
                
                dataList.Add(data);
            }

            inited = true;
        }
    }
}