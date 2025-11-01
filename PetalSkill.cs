using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace PressMyPetals {
    internal abstract class PetalSkill {
        internal PressMyPetals Plugin;
        internal string Name { get; init; }
        internal string Description { get; init; }
        internal string ShortDescription { get; init; }
        internal int CurrentCharges { get { return GetCharges(); } }
        internal Element SkillButton { get; set; }
        internal Stopwatch Timer { get; private set; } = new Stopwatch();
        
        internal delegate bool CostFunction();
        internal CostFunction IsCostCovered { get; set; } = () => false;

        internal delegate bool ConditionFunction();
        internal ConditionFunction IsConditionMet { get; set; } = () => false;

        internal virtual NearbyMonsterInfo MInfo { get; }
        internal virtual bool IsAutomated { get; }
        internal virtual int Cooldown { get; }
        internal virtual int Priority { get; }
        
        private int _basePetalCost = 0;
        internal int BasePetalCost {
            get {
                if (_basePetalCost <= 0) { BasePetalCost = GetBaseCost(); }
                return _basePetalCost;
            } set {
                _basePetalCost = value;
            }
        }
        private int _perPowerPetalCost = 0;
        internal int PerPowerPetalCost {
            get {
                if (_basePetalCost <= 0) { PerPowerPetalCost = GetPerPowerCost(); }
                return _perPowerPetalCost;
            }
            set {
                _perPowerPetalCost = value;
            }
        }

        private Keys _hotkey = Keys.None;
        internal Keys Hotkey {
            get {
                if (_hotkey == Keys.None) { Hotkey = ParseHotkey(); }
                return _hotkey;
            } private set {
                _hotkey = value;
            }
        }

        internal protected PetalSkill (PressMyPetals plugin) {
            this.Plugin = plugin;
            this.IsCostCovered = () => CurrentCharges >= BasePetalCost + PerPowerPetalCost * Plugin.MonsterInfo.NearbyPower;
        }

        internal bool CanBeUsed() {
            var actorSkill = Plugin.GameController?.Player.GetComponent<Actor>().ActorSkills.Where(s => s.Name == Name).FirstOrDefault();
            if (IsAutomated && (!Timer.IsRunning || Timer.Elapsed.TotalSeconds > Cooldown)
                && actorSkill != null && actorSkill.CanBeUsed
                && IsConditionMet() && IsCostCovered()) {
                return true;
            } else {
                return false;
            }
        }

        internal void ResetState() {
            Timer.Reset();
            SkillButton = null;
            Hotkey = Keys.None;
        }

        private Keys ParseHotkey() {
            var indexInParent = (int)SkillButton.IndexInParent;
            var shortcuts = Plugin.GameController.IngameState.ShortcutSettings.Shortcuts;
            var petalSkillStartIndex = shortcuts.ToList().FindIndex(s => s.Usage.ToString() == "ApexSentinel") + 1;

            if(indexInParent < 0 || petalSkillStartIndex < 0 || shortcuts.Count < 1) {
                return Keys.None;
            }
            return (Keys)Plugin.GameController.IngameState.ShortcutSettings.Shortcuts.ElementAt(petalSkillStartIndex + indexInParent).MainKey;
        }

        private int GetCharges() {
            if (SkillButton != null && SkillButton.ChildCount > 2) {
                var s = SkillButton.Children[2].Text;
                if (s != null && s.Length > 0) {
                    try {
                        s = new string(s.Where(Char.IsDigit).ToArray());
                        return int.Parse(s);
                    } catch (Exception e) {
                        DebugWindow.LogError($"Failed to parse current charges for {Name}: {e.Message}");
                        return 0;
                    }
                }
            }
            return 0;
        }

        private int GetPerPowerCost() {
            var actorSkills = Plugin.GameController?.Player.GetComponent<Actor>().ActorSkills;
            if (actorSkills != null && actorSkills.Count() > 0) {
                return (int)actorSkills.Where(s => s.Name == Name).FirstOrDefault()?.GetStat(ExileCore.Shared.Enums.GameStat.BasePetalCost);
            } else {
                DebugWindow.LogError($"Failed to get PerPowerCost for {Name}");
                return 0;
            }
        }

        private int GetBaseCost() {
            var actorSkills = Plugin.GameController?.Player.GetComponent<Actor>().ActorSkills;
            if (actorSkills != null && actorSkills.Count() > 0) {
                var costPerPower = (int)actorSkills.Where(s => s.Name == Name).FirstOrDefault()?.GetStat(ExileCore.Shared.Enums.GameStat.PetalCostPerMonsterAffectedPower);
                var cost = (int)actorSkills.Where(s => s.Name == Name).FirstOrDefault()?.GetStat(ExileCore.Shared.Enums.GameStat.PetalCostPerMonsterAffected);
                if (cost > costPerPower) {
                    return cost;
                } else {
                    return costPerPower;
                }
            } else {
                DebugWindow.LogError($"Failed to get BasePetalCost for {Name}");
                return 0;
            }
        }

    }
}
