
namespace Control
{
    using UnityEngine;
    using System;
    public struct UserInput {
        public float X,Y;
        public bool JumpDown;//按下跳跃键
        public bool JumpUp;//松开跳跃键
        public override string ToString()
        {
            return $"X:{X} Y:{Y} JumpDown:{JumpDown} JumpUp:{JumpUp}";
        }
    }
    [Serializable]
    public struct CollisionDirection
    {
        public bool fromRight, fromLeft, fromUp, fromDown;
        public Vector3 normal;
        public override string ToString()
        {
            //ToDo 优化显示
            return $"fromRight:{fromRight},fromLeft:{fromLeft},fromUp:{fromUp},fromDown:{fromDown}";
        }

        public void Clear()
        {
            fromRight = fromLeft = fromUp = fromDown = false;
            normal = new Vector3(0, 0, 0);
        }
    }
    
}