using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using PokemonGame.Code.Structs;
using UnityEngine;

namespace PokemonGame.Code.Manager
{
    public class GameSettings : MonoBehaviour
    {
        private static string _baseDir;
        private static string _assets;
        private static string _saveDir;

        static GameSettings()
        {
            _baseDir = Environment.CurrentDirectory;
            _assets = "Assets";
            _saveDir = "Data";
        }

        private static string get_dir()
        {
            return Path.Combine(_baseDir, _assets, _saveDir);
        }

//ToDo 同时检查文件夹是否存在
        public static Dictionary<CharacterEnum, Character> load_characters(string key)
        {
            var path = Path.Combine(get_dir(), key);
            if (!CheckFileExits(path, key))
            {
                return null;
            }

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<CharacterEnum, Character>>(json);
        }

        public static bool save_characters(Dictionary<CharacterEnum, Character> characters, string key)
        {
            //是否每次游戏结束时 都要进行保存?
            var path = Path.Combine(get_dir(), key);
            CheckFileExits(path, key);

            var json = JsonConvert.SerializeObject(characters);
            File.WriteAllText(path, json, Encoding.UTF8);
            return true;
        }

        public static Dictionary<PropertyEnum, Property> load_properties(string key)
        {
            var path = Path.Combine(get_dir(), key);
            if (!CheckFileExits(path, key))
            {
                return null;
            }

            var json = File.ReadAllText(path);
            return JsonConvert
                .DeserializeObject<Dictionary<PropertyEnum, Property>>(json);
        }

        public static bool save_properties(Dictionary<PropertyEnum, Property> map,
            string key)
        {
            var path = Path.Combine(get_dir(), key);
            CheckFileExits(path, key);
            var json = JsonConvert.SerializeObject(map);
            File.WriteAllText(path, json, Encoding.UTF8);
            return true;
        }

        private static bool CheckFileExits(string path, string key)
        {
            if (File.Exists(path)) return true;
            Debug.LogWarning($"key:{key} 对应的文件不存在,将在{path}创建对应文件");
            File.Create(path).Close();
            return false;
        }
    }


    class TextEncoding
    {
        public readonly static Encoding Utf8 = Encoding.UTF8;
        public readonly static Encoding Utf8Bom = new UTF8Encoding(true);
        public readonly static Encoding Utf8NoBom = new UTF8Encoding(false);
        public readonly static byte[] Utf8BomBytes = { 0xEF, 0xBB, 0xBF };

        public static bool HasBom(byte[] bytes)
            => bytes[0] == Utf8BomBytes[0] && bytes[1] == Utf8BomBytes[1] && bytes[2] == Utf8BomBytes[2];

        public static string GetUtf8RemovedBomString(byte[] bytes)
        {
            if (HasBom(bytes))
                return Utf8.GetString(bytes, 3, bytes.Length - 3);
            return Utf8.GetString(bytes);
        }
    }
}