using UnityEngine;

namespace Nico.Common
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // 单例实例
        private static T _instance;

        // 获取单例实例的属性
        public static T instance
        {
            get
            {
                // 如果实例还没有被创建，则在场景中查找对应的组件
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                // 如果还是没有找到，则在场景中创建新的实例
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    _instance = go.AddComponent<T>();
                }

                return _instance;
            }
        }

        // 在单例脚本被加载时保证只有一个实例存在
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}