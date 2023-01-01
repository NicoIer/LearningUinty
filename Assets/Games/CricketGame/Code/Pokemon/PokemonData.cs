using System;

namespace Games.CricketGame.Code.Pokemon
{
    public class PokemonData
    {
        public PokemonDataMeta data;
        public string name;
        public int level;

        public int health_ability;
        public int attack_ability;
        public int defense_ability;
        public int speed_ability;

        public int health_individual;
        public int attack_individual;
        public int defense_individual;
        public int speed_individual;

        public int health_effort;
        public int attack_effort;
        public int defense_effort;
        public int speed_effort;

        public PokemonData(PokemonEnum pokemonEnum,
            int level,
            int healthIndividual,
            int attackIndividual,
            int defenseIndividual,
            int speedIndividual,
            int healthEffort,
            int attackEffort,
            int defenseEffort,
            int speedEffort
        )
        {
            this.level = level;

            this.health_individual = healthIndividual;
            this.attack_individual = attackIndividual;
            this.defense_individual = defenseIndividual;
            this.speed_individual = speedIndividual;
            
            this.health_effort = healthEffort;
            this.attack_effort = attackEffort;
            this.defense_effort = defenseEffort;
            this.speed_effort = speedEffort;
            
            this.data = PokemonDataMeta.Find(pokemonEnum);
            //ToDo 计算默认能力值
            _cal_default();
        }

        private void _cal_default()
        {
            throw new NotImplementedException();
        }
        public void LevelUp()
        {
            throw new NotImplementedException();
        }
    }
}