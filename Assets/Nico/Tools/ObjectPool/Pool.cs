using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Tools.ObjectPool
{
    public class Pool
    {
        private string _pool_name;
        public Transform root; //为了方便管理和查看 将所有池中的对象放到root物体下
        private GameObject _prefab; //为生成对象保留的一个GameObject
        private uint _capacity; //最大容量
        public uint Count; //当前容量
        private Stack<PoolObject> _objects = new(); //存放对象的栈

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pool(string name, Transform root, GameObject prefab, uint capacity, uint start_size = 1)
        {
            this.root = root;
            _pool_name = name;
            _prefab = prefab;
            _capacity = capacity;

            if (start_size > 0)
            {
                Count = start_size;
                for (var i = 0; i < start_size; i++)
                {
                    AddObject(CreateObject());
                }
            }
            else
            {
                Count = 0;
            }
        }

        private PoolObject CreateObject()
        {
            GameObject go = GameObject.Instantiate(_prefab);
            var po = go.GetComponent<PoolObject>();

            if (po == null)
            {
                po = go.AddComponent<PoolObject>();
            }

            po.poolName = _pool_name;
            po.pooled = true;

            return po;
        }

        /// <summary>
        /// 向Pool中添加一个物体 并存放到设定的root下
        /// </summary>
        /// <param name="poolObject"></param>
        private void AddObject(PoolObject poolObject)
        {
            poolObject.transform.SetParent(root);
            poolObject.gameObject.SetActive(false);
            _objects.Push(poolObject);
            poolObject.pooled = true;
        }

        public PoolObject GetObject(Vector3? position = null, Quaternion? quaternion = null)
        {
            _objects.TryPop(out var obj);
            if (obj == null)
            {
                Count++;
                obj = CreateObject();
            }

            obj.pooled = false;
            var result = obj.gameObject;
            if (position != null)
            {
                result.transform.position = (Vector3)position;
            }

            if (quaternion != null)
            {
                result.transform.rotation = (Quaternion)quaternion;
            }


            result.SetActive(true);

            return obj;
        }

        public bool ReturnObject(PoolObject poolObject)
        {
            if (_pool_name.Equals(poolObject.poolName))
            {
                if (poolObject.pooled)
                {
                    Debug.LogWarning($"{poolObject} already in pool:{_pool_name}");
                    return false;
                }

                AddObject(poolObject);
                return true;
            }
            else
            {
                Debug.LogWarning($"{poolObject} not belong to pool:{_pool_name}");
                return false;
            }
        }
    }
}