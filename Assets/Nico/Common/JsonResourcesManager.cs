using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using PackageGame.Talking;
using UnityEngine;

namespace Nico.Common
{
    public static class JsonResourcesManager
    {
        private static readonly string _persistentDataPath = Application.persistentDataPath;
        private static readonly string _dataPath = Application.dataPath;
        private static readonly string _streamingAssetsPath = $"{Application.dataPath}/StreamingAssets/";

        public static void PreLoadResources()
        {
            JsonTalkingData.LoadMeta();
        }

        private static string _obj_to_json(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static T json_to_obj<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SaveStreamingAssets<T>(T obj, string path, bool autoCreate)
        {
            path = Path.Combine(_streamingAssetsPath, path);
            Debug.Log($"Save At {path}");
            if (autoCreate)
            {
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
            }

            string json = _obj_to_json(obj);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        public static T LoadStreamingAssets<T>(string path)
        {
            path = Path.Combine(_streamingAssetsPath, path);
            var json = File.ReadAllText(path, Encoding.UTF8);
            var obj = json_to_obj<T>(json);
            return obj;
        }

        public static T LoadInDynamic<T>(string path, bool autoPath = true, bool autoCreate = true)
        {
            path = get_path(path, autoPath, autoCreate);
            var json = File.ReadAllText(path, Encoding.UTF8);
            var obj = json_to_obj<T>(json);
            return obj;
        }

        public static void SaveInDynamic<T>(T obj, string path, bool autoPath = true, bool autoCreate = true)
        {
            path = get_path(path, autoPath, autoCreate);
            string json = _obj_to_json(obj);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        private static string get_path(string path, bool autoPath, bool autoCreate)
        {
            if (autoPath)
            {
                path = Path.Combine(_persistentDataPath, path);
            }

            var dir = Path.GetDirectoryName(path);
            if (dir == null)
                throw new NullReferenceException($"不存在的null目录!");
            if (autoCreate)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                if (!File.Exists(path))
                {
                    //Close必不可少
                    File.Create(path).Close();
                }
            }

            return path;
        }
    }
}