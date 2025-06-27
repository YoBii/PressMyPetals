using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using Newtonsoft.Json;
using PressMyPetals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace PressMyPetals;

/* 
 * Taken from ReAgent. Slightly adapted. 
 */

public class NearbyMonsterInfo {
    private readonly PressMyPetalsSettings settings;
    private readonly SortedDictionary<int, List<MonsterInfo>> _monsters;

    internal IEnumerable<MonsterInfo> NearbyMonsters => SearchMonsters(settings.NearbyRadius, MonsterRarity.Any); 
    internal int NearbyNormalMonsters => SearchMonsters(settings.NearbyRadius, MonsterRarity.Normal).Count();
    internal int NearbyMagicMonsters => SearchMonsters(settings.NearbyRadius, MonsterRarity.Magic).Count();
    internal int NearbyRareMonsters => SearchMonsters(settings.NearbyRadius, MonsterRarity.Rare).Count();
    internal int NearbyUniqueMonsters => SearchMonsters(settings.NearbyRadius, MonsterRarity.Unique).Where(m => !m.Metadata.Contains("Mercenary")).Count();
    internal int NearbyPower { 
        get {
            var pwr = NearbyNormalMonsters;
            pwr += NearbyMagicMonsters * 2;
            pwr += NearbyRareMonsters * 5;
            pwr += NearbyUniqueMonsters * 10;
            return pwr;
        } 
    }

    public NearbyMonsterInfo(PressMyPetals plugin, PressMyPetalsSettings settings) {
        this.settings = settings;
        _monsters = new SortedDictionary<int, List<MonsterInfo>>();
        if (!plugin.GameController.Player.HasComponent<Render>()) {
            return;
        }

        foreach (var entity in plugin.GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster]) {
            if (!IsValidMonster(plugin, entity, true, false)) {
                continue;
            }

            var distance = (int)Math.Ceiling(entity.DistancePlayer);
            var monsterInfo = new MonsterInfo(plugin.GameController, entity);
            if (entity.IsHostile) {
                if (_monsters.TryGetValue(distance, out var list)) {
                    list.Add(monsterInfo);
                } else {
                    _monsters[distance] = [monsterInfo];
                }
            }
        }
    }

    public static bool IsValidMonster(PressMyPetals plugin, Entity entity, bool checkIsAlive, bool desiredIsHiddenValue) =>
        entity.DistancePlayer <= plugin.Settings.NearbyRadius &&
        entity.HasComponent<Monster>() &&
        entity.HasComponent<Positioned>() &&
        entity.HasComponent<Render>() &&
        entity.HasComponent<Life>() &&
        (!checkIsAlive || entity.IsAlive) &&
        entity.HasComponent<ObjectMagicProperties>() &&
        entity.TryGetComponent<Buffs>(out var buffs) &&
        (desiredIsHiddenValue == buffs.HasBuff("hidden_monster"));

    public IEnumerable<MonsterInfo> SearchMonsters(int range, MonsterRarity rarity) {
        return _monsters
            .TakeWhile(x => x.Key <= range)
            .SelectMany(x => x.Value)
            .Where(x => (x.Rarity & rarity) != 0)
            .Where(x => x.IsTargetable && x.IsAlive && !x.IsInvincible );
    }
}

public class MonsterInfo : EntityInfo {
    private bool? _isInvincible;

    public MonsterInfo(GameController controller, Entity entity) : base(controller, entity) { 
    }

    public bool IsInvincible => _isInvincible ??= Stats[GameStat.CannotBeDamaged].Value switch { 0 => false, _ => true };

    public MonsterRarity Rarity => Entity.Rarity switch {
        ExileCore.Shared.Enums.MonsterRarity.White => MonsterRarity.Normal,
        ExileCore.Shared.Enums.MonsterRarity.Magic => MonsterRarity.Magic,
        ExileCore.Shared.Enums.MonsterRarity.Rare => MonsterRarity.Rare,
        ExileCore.Shared.Enums.MonsterRarity.Unique => MonsterRarity.Unique,
        _ => MonsterRarity.Normal
    };
}

public class StatDictionary {
    private readonly Dictionary<GameStat, int> _source;

    public StatDictionary(Dictionary<GameStat, int> source) {
        _source = source;
    }

    public Stat this[GameStat id] {
        get {
            if (_source.TryGetValue(id, out var value)) {
                return new Stat(true, value);
            }

            return new Stat(false, 0);
        }
    }

    public bool Has(GameStat id) {
        return _source.ContainsKey(id);
    }

    [JsonProperty]
    private Dictionary<GameStat, int> AllStats => _source;
}

public class EntityInfo {
    protected readonly GameController Controller;
    protected readonly Entity Entity;
    private readonly Lazy<StatDictionary> _stats;

    public EntityInfo(GameController controller, Entity entity) {
        Controller = controller;
        Entity = entity;
        _stats = new Lazy<StatDictionary>(() => new StatDictionary(Entity.Stats ?? new Dictionary<GameStat, int>()), LazyThreadSafetyMode.None);
    }

    public string Metadata => Entity.Metadata;
    public bool IsAlive => Entity.IsAlive;
    public StatDictionary Stats => _stats.Value;
    public bool IsTargeted => Entity.TryGetComponent<Targetable>(out var targetable) && targetable.isTargeted;
    public bool IsTargetable => Entity.TryGetComponent<Targetable>(out var targetable) && targetable.isTargetable;
}

public enum MonsterRarity {
    Normal = 1 << 0,
    Magic = 1 << 1,
    Rare = 1 << 2,
    Unique = 1 << 3,
    Any = Normal | Magic | Rare | Unique,
    AtLeastRare = Rare | Unique
}

public record Stat(bool Exists, int Value);