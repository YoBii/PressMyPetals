namespace PressMyPetals {
    internal static class PetalSkillConstants {
        internal static class MemoryOfDesire {
            internal const string Name = "ZanaInfluenceMemoryofDesire";
            internal const string Description = "Consumes Petals to cause affected nearby monsters to have a chance to gain bonus rewards " +
                    "and 100 percent increased Toughness.\nHigher rarity monsters have a higher chance to gain rewards.\n" +
                    "The number of Petals consumed is proportional to the Power of affected monsters.\n" +
                    "(7 percent normal, 15 percent magic, 100 percent rare/unique)";
            internal const string ShortDescription = "More toughness & rewards";
            internal const int BasePetalCost = 40;
            internal const int PerPowerPetalCost =6;
        }

        internal static class MemoryOfDisbelief {
            internal const string Name = "ZanaInfluenceMemoryofDisbelief";
            internal const string Description = "Consumes Petals to cause affected nearby monsters to not be able to drop items that can " +
                    "have rarity.\nThe number of Petals consumed is proportional to the Power of affected monsters.";
            internal const string ShortDescription = "No rarity-item drops";
            internal const int BasePetalCost = 30;
            internal const int PerPowerPetalCost = 6;
        }

        internal static class MemoryOfFamiliarity {
            internal const string Name = "ZanaInfluenceMemoryofFamiliarity";
            internal const string Description = "Consumes Petals to cause affected nearby monsters to have either 200 percent more item quantity " +
                    "or 90 percent less item quantity, and have all dropped items of the same type.\n" +
                    "The number of Petals consumed is proportional to the Power of affected monsters.";
            internal const string ShortDescription = "Gamble IIQ";
            internal const int BasePetalCost = 30;
            internal const int PerPowerPetalCost = 4;
        }

        internal static class MemoryOfImpatience {
            internal const string Name = "ZanaInfluenceMemoryofImpatience";
            internal const string Description = "Consumes Petals to extract Souls from nearby monsters.\nThe number of Petals consumed is " +
                    "proportional to the Power of nearby monsters.";
            internal const string ShortDescription = "Soul Eater";
            internal const int BasePetalCost = 20;
            internal const int PerPowerPetalCost = 3;
        }

        internal static class MemoryOfImpulsiveness {
            internal const string Name = "ZanaInfluenceMemoryofImpulsiveness";
            internal const string Description = "Consumes Petals to cause affected nearby monsters to have 200 percent increased Item Rarity " +
                    "and convert non-unique equipment items to Gold.\nThe number of Petals consumed is " +
                    "proportional to the Power of affected monsters. " +
                    "200 percent increased Quantity of Gold Dropped by Slain Enemies";
            internal const string ShortDescription = "Boost gold";
            internal const int BasePetalCost = 30;
            internal const int PerPowerPetalCost = 4;
        }

        internal static class MemoryOfMocking {
            internal const string Name = "ZanaInfluenceMemoryofMocking";
            internal const string Description = "Consumes Petals to gain the modifiers of affected nearby magic and rare monsters for 30 seconds.\n" +
                    "The number of Petals consumed is proportional to the Power of affected monsters.";
            internal const string ShortDescription = "Headhunter";
            internal const int BasePetalCost = 20;
            internal const int PerPowerPetalCost = 3;
        }

        internal static class MemoryOfPanic {
            internal const string Name = "ZanaInfluenceMemoryofPanic";
            internal const string Description = "Consumes Petals to cause affected monsters to revive on death, dropping loot and granting " +
                    "experience again.\nThe number of Petals consumed is proportional to the Power of affected monsters.\n" +
                    "Does not affect Unique monsters.";
            internal const string ShortDescription = "Revive monsters";
            internal const int BasePetalCost = 50;
            internal const int PerPowerPetalCost = 12;
        }

        internal static class MemoryOfSuspicion {
            internal const string Name = "ZanaInfluenceMemoryofSuspicion";
            internal const string Description = "Consumes Petals to cause affected nearby normal monsters to have a chance to be upgraded to " +
                    "magic or rare rarity.\n(50 percent magic, 8 percent rare)";
            internal const string ShortDescription = "Chance monsters";
            internal const int BasePetalCost = 30;
            internal const int PerPowerPetalCost = 10;
        }
    }
}
