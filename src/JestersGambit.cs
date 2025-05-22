using SML;
using System;
using CommandLib.API;
using System.Collections.Generic;
using Services;
using Server.Shared.State;
using System.Linq;
using BetterTOS2;

namespace JestersGambit
{
    [Mod.SalemMod]
    public class JestersGambit
    {

        private static readonly Random random = new();

        private static readonly HashSet<int> VanillaDupes =
        [
            1, 2, 3, 4, 5, 6, 7, 8, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
            40, 43, 45, 48, 49, 51,
            54, 56
        ];

        private static readonly HashSet<int> BTOSDupes =
        [
            1, 2, 3, 4, 5, 6, 7, 8, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
            40, 43, 45, 48, 49, 51, 53,
            61
        ];


        public static void Start()
        {
            Console.WriteLine("why put effort into your role lists?");

            CommandRegistry.AddCommand(new RoleCommand("randomrole", new[] { "r", "r-role" }));
            CommandRegistry.AddCommand(new ModifierCommand("randommodifier", new[] { "m", "r-mod" }));
            CommandRegistry.AddCommand(new BanCommand("randomban", new[] { "b", "r-ban" }));
            CommandRegistry.AddCommand(new ClearCommand("clear", new[] { "c" }));
            CommandRegistry.AddCommand(new FullRandomCommand("randomall", new[] { "a", "r-all" }));

        }
        public class RoleCommand : Command, IHelpMessage
        {
            public RoleCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
            {
            }

            private const int MaxRoleCount = 15;

            private static readonly List<int> VanillaRoleIDs = [.. Enumerable.Range(1, 56)];
            private static readonly List<int> VanillaBucketIDs = [.. Enumerable.Range(100, 18)];

            private static readonly List<int> BTOSRoleIDs = [.. Enumerable.Range(1, 62), 251, 252];
            private static readonly List<int> BTOSBucketIDs = [.. Enumerable.Range(100, 22)];

            public static void AddRandomRoleToDeck(Random rand)
            {
                int setting = ModSettings.GetInt("Role Bucket Chance", "loonie.jestersgambit");
                double bucketChance = Math.Clamp(setting, 0, 100) / 100.0;

                var currentRoles = Service.Game.Sim.simulation.roleDeckBuilder.Data.roles.Select(role => (int)role).ToHashSet();
                var currentBans = Service.Game.Sim.simulation.roleDeckBuilder.Data.bannedRoles.Select(role => (int)role).ToHashSet();

                var roles = Utils.IsBTOS2() ? BTOSRoleIDs : VanillaRoleIDs;
                var buckets = Utils.IsBTOS2() ? BTOSBucketIDs : VanillaBucketIDs;
                var validDupes = Utils.IsBTOS2() ? BTOSDupes : VanillaDupes;

                const int maxAttempts = 50;

                for (int attempts = 0; attempts < maxAttempts; attempts++)
                {
                    List<int> sourceList = rand.NextDouble() < bucketChance ? buckets : roles;
                    int roleID = sourceList[rand.Next(sourceList.Count)];

                    bool isDupe = validDupes.Contains(roleID);
                    bool alreadyInDeck = currentRoles.Contains(roleID);
                    bool isBanned = currentBans.Contains(roleID);

                    if ((isDupe || !alreadyInDeck) && !isBanned)
                    {
                        Service.Game.Sim.simulation.AddRoleToRoleDeck((Role)roleID);
                        Console.WriteLine($"Attempted to add role {roleID} to the role deck.");
                        return;
                    }
                }

                Console.WriteLine("Failed to add a valid random role after multiple attempts.");
            }


            public override Tuple<bool, string> Execute(string[] args)
            {
                int count = 1;

                if (args.Length > 0 && int.TryParse(args[0], out int parsedCount))
                {
                    count = Math.Clamp(parsedCount, 1, MaxRoleCount);
                }

                for (int i = 0; i < count; i++)
                {
                    AddRandomRoleToDeck(random);
                }

                return new Tuple<bool, string>(true, $"Attempted to add {count} random roles to the role deck.");
            }

            public string GetHelpMessage()
            {
                return "Adds up to 15 random valid roles to the role list.";
            }
        }

        public class ModifierCommand : Command, IHelpMessage
        {
            public ModifierCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
            {
            }

            private const int MaxModifierCount = 3;

            private static readonly List<int> VanillaModifierIDs = [.. Enumerable.Range(201, 14)];

            private static readonly List<int> BTOSModifierIDs = [.. Enumerable.Range(201, 31)];

