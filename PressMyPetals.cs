using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PressMyPetals;

public class PressMyPetals : BaseSettingsPlugin<PressMyPetalsSettings>
{
    internal List<PetalSkill> PetalSkills;
    internal NearbyMonsterInfo MonsterInfo;
    internal int? currentPetals;
    internal bool memoryZone = false;
    internal List<PetalSkill> availableSkills = new List<PetalSkill>();

    internal List<Button> Hotkeys = new List<Button>();

    private SyncTask<bool> _currentTask;
    private bool _initialized = false;
    private string debugString;

    public override bool Initialise()
    {
        PetalSkills = new List<PetalSkill> {
            new PetalSkillTypes.MemoryOfDesire(this),
            new PetalSkillTypes.MemoryOfDisbelief(this),
            new PetalSkillTypes.MemoryOfFamiliarity(this),
            new PetalSkillTypes.MemoryOfImpatience(this),
            new PetalSkillTypes.MemoryOfImpulsiveness(this),
            new PetalSkillTypes.MemoryOfMocking(this),
            new PetalSkillTypes.MemoryOfPanic(this),
            new PetalSkillTypes.MemoryOfSuspicion(this)
        };
        _initialized = true;
        DebugWindow.LogMsg("PressMyPetals initialized!");
        return true;
    }

    public override void AreaChange(AreaInstance area)
    {
        foreach (var skill in availableSkills) {
            skill?.ResetState();
        }
        memoryZone = false;
        _currentTask = null;
    }

    public override Job Tick()
    {
        return null;
    }

    private async SyncTask<bool> Routine() {
        while (memoryZone && availableSkills.Count() > 0) {
            MonsterInfo = new NearbyMonsterInfo(this, Settings);
            try {
                currentPetals = (GameController?.Player.Buffs.Where(b => b.Name == "zana_rose_petal_visual_buff").Select(b => (int)b.BuffCharges).FirstOrDefault());
            } catch (Exception e) {
                currentPetals = 0;
                DebugWindow.LogError($"Failed to get petal amount from buffs dictionary: {e.Message}");
                return false;
            }

            List<PetalSkill> selectedSkills = new List<PetalSkill>();
            List<string> debugStringEntries = new List<string>();

            foreach (var skill in availableSkills) {
                if (skill.CanBeUsed()) {
                    selectedSkills.Add(skill);
                }
                if (Settings.DebugSettings.ShowDebug) {
                    debugStringEntries.Add($"{skill.Name} ({skill.ShortDescription}) on key \"{skill.Hotkey}\":\n\t\tCharges = {skill.CurrentCharges}, BaseCost = {skill.BasePetalCost}, " +
                    $"PowerCost = {skill.PerPowerPetalCost}, Last Used = {(skill.Timer.IsRunning ? (int)skill.Timer.Elapsed.TotalSeconds + "s" : "N/A")}");
                }
            }
            if (Settings.DebugSettings.ShowDebug) {
                debugString = string.Join("\n\n\t", debugStringEntries);
            }

            if (selectedSkills.Count > 0) {
                if (Settings.DebugSettings.ShowDebug) DebugWindow.LogMsg($"Conditions for the following skills are met: {string.Join(", ", selectedSkills.Select(s => s.Name))}");
                var chosenSkill = selectedSkills.FirstOrDefault();
                if (selectedSkills.Count > 1) {
                    var bestCandidate = selectedSkills
                        .OrderByDescending(p => p.Timer.IsRunning ? p.Timer.Elapsed.TotalSeconds : short.MaxValue)
                        .ThenBy(p => p.Priority)
                        .FirstOrDefault();
                    if (bestCandidate != null) {
                        chosenSkill = bestCandidate;
                    }
                }
                if (Settings.DebugSettings.ShowDebug) DebugWindow.LogMsg($"Triggering petal skill: Name = {chosenSkill.Name}, Priority = {chosenSkill.Priority}, cooldown = {(chosenSkill.Timer.IsRunning ? chosenSkill.Timer.Elapsed.TotalSeconds : "N/A")}");

                Input.KeyPressRelease(chosenSkill.Hotkey);
                if (chosenSkill.Timer.IsRunning) {
                    chosenSkill.Timer.Restart();
                } else {
                    chosenSkill.Timer.Start();
                }
                await Task.Delay(500);
            } else {
                await Task.Delay(50);
            }
        }
        return false;
    }

