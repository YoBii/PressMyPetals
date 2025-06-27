using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace PressMyPetals;

public class PressMyPetalsSettings : ISettings
{
    public ToggleNode Enable { get; set; } = new ToggleNode(false);
    public DebugSettings DebugSettings { get; set; } = new DebugSettings();
    public RangeNode<int> NearbyRadius { get; set; } = new RangeNode<int>(85, 0, 200);
    [Menu($"Memory Of Desire ({PetalSkillConstants.MemoryOfDesire.ShortDescription})")]
    public MemoryOfDesireSettings DesireSettings { get; set; } = new MemoryOfDesireSettings();
    [Menu($"Memory Of Disbelief ({PetalSkillConstants.MemoryOfDisbelief.ShortDescription})")]
    public MemoryOfDisbeliefSettings DisbeliefSettings { get; set; } = new MemoryOfDisbeliefSettings();
    [Menu($"Memory Of Familiarity ({PetalSkillConstants.MemoryOfFamiliarity.ShortDescription})")]
    public MemoryOfFamiliaritySettings FamiliaritySettings { get; set; } = new MemoryOfFamiliaritySettings();
    [Menu($"Memory Of Impatience ({PetalSkillConstants.MemoryOfImpatience.ShortDescription})")]
    public MemoryOfImpatienceSettings ImpatienceSettings { get; set; } = new MemoryOfImpatienceSettings();
    [Menu($"Memory Of Impulsiveness ({PetalSkillConstants.MemoryOfImpulsiveness.ShortDescription})")]
    public MemoryOfImpulsivenessSettings ImpulsivenessSettings { get; set; } = new MemoryOfImpulsivenessSettings();
    [Menu($"Memory Of Mocking ({PetalSkillConstants.MemoryOfMocking.ShortDescription})")]
    public MemoryOfMockingSettings MockingSettings { get; set; } = new MemoryOfMockingSettings();
    [Menu($"Memory Of Panic ({PetalSkillConstants.MemoryOfPanic.ShortDescription})")]
    public MemoryOfPanicSettings PanicSettings { get; set; } = new MemoryOfPanicSettings();
    [Menu($"Memory Of Suspicion ({PetalSkillConstants.MemoryOfSuspicion.ShortDescription})")]
    public MemoryOfSuspicionSettings SuspicionSettings { get; set; } = new MemoryOfSuspicionSettings();
}
[Submenu(CollapsedByDefault = true)]
public class DebugSettings {
    [Menu("Show debug information", "Draw information about petal skills and nearby monsters on screen.\nAlso enables additional logging.")]
    public ToggleNode ShowDebug { get; set; } = new ToggleNode(false);
    public RangeNode<int> DebugPositionX { get; set; } = new RangeNode<int>(20, 0, 3840);
    public RangeNode<int> DebugPositionY { get; set; } = new RangeNode<int>(600, 0, 2160);

}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfDesireSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfDesire.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(5, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(300, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinRare || MinUnique")]
    public MemoryOfDesireConditionSettings Condition { get; set; } = new MemoryOfDesireConditionSettings();
}

[Submenu(CollapsedByDefault = false)]
public class MemoryOfDesireConditionSettings {
    public RangeNode<int> MinimumNearbyRareMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
    public RangeNode<int> MinimumNearbyUniqueMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfDisbeliefSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfDisbelief.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(3, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(300, 0, 1000);
    [Menu("Condition Logic", "Skill will be triggered when the following condition is true:\nMinPower || MinMagic || MinRare || MinUnique")]
    public MemoryOfDisbeliefConditionSettings Condition { get; set; } = new MemoryOfDisbeliefConditionSettings();
}

[Submenu(CollapsedByDefault = false)]
public class MemoryOfDisbeliefConditionSettings {
    public RangeNode<int> MinimumNearbyPower { get; set; } = new RangeNode<int>(15, 0, 100);
    public RangeNode<int> MinimumNearbyMagicMonsters { get; set; } = new RangeNode<int>(5, 0, 25);
    public RangeNode<int> MinimumNearbyRareMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
    public RangeNode<int> MinimumNearbyUniqueMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfFamiliaritySettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfFamiliarity.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(5, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(200, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinPower || MinMagic || MinRare || MinUnique")]
    public MemoryOfFamiliarityConditionSettings Condition { get; set; } = new MemoryOfFamiliarityConditionSettings();
}
[Submenu(CollapsedByDefault = false)]
public class MemoryOfFamiliarityConditionSettings {
    public RangeNode<int> MinimumNearbyPower { get; set; } = new RangeNode<int>(10, 0, 100);
    public RangeNode<int> MinimumNearbyMagicMonsters { get; set; } = new RangeNode<int>(3, 0, 25);
    public RangeNode<int> MinimumNearbyRareMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
    public RangeNode<int> MinimumNearbyUniqueMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfImpatienceSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfImpatience.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(2, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(100, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinPower")]
    public MemoryOfImpatienceConditionSettings Condition { get; set; } = new MemoryOfImpatienceConditionSettings();
}
[Submenu(CollapsedByDefault = false)]
public class MemoryOfImpatienceConditionSettings {
    public RangeNode<int> MinimumNearbyPower { get; set; } = new RangeNode<int>(5, 0, 100);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfImpulsivenessSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfImpulsiveness.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(3, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(150, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinPower")]
    public MemoryOfImpulsivenessConditionSettings Condition { get; set; } = new MemoryOfImpulsivenessConditionSettings();
}
[Submenu(CollapsedByDefault = false)]
public class MemoryOfImpulsivenessConditionSettings {
    public RangeNode<int> MinimumNearbyPower { get; set; } = new RangeNode<int>(10, 0, 100);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfMockingSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfMocking.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(3, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(100, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinMagic || MinRare")]
    public MemoryOfMockingConditionSettings Condition { get; set; } = new MemoryOfMockingConditionSettings();
}
[Submenu(CollapsedByDefault = false)]
public class MemoryOfMockingConditionSettings {
    public RangeNode<int> MinimumNearbyMagicMonsters { get; set; } = new RangeNode<int>(1, 0, 25);
    public RangeNode<int> MinimumNearbyRareMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfPanicSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfPanic.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(5, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(500, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinPower && (MinMagic || MinRare)")]
    public MemoryOfPanicConditionSettings Condition { get; set; } = new MemoryOfPanicConditionSettings();
}
[Submenu(CollapsedByDefault = false)]
public class MemoryOfPanicConditionSettings {
    public RangeNode<int> MinimumNearbyPower { get; set; } = new RangeNode<int>(10, 0, 100);
    public RangeNode<int> MinimumNearbyMagicMonsters { get; set; } = new RangeNode<int>(5, 0, 25);
    public RangeNode<int> MinimumNearbyRareMonsters { get; set; } = new RangeNode<int>(1, 0, 10);
}

[Submenu(CollapsedByDefault = true)]
public class MemoryOfSuspicionSettings {
    [Menu("Automate this skill", PetalSkillConstants.MemoryOfSuspicion.Description)]
    public ToggleNode Automate { get; set; } = new ToggleNode(true);
    [Menu("Cooldown", "Internal cooldown for this skill")]
    public RangeNode<int> Cooldown { get; set; } = new RangeNode<int>(3, 0, 60);
    [Menu("Priority", "If multiple skills can be used at the same time, the higher priority skill will be picked.")]
    public RangeNode<int> Priority { get; set; } = new RangeNode<int>(200, 0, 1000);
    [Menu("Trigger condition", "Skill will be triggered when the following condition is true:\nMinNormal")]
    public MemoryOfSuspicionConditionSettings Condition { get; set; } = new MemoryOfSuspicionConditionSettings();
}
[Submenu(CollapsedByDefault = false)]
public class MemoryOfSuspicionConditionSettings {
    public RangeNode<int> MinimumNearbyNormalMonsters { get; set; } = new RangeNode<int>(10, 0, 100);
}