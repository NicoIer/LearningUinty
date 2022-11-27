namespace PokemonGame.Structs
{
    using System;
    [Serializable]
    public struct PokemonInfo
    {
        //ToDo 将这里的Enum换成string替代  因为要代码和数据分离 (是否有必要,会加大界面上调试的难度么??)
        public string name;
        public string otherName;
        public uint id;
        public uint level;
        public StateEnum stateEnum;
        public CharacterEnum character;
        public PropertyEnum firstPropertyEnum;
        public PropertyEnum secondPropertyEnum;
        public Sex sex;
        public uint currentHealth;
        public ItemEnum item;
        public uint expNow;
        public uint expNeed;
        public SkillEnum skill1;
        public SkillEnum skill2;
        public SkillEnum skill3;
        public SkillEnum skill4;
        public PeculiarityEnum peculiarityEnum;
        public Trainer trainer;
        
    }
}