namespace PressMyPetals {
    internal class PetalSkillCondition {
        private readonly PetalSkill Skill;

        internal delegate bool CostFunction();
        internal CostFunction Cost { get; set; } = () => false;

        internal delegate bool ConditionFunction();
        internal ConditionFunction Condition { get; set; } = () => false;

        internal PetalSkillCondition(PetalSkill skill, ConditionFunction func) {
            this.Skill = skill;
            this.Cost = () => Skill.CurrentCharges >= Skill.BasePetalCost + Skill.PerPowerPetalCost * Skill.Plugin.MonsterInfo.NearbyPower;
            this.Condition = func;
        }

        internal bool Evaluate() {
            return Condition() && Cost();
        }
    }
}
