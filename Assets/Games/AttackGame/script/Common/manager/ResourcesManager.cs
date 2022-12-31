using System;
using System.IO;
using System.Text;
using AttackGame.Talking;
using Newtonsoft.Json;
using UnityEngine;

namespace AttackGame.Common.Manager
{
    public static class ResourcesManager
    {
        private static readonly string _persistentDataPath = Application.persistentDataPath;

        public static void PreLoadResources()
        {
            JsonTalkingData.LoadMeta();
        }
        private static string ObjToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static T JsonToObj<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T Load<T>(string path)
        {
            path = get_path(path);
            var json = File.ReadAllText(path, Encoding.UTF8);
            return JsonToObj<T>(json);
        }

        public static void Save<T>(T obj, string path)
        {
            path = get_path(path);
            string json = ObjToJson(obj);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        private static string get_path(string path)
        {
            path = Path.Combine(_persistentDataPath, path);
            var dir = Path.GetDirectoryName(path);
            if (dir == null)
                throw new NullReferenceException($"不存在的null目录!");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!File.Exists(path))
            {
                //Close必不可少
                File.Create(path).Close();
            }

            return path;
        }
    }
}