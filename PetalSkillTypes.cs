namespace PressMyPetals {
    internal class PetalSkillTypes {
        internal class MemoryOfDesire : PetalSkill {
            private readonly MemoryOfDesireSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get {  return settings.Cooldown; } }
            internal override int Priority {  get { return settings.Priority; } }
            internal MemoryOfDesire(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfDesire.Name;
                Description = PetalSkillConstants.MemoryOfDesire.Description;
                ShortDescription = PetalSkillConstants.MemoryOfDesire.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfDesire.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfDesire.PerPowerPetalCost;
                settings = plugin.Settings.DesireSettings;
                IsConditionMet = () => 
                    MInfo.NearbyRareMonsters >= settings.Condition.MinimumNearbyRareMonsters
                    || MInfo.NearbyUniqueMonsters >= settings.Condition.MinimumNearbyUniqueMonsters;
            }
        }
        internal class MemoryOfDisbelief : PetalSkill {
            private readonly MemoryOfDisbeliefSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfDisbelief(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfDisbelief.Name;
                Description = PetalSkillConstants.MemoryOfDisbelief.Description;
                ShortDescription = PetalSkillConstants.MemoryOfDisbelief.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfDisbelief.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfDisbelief.PerPowerPetalCost;
                settings = plugin.Settings.DisbeliefSettings;
                IsConditionMet = () => 
                    MInfo.NearbyPower >= settings.Condition.MinimumNearbyPower
                    || MInfo.NearbyMagicMonsters >= settings.Condition.MinimumNearbyMagicMonsters 
                    || MInfo.NearbyRareMonsters >= settings.Condition.MinimumNearbyRareMonsters 
                    || MInfo.NearbyUniqueMonsters >= settings.Condition.MinimumNearbyUniqueMonsters;
            }
        }

        internal class MemoryOfFamiliarity : PetalSkill {
            private readonly MemoryOfFamiliaritySettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfFamiliarity(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfFamiliarity.Name;
                Description = PetalSkillConstants.MemoryOfFamiliarity.Description;
                ShortDescription = PetalSkillConstants.MemoryOfFamiliarity.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfFamiliarity.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfFamiliarity.PerPowerPetalCost;
                settings = plugin.Settings.FamiliaritySettings;
                IsConditionMet = () =>
                    MInfo.NearbyPower >= settings.Condition.MinimumNearbyPower
                    || MInfo.NearbyMagicMonsters >= settings.Condition.MinimumNearbyMagicMonsters
                    || MInfo.NearbyRareMonsters >= settings.Condition.MinimumNearbyRareMonsters
                    || MInfo.NearbyUniqueMonsters >= settings.Condition.MinimumNearbyUniqueMonsters;
            }
        }

        internal class MemoryOfImpatience : PetalSkill {
            private readonly MemoryOfImpatienceSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfImpatience(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfImpatience.Name;
                Description = PetalSkillConstants.MemoryOfImpatience.Description;
                ShortDescription = PetalSkillConstants.MemoryOfImpatience.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfImpatience.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfImpatience.PerPowerPetalCost;
                settings = plugin.Settings.ImpatienceSettings;
                IsConditionMet = () => MInfo.NearbyPower >= settings.Condition.MinimumNearbyPower;
            }
        }

        internal class MemoryOfImpulsiveness : PetalSkill {
            private readonly MemoryOfImpulsivenessSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfImpulsiveness(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfImpulsiveness.Name;
                Description = PetalSkillConstants.MemoryOfImpulsiveness.Description;
                ShortDescription = PetalSkillConstants.MemoryOfImpulsiveness.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfImpulsiveness.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfImpulsiveness.PerPowerPetalCost;
                settings = plugin.Settings.ImpulsivenessSettings;
                IsConditionMet = () => MInfo.NearbyPower >= settings.Condition.MinimumNearbyPower;
            }
        }

        internal class MemoryOfMocking : PetalSkill {
            private readonly MemoryOfMockingSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfMocking(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfMocking.Name;
                Description = PetalSkillConstants.MemoryOfMocking.Description;
                ShortDescription = PetalSkillConstants.MemoryOfMocking.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfMocking.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfMocking.PerPowerPetalCost;
                settings = plugin.Settings.MockingSettings;
                IsConditionMet = () => 
                    MInfo.NearbyRareMonsters >= settings.Condition.MinimumNearbyRareMonsters
                    || MInfo.NearbyMagicMonsters >= settings.Condition.MinimumNearbyMagicMonsters;
                IsCostCovered = () => CurrentCharges >= BasePetalCost + PerPowerPetalCost * (MInfo.NearbyMagicMonsters * 2 + MInfo.NearbyRareMonsters * 5 + MInfo.NearbyUniqueMonsters * 10);
            }
        }
        internal class MemoryOfPanic : PetalSkill {
            private readonly MemoryOfPanicSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfPanic(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfPanic.Name;
                Description = PetalSkillConstants.MemoryOfPanic.Description;
                ShortDescription = PetalSkillConstants.MemoryOfPanic.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfPanic.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfPanic.PerPowerPetalCost;
                settings = plugin.Settings.PanicSettings;
                IsConditionMet = () =>
                    MInfo.NearbyPower >= settings.Condition.MinimumNearbyPower 
                    && (MInfo.NearbyMagicMonsters >= settings.Condition.MinimumNearbyMagicMonsters
                        || MInfo.NearbyRareMonsters >= settings.Condition.MinimumNearbyRareMonsters);
                IsCostCovered = () => CurrentCharges >= BasePetalCost + PerPowerPetalCost * (MInfo.NearbyPower - MInfo.NearbyUniqueMonsters * 10);
            }
        }

        internal class MemoryOfSuspicion : PetalSkill {
            private readonly MemoryOfSuspicionSettings settings;
            internal override NearbyMonsterInfo MInfo { get { return Plugin.MonsterInfo; } }
            internal override bool IsAutomated { get { return settings.Automate; } }
            internal override int Cooldown { get { return settings.Cooldown; } }
            internal override int Priority { get { return settings.Priority; } }
            internal MemoryOfSuspicion(PressMyPetals plugin) : base(plugin) {
                Name = PetalSkillConstants.MemoryOfSuspicion.Name;
                Description = PetalSkillConstants.MemoryOfSuspicion.Description;
                ShortDescription = PetalSkillConstants.MemoryOfSuspicion.ShortDescription;
                BasePetalCost = PetalSkillConstants.MemoryOfSuspicion.BasePetalCost;
                PerPowerPetalCost = PetalSkillConstants.MemoryOfSuspicion.PerPowerPetalCost;
                settings = plugin.Settings.SuspicionSettings;
                IsConditionMet = () => MInfo.NearbyNormalMonsters >= settings.Condition.MinimumNearbyNormalMonsters;
                IsCostCovered = () => CurrentCharges >= BasePetalCost + PerPowerPetalCost * MInfo.NearbyNormalMonsters;
            }
        }
    }
}
