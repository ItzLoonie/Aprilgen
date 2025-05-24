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
        public class RoleCommand(string name, string[] aliases = null, string harmonyId = null) : Command(name, aliases, harmonyId), IHelpMessage
        {
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
                return "Format: /randomrole [roles] - Adds up to 15 random valid roles to the role list.";
            }
        }

        public class ModifierCommand(string name, string[] aliases = null, string harmonyId = null) : Command(name, aliases, harmonyId), IHelpMessage
        {
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
                return "Format: /randommodifier [modifiers] - Adds up to 3 random valid modifiers to the role list.";
            }
        }

        public class BanCommand(string name, string[] aliases = null, string harmonyId = null) : Command(name, aliases, harmonyId), IHelpMessage
        {
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
                return "Format: /randomban [bans] - Bans up to 3 random valid roles from the role list.";
            }
        }

        public class ClearCommand(string name, string[] aliases = null, string harmonyId = null) : Command(name, aliases, harmonyId), IHelpMessage
        {
            public static void ClearRoleDeck()
            {
                Service.Game.Sim.simulation.ClearRoleDeck();

                Console.WriteLine("Role deck reset.");
            }


            public override Tuple<bool, string> Execute(string[] args)
            {
                {
                    ClearRoleDeck();
                }

                return new Tuple<bool, string>(true, $"Reset role deck.");
            }

            public string GetHelpMessage()
            {
                return "Format: /clear - Clears the role deck.";
            }
        }

        public class FullRandomCommand(string name, string[] aliases = null, string harmonyId = null) : Command(name, aliases, harmonyId), IHelpMessage
        {
            public override Tuple<bool, string> Execute(string[] args)
            {
                int modifiersMax = ModSettings.GetInt("Maximum Modifiers", "loonie.jestersgambit");
                int bansMax = ModSettings.GetInt("Maximum Bans", "loonie.jestersgambit");

                int maxModifiers = !Utils.IsBTOS2() && modifiersMax > 3 ? 3 : modifiersMax;
                int maxBans = !Utils.IsBTOS2() && bansMax > 3 ? 3 : bansMax;

                int roleCount = 20;
                int modifierCount = random.Next(0, maxModifiers + 1);
                int banCount = random.Next(0, maxBans + 1);

                ClearCommand.ClearRoleDeck();

                if (args.Length > 0 && int.TryParse(args[0], out int parsedRoles))
                    roleCount = Math.Clamp(parsedRoles, 1, 20);

                if (args.Length > 1 && int.TryParse(args[1], out int parsedMods))
                    modifierCount = Math.Clamp(parsedMods, 0, maxModifiers);

                if (args.Length > 2 && int.TryParse(args[2], out int parsedBans))
                    banCount = Math.Clamp(parsedBans, 0, maxBans);

                for (int i = 0; i < modifierCount; i++)
                    ModifierCommand.AddRandomModifierToDeck(random);

                for (int i = 0; i < roleCount; i++)
                    RoleCommand.AddRandomRoleToDeck(random);

                for (int i = 0; i < banCount; i++)
                    BanCommand.BanRandomRoleFromDeck(random);

                string[] titles =
                [
                    // Keywords
                    "Conjure", "Blood Ritual", "Doom", "Haunt", "Ignite", "Cautious", "Bite", "Convert", "Counter Claim",
                    "Crime", "Care", "Dreamweave", "Torment", "Magic Mirror", "Rapid Mode", "Ghost Town", "Build", "Rebecca",
                    "Death Roll", "Death Guess", "Spread", "Perfect Town", "Slow Mode", "Fast Mode", "Anonymous Voting", "Secret Killers",
                    "Hidden Roles", "One Trial", "Unique", "Murder", "Claim", "Town Elder", "No Crime", "Vote For Alignment",
                    "Trespassing", "Unleash", "Curtis", "Group Lynch", "Aegis", "Shriek", "Deafen", "Smokebomb", "Audit", "L", "W",
                    "Vanquish", "Inquire", "Heretic", "Recruit", "Assassinate", "Court", "Subpoena", "Daybreak", "Isolate", "Starbound",
                    "Anon Players", "Town Traitor", "Necro Passing", "Teams", "Walking Dead", "Real", "Fake", "Snitch", "Compliant Killers",
                    "Secret Whispers", "Speaking Spirits", "Pandora's Box", "Egotist Townie", "No Last Wills", "Immovable",
                    "Individuality", "Unknown Obstacle", "Known Obstacle", "Curse", "Mass Hysteria", "Safe", "Drain", "Godframer",
                    "Soul Swap", "Lovers", "VC Lobby", "Illuminated", "Load", "Camouflage", "Chatterbox", "Feelin' Lucky?", "Basic",
                    "Powerful", "Unstoppable", "Ethereal", "Invincible", "Plague", "Stone Gaze", "Starve", "Investigate", "Duel",
                    "Jail", "Rampage", "Control", "Trap", "Douse", "Hex", "Astral", "Necronomicon", "Stoned", "Harmful", "Armageddon",
                    "Silence", "Enchant", "Illusion", "Insane", "Knight", "Hungover", "Roleblock", "Barrier", "Revealed", "Bestow",
                    "Bread", "Poison", "Accompany", "Scurvy", "Good", "Evil", "Sus", "Not Sus", "Hang", "Hangman", "Vote", "Guilty",
                    "Abstain", "Innocent", "Attack", "Defense", "Autopsy", "Protect", "Visit", "VIP", "Examine", "Watch", "Vision",
                    "Gaze", "Intuit", "Search", "Bug", "Track", "Execute", "Prosecute", "Fortify", "Guard", "Shoot", "Alert",
                    "Remember", "Raise", "Drink", "Retrain", "Reveal", "Sense", "Plunder", "Scour", "Party", "Guest List",
                    "Propose", "Landlubber", "Insomnia",

                    // Roles
                    "Admirer", "Amnesiac", "Bodyguard", "Cleric", "Coroner", "Crusader", "Deputy", "Investigator", "Jailor",
                    "Lookout", "Mayor", "Monarch", "Prosecutor", "Psychic", "Retributionist", "Seer", "Sheriff",
                    "Spy", "Tavern Keeper", "Tracker", "Trapper", "Trickster", "Veteran", "Vigilante",
                    "Conjurer", "Coven Leader", "Dreamweaver", "Enchanter", "Hex Master", "Illusionist", "Jinx",
                    "Medusa", "Necromancer", "Poisoner", "Potion Master", "Ritualist", "Voodoo Master",
                    "Wildling", "Witch",
                    "Arsonist", "Baker", "Berserker", "Doomsayer", "Executioner", "Jester", "Pirate",
                    "Plaguebearer", "Serial Killer", "Shroud", "Soul Collector", "Werewolf", "Vampire",
                    "Cursed Soul", "Banshee", "Jackal", "Marshal", "Judge", "Auditor", "Inquisitor", "Starspawn",
                    "Oracle", "Warlock", "Socialite", "War", "Famine", "Pestilence", "Death", "Stoned", "Hidden",

                    // Factions
                    "Town", "Coven", "Apocalypse", "Pandora", "Egotist", "Compliance", "Lovers", "Frogs", "Lions", "Hawks",

                    // Skins
                    "John", "Macy", "Deodat", "Mary", "Giles", "Jack", "Brokk", "Artemys", "Francisco", "Avery",
                    "Jackie", "Davey", "Catherine", "Martha", "Samuel", "Liric", "Grim", "Blyte", "Bridget", "Lupin",
                    "Vladimir", "Nikki", "Petra", "Thomas", "Rosemary", "Gerald", "Robert", "Shinrin", "Famine", "War",
                    "Pestilence", "Betty", "Widow", "White Witch", "Coven Leader", "Husky", "Anubis", "Dusty", "Lockwood",
                    "Shadow Wolf", "Sabrina", "Grave Digger", "Nevermore", "Headless Horseman", "Iron Chef", "Firebug",
                    "Kande", "Odin", "Krampus", "Jekyll", "Hyde", "Clef", "Piper", "Archibald", "Lauf", "Giles Quarry", "Gorgon",
                    "Radu", "Ivy", "Sun Wukong", "Duchess", "McBrains", "Joao", "Sister", "Tabitha", "Midknight", "Poisoner",
                    "Summer", "Helsing", "Glinda", "Blueflame", "Hermes", "Sisyphus", "Zeus", "Spartan", "Pillarman",
                    "Minotaur", "Cupid", "Heartless Horseman", "Drachen", "Cat Jester", "Jestilence",

                    // Pets
                    "Pale Horse", "Black Cat", "White Wolf", "Gobby", "Bessie", "Cob", "Poe", "Hammond", "Coyote",
                    "Mushruwum", "Demon Pup", "Polly", "Jack-o'-lantern", "Sir Froglington", "Spike", "Sir Gobbleston",
                    "Iceberg", "Shimmerscale", "Satrio", "Cthulhu", "Grimmie", "Necronomicon", "Woolums", "Chameleon",
                    "Gryphon", "Hipgrook", "Blicks", "Thunder Bird", "Pegasus", "Phoenix", "Deer", "Baby Dragon", "Monkey Box",

                    // References
                    "Kidnapper", "Handler", "Medium", "Transporter", "Agent", "Godfather", "Mafioso", "Framer",
                    "Disguiser", "Hypnotist", "Consigliere", "Forger", "Mafia", "Escort", "Consort", "Bootlegger",
                    "Vampire Hunter", "Beguiler", "Doctor", "Janitor", "Blackmailer", "Guardian Angel", "Survivor",
                    "Juggernaut", "Altruist", "Imitator", "Hunter", "The", "Glitch", "Medic",

                    // Colors
                    "Red", "Blue", "Green", "Pink", "Purple", "Orange", "Yellow", "White", "Black", "Brown", "Gray",
                    "Magenta", "Cyan", "Rose", "Maroon", "Tan", "Olive", "Banana", "Indigo", "Teal", "Violet",

                    // Other
                    "waga", "baba", "bobo", "Tuba", "Bald", "Loonie", "Dyl", "cag", "Any", "Banned", "Ravens", "Iguanas",
                    "amogus", "April", "Revolution", "Steve", "Chicken", "Jockey", "Bater", "Water", "Horseman", "of",
                    "Wucket", "Bucket", "Release", "Flint", "and", "Steel", "Nether", "Dennis", "Hamburger", "Salem",
                    "Fire", "Thunder", "Hammer", "Jump", "High", "Spin", "Tickle", "Rework", "Overhaul", "Buff", "Nerf",
                    "Craft", "Mine", "Cheese", "Fork", "Traitor", "King", "Queen", "Chaos", "Corvus", "Viking", "Confirmed",
                    "Mode", "Token", "Fool", "Fortune", "Slow", "Fast", "Rapid", "Horsemen", "Shield", "Deck", "Role", "Modifier",
                    "Exposed", "Lightly", "Weathered", "Cut", "Copper", "Stairs", "Draw", "Match", "Player", "Squid", "Bomb",
                    "Crewmate", "Impostor", "Random", "Common", "Power", "Killing", "Protective", "Support", "Special", "Pariah",
                    "Neutral", "True", "Utility", "Deception", "Forgor", "Scam", "Discord", "Puppeteer", "Master", "Puppet", "Target",
                    "Reanimate", "Math", "Science", "Time", "Travel", "Diamond", "Emerald", "Redstone", "Bluestone", "Quartz", "Iron",
                    "Gold", "Purpur", "Chorus", "Conquest", "Vanilla", "Better"
                ];


                var shuffled = titles.OrderBy(x => random.Next()).ToArray();
                string text1 = shuffled[0];
                string text2 = shuffled[1];
                string text3 = shuffled[2];

                Random r = new();
                int lobbyIcon = r.Next(0, 246); 
                if (ModSettings.GetBool("Randomize Lobby Info", "loonie.jestersgambit")) Service.Game.Sim.simulation.SetLobbyInfo(lobbyIcon, $"{text1} {text2} {text3}");


                return new Tuple<bool, string>(true,
                    $"Attempted to add {roleCount} roles, {modifierCount} modifiers, and ban {banCount} roles.");
            }


            public string GetHelpMessage()
            {
                return "Format: /randomall [roles] [modifiers] [bans] - Sets a role deck with random roles, modifiers, and bans.";
            }
        }

    }

}
