using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace NetGame
{
    public class CudePlayer : NetworkBehaviour
    {
        private NetworkVariable<PlayerMovementData> _position = new(writePerm:NetworkVariableWritePermission.Owner);
        private void Update()
        {
            if (IsOwner)
            {//当前实例是否由本地控制
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");
                transform.Translate(Time.deltaTime*vertical*transform.forward);
        
                transform.Translate(Time.deltaTime*horizontal*transform.right);
                _position.Value = new PlayerMovementData
                {
                    Position = transform.position
                };
                
            }
            else
            {
                transform.position = _position.Value.Position;
            }

        }
    }

    internal struct PlayerMovementData:INetworkSerializable
    {
        private float _x, _z;
        public Vector3 Position
        {
            get => new Vector3(_x, 0, _z);
            set
            {
                _x = value.x;
                _z = value.z;
            }
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _z);
        }
    }
}
