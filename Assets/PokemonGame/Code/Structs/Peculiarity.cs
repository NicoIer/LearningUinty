using System;

namespace PokemonGame
{
    public enum PeculiarityEnum
    {
        None,
        茂盛,
    }

    public class Peculiarity
    {
        #region STATIC

        public static Peculiarity FindPeculiarity(PeculiarityEnum peculiarityEnum)
        {
            switch (peculiarityEnum)
            {
                //ToDo 数据和代码分离
                case PeculiarityEnum.None:
                    return null;
                case PeculiarityEnum.茂盛:
                    return new Peculiarity(
                        "茂盛",
                        peculiarityEnum,
                        "当体力不支时,草属性招式威力提升"
                    );
                default:
                    throw new ArgumentOutOfRangeException(nameof(peculiarityEnum), peculiarityEnum, null);
            }
        }

        #endregion

        #region ATTRIBUTE

        public string name;
        public PeculiarityEnum peculiarityEnum;
        public string desc;

        #endregion

        public Peculiarity(string name, PeculiarityEnum peculiarityEnum, string desc)
        {
            this.name = name;
            this.peculiarityEnum = peculiarityEnum;
            this.desc = desc;
        }
    }
}