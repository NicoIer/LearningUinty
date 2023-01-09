using System;
using Games.CricketGame.Cricket_;

namespace Games.CricketGame.Attack
{
    /// <summary>
    /// 战斗时做出的输入
    /// </summary>
    public class AttackInputStruct
    {
        public SelectTypeEnum typeEnum;
        public Type type;
        public object obj;

        public static T CastToType<T>(AttackInputStruct inputStruct)
        {
            return (T)inputStruct.obj;
        }

        public AttackInputStruct(SelectTypeEnum typeEnum)
        {
            if (typeEnum != SelectTypeEnum.逃跑)
            {
                throw new ArgumentException();
            }

            this.typeEnum = typeEnum;
        }

        public AttackInputStruct(SelectTypeEnum typeEnum, Type type, object obj)
        {
            this.typeEnum = typeEnum;
            this.type = type;
            this.obj = obj;
        }

        public override string ToString()
        {
            return $"操作类型:{typeEnum},class:{type},obj:{obj}";
        }
    }
}