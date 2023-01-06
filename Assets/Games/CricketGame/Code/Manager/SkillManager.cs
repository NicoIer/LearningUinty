using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Cricket_;
using Nico.Common;

namespace Games.CricketGame.Manager
{
    public static class SkillManager
    {
        public static int PhysicalInjury(CricketData attcker, CricketData defenser, Skill skill)
        {
            //额外加成最多可以造成 1*1.5*1.5 = 2.25倍加成
            var randomRate = RandomManager.Probability(85, 100); //随机数
            var restrainRate = PropertyMap.QueryAttack(skill.propertyEnum, defenser.meta.property); //属性克制
            var effectRate = attcker.attackAbility / defenser.defenseAbility; //攻击/防御比
            float propertyRate = attcker.meta.property == skill.propertyEnum ? 1.5f : 1;
            return (int)(skill.meta.power * effectRate * randomRate * restrainRate * propertyRate);
        }
    }
}