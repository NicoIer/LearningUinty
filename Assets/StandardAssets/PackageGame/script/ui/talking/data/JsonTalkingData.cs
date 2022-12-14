using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Nico.Common;
using PackageGame.Common.Manager;
using UnityEngine;

namespace PackageGame.Talking
{
    [DataContract]
    public class JsonTalkingData : TalkingData
    {
        private static readonly string _prefix_path = "./data/talking/";
        private static readonly string _meta_path = Path.Combine(_prefix_path, "meta.json");
        private static List<string> _jsonDataES; //存放对话meta数据
        private readonly int _uid;

        public static void LoadMeta()
        {
            _jsonDataES ??= JsonResourcesManager.LoadInDynamic<List<string>>(_meta_path);
        }
        public JsonTalkingData(int uid)
        {
            _uid = uid;
            Init();
        }
        public sealed override void Init()
        {
            //从meta表中找到当前对话的索引 并加载
            var data = JsonResourcesManager.LoadInDynamic<TalkingData>(Path.Combine(_prefix_path, _jsonDataES[_uid]));
            if(data==null){
                throw new NullReferenceException("未找对应Json对话文件");
            }
            CopyFrom(data);
        }
    }
}