    private async SyncTask<bool> CheckArea() {
        while (!memoryZone) {
            try {
                var zone = GameController?.IngameState.Data.MapStats[ExileCore.Shared.Enums.GameStat.MapZanaInfluence];
                if (Settings.DebugSettings.ShowDebug) DebugWindow.LogMsg("Entered originator influenced area! Looking for memory petal skills..");
                if (zone != 0) memoryZone = true;
            } catch {
                memoryZone = false;
            }
            availableSkills.Clear();
            if (memoryZone) {
                var UI_Elements = GameController?.IngameState.IngameUi.GameUI.Children[7].Children;

                var matches = Util.Traverse<Element>(UI_Elements, x => x.Children, x => x.Tooltip.Children);
                var results = matches.Where(x => x.Text != null && x.Text.Contains("Memory of"));
                var petalSkillButtons = results.FirstOrDefault().GetParentChain().ElementAt(5).Children;

                if (Settings.DebugSettings.ShowDebug) DebugWindow.LogMsg("Skill buttons found: " + petalSkillButtons.Count);

                if (petalSkillButtons.Count <= 0) {
                    if (Settings.DebugSettings.ShowDebug) DebugWindow.LogError("Can't find petal skill buttons!");
                    memoryZone = false;
                }

                List<string> foundSkills = new List<string>();

                foreach (var button in petalSkillButtons) {
                    var buttonSkillName = button.Tooltip.Children[0].Children[0].Children[0].Children[1].Text;
                    foundSkills.Add(buttonSkillName);
                    buttonSkillName = buttonSkillName.Split(" ").Last().Trim();
                    var skill = PetalSkills.Where(p => p.Name.Contains(buttonSkillName)).FirstOrDefault();
                    if (skill != null) {
                        skill.SkillButton = button;
                        availableSkills.Add(skill);
                    }
                }

                if (Settings.DebugSettings.ShowDebug) DebugWindow.LogMsg("Available skills: " + string.Join(", ", foundSkills));

                _currentTask = null;
                return true;
            }
            await Task.Delay(5000);
        }
        return false;
    }

    public override void Render() {
        if (!_initialized) {
            Initialise();
        }
        if (_currentTask != null) {
            TaskUtils.RunOrRestart(ref _currentTask, () => null);
        } else {
            if (memoryZone) {
                _currentTask = Routine();
            } else {
                _currentTask = CheckArea();
            }
        }

        if (Settings.DebugSettings.ShowDebug && memoryZone && MonsterInfo != null) {
            if (availableSkills.Count() > 0) {
                var locX = Settings.DebugSettings.DebugPositionX; 
                var locY = Settings.DebugSettings.DebugPositionY;
                Graphics.DrawText($"Petal Skill Automation:\n\tnearbyPower = {MonsterInfo.NearbyPower}, nearbyMonsters = {MonsterInfo.NearbyMonsters.Count()} (Normal:{MonsterInfo.NearbyNormalMonsters}, Magic:{MonsterInfo.NearbyMagicMonsters}, Rare:{MonsterInfo.NearbyRareMonsters}, Unique:{MonsterInfo.NearbyUniqueMonsters}), "
                + $"Total Petals = {currentPetals}", new System.Numerics.Vector2(locX, locY), SharpDX.Color.White);
                Graphics.DrawText($"Petal Skills:\n\t{debugString}", new System.Numerics.Vector2(locX, locY + 50), SharpDX.Color.White);
            }
        }
        return;
    }
}