            public static void AddRandomModifierToDeck(Random rand)
            {
                var validModifierIDs = Utils.IsBTOS2() ? BTOSModifierIDs : VanillaModifierIDs;
                int attempts = 0;
                const int maxAttempts = 50;

                var currentModifiers = Service.Game.Sim.simulation.roleDeckBuilder.Data.modifierCards.Select(role => (int)role).ToHashSet();

                while (attempts++ < maxAttempts)
                {
                    int roleID = validModifierIDs[rand.Next(validModifierIDs.Count)];

                    if (currentModifiers.Contains(roleID))
                        continue;

                    Service.Game.Sim.simulation.AddRoleToRoleDeck((Role)roleID);
                    Console.WriteLine($"Attempted to add modifier {roleID} to the role deck.");
                    return;
                }

                Console.WriteLine("Failed to add a valid random modifier after multiple attempts.");
            }

            public override Tuple<bool, string> Execute(string[] args)
            {
                int count = 1;

                if (args.Length > 0 && int.TryParse(args[0], out int parsedCount))
                {
                    count = Math.Clamp(parsedCount, 1, MaxModifierCount);
                }

                for (int i = 0; i < count; i++)
                {
                    AddRandomModifierToDeck(random);
                }

                return new Tuple<bool, string>(true, $"Attempted to add {count} random modifiers to the role deck.");
            }

            public string GetHelpMessage()
            {
                return "Adds up to 3 random valid modifiers to the role list.";
            }
        }

        public class BanCommand : Command, IHelpMessage
        {
            public BanCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
            {
            }

            private const int MaxBanCount = 3;

            private static readonly List<int> VanillaRoleIDs = [.. Enumerable.Range(1, 56)];

            private static readonly List<int> BTOSRoleIDs = [.. Enumerable.Range(1, 62)];

            public static void BanRandomRoleFromDeck(Random rand)
            {
                var validRoleIDs = Utils.IsBTOS2() ? BTOSRoleIDs : VanillaRoleIDs;
                var currentRoles = Service.Game.Sim.simulation.roleDeckBuilder.Data.roles.Select(role => (int)role).ToHashSet();
                var currentBans = Service.Game.Sim.simulation.roleDeckBuilder.Data.bannedRoles.Select(role => (int)role).ToHashSet();

                const int maxAttempts = 50;

                for (int attempts = 0; attempts < maxAttempts; attempts++)
                {
                    int roleID = validRoleIDs[rand.Next(validRoleIDs.Count)];

                    // Only ban roles not in the deck and not already banned
                    if (!currentRoles.Contains(roleID) && !currentBans.Contains(roleID))
                    {
                        Service.Game.Sim.simulation.AddBannedRoleToRoleDeck((Role)roleID);
                        Console.WriteLine($"Attempted to ban role {roleID} from the role deck.");
                        return;
                    }
                }

                Console.WriteLine("Failed to ban a valid random role after multiple attempts.");
            }



            public override Tuple<bool, string> Execute(string[] args)
            {
                int count = 1;

                if (args.Length > 0 && int.TryParse(args[0], out int parsedCount))
                {
                    count = Math.Clamp(parsedCount, 1, MaxBanCount);
                }

                for (int i = 0; i < count; i++)
                {
                    BanRandomRoleFromDeck(random);
                }

                return new Tuple<bool, string>(true, $"Attempted to ban {count} random roles from the role deck.");
            }

            public string GetHelpMessage()
            {
                return "Bans up to 3 random valid roles from the role list.";
            }
        }

        public class ClearCommand : Command, IHelpMessage
        {
            public ClearCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
            {
            }

            private static void ClearRoleDeck(Random rand)
            {
                Service.Game.Sim.simulation.ClearRoleDeck();

                Console.WriteLine("Role deck reset.");
            }


            public override Tuple<bool, string> Execute(string[] args)
            {
                {
                    ClearRoleDeck(random);
                }

                return new Tuple<bool, string>(true, $"Reset role deck.");
            }

            public string GetHelpMessage()
            {
                return "Clears the role deck.";
            }
        }

        public class FullRandomCommand : Command, IHelpMessage
        {
            public FullRandomCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
            {
            }

            public override Tuple<bool, string> Execute(string[] args)
            {
                int roleCount = 15;
                int modCount = 1;
                int banCount = 1;

                if (args.Length >= 3)
                {
                    int.TryParse(args[0], out roleCount);
                    int.TryParse(args[1], out modCount);
                    int.TryParse(args[2], out banCount);
                }

                roleCount = Math.Clamp(roleCount, 1, 15);
                modCount = Math.Clamp(modCount, 1, 3);
                banCount = Math.Clamp(banCount, 1, 3);

                for (int i = 0; i < modCount; i++)
                    ModifierCommand.AddRandomModifierToDeck(random);

                for (int i = 0; i < roleCount; i++)
                    RoleCommand.AddRandomRoleToDeck(random);

                for (int i = 0; i < banCount; i++)
                    BanCommand.BanRandomRoleFromDeck(random);

                return new Tuple<bool, string>(true, $"Attempted to add {modCount} modifiers, {roleCount} roles, and banned {banCount} roles.");
            }

            public string GetHelpMessage()
            {
                return "Adds random roles, modifiers, and bans at once.";
            }
        }

    }

}
