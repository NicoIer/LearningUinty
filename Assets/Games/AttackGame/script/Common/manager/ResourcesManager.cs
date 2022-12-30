using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace AttackGame.Common.Manager
{
    //ToDo 使用它 读取/存储 信息
    public static class ResourcesManager
    {
        public static string persistentDataPath = Application.persistentDataPath;

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

        public static void Save(object obj, string path)
        {
            path = get_path(path);
            Debug.Log(path);
            string json = ObjToJson(obj);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        private static string get_path(string path)
        {
            path = Path.Combine(persistentDataPath, path);
            var dir = Path.GetDirectoryName(path);
            if (dir == null)
                throw new NullReferenceException($"不存在的null目录!");
            
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            return path;
        }
    }
}