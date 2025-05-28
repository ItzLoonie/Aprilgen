using Server.Shared.State;

namespace Aprilgen
{
    public static class RoleBTOS
    {
        public const Role NONE = Role.NONE;

        // Town
        public const Role ADMIRER = Role.ADMIRER;
        public const Role AMNESIAC = Role.AMNESIAC;
        public const Role BODYGUARD = Role.BODYGUARD;
        public const Role CLERIC = Role.CLERIC;
        public const Role CORONER = Role.CORONER;
        public const Role CRUSADER = Role.CRUSADER;
        public const Role DEPUTY = Role.DEPUTY;
        public const Role INVESTIGATOR = Role.INVESTIGATOR;
        public const Role JAILOR = Role.JAILOR;
        public const Role LOOKOUT = Role.LOOKOUT;
        public const Role MAYOR = Role.MAYOR;
        public const Role MONARCH = Role.MONARCH;
        public const Role PROSECUTOR = Role.PROSECUTOR;
        public const Role PSYCHIC = Role.PSYCHIC;
        public const Role RETRIBUTIONIST = Role.RETRIBUTIONIST;
        public const Role SEER = Role.SEER;
        public const Role SHERIFF = Role.SHERIFF;
        public const Role SPY = Role.SPY;
        public const Role TAVERNKEEPER = Role.TAVERNKEEPER;
        public const Role TRACKER = Role.TRACKER;
        public const Role TRAPPER = Role.TRAPPER;
        public const Role TRICKSTER = Role.TRICKSTER;
        public const Role VETERAN = Role.VETERAN;
        public const Role VIGILANTE = Role.VIGILANTE;

        // Coven
        public const Role CONJURER = Role.CONJURER;
        public const Role COVENLEADER = Role.COVENLEADER;
        public const Role DREAMWEAVER = Role.DREAMWEAVER;
        public const Role ENCHANTER = Role.ENCHANTER;
        public const Role HEXMASTER = Role.HEXMASTER;
        public const Role ILLUSIONIST = Role.ILLUSIONIST;
        public const Role JINX = Role.JINX;
        public const Role MEDUSA = Role.MEDUSA;
        public const Role NECROMANCER = Role.NECROMANCER;
        public const Role POISONER = Role.POISONER;
        public const Role POTIONMASTER = Role.POTIONMASTER;
        public const Role RITUALIST = Role.RITUALIST;
        public const Role VOODOOMASTER = Role.VOODOOMASTER;
        public const Role WILDLING = Role.WILDLING;
        public const Role WITCH = Role.WITCH;

        // Neutral
        public const Role ARSONIST = Role.ARSONIST;
        public const Role BAKER = Role.BAKER;
        public const Role BERSERKER = Role.BERSERKER;
        public const Role DOOMSAYER = Role.DOOMSAYER;
        public const Role EXECUTIONER = Role.EXECUTIONER;
        public const Role JESTER = Role.JESTER;
        public const Role PIRATE = Role.PIRATE;
        public const Role PLAGUEBEARER = Role.PLAGUEBEARER;
        public const Role SERIALKILLER = Role.SERIALKILLER;
        public const Role SHROUD = Role.SHROUD;
        public const Role SOULCOLLECTOR = Role.SOULCOLLECTOR;
        public const Role WEREWOLF = Role.WEREWOLF;

        // Special
        public const Role VAMPIRE = Role.VAMPIRE;
        public const Role CURSED_SOUL = Role.CURSED_SOUL;

        // Modded
        public const Role SOCIALITE = Role.SOCIALITE;
        public const Role MARSHAL = Role.MARSHAL;
        public const Role ORACLE = Role.ORACLE;
        public const Role ROLE_COUNT = Role.ROLE_COUNT;
        public const Role AUDITOR = (Role)58;
        public const Role INQUISITOR = (Role)59;
        public const Role STARSPAWN = (Role)60;
        public const Role ORACLE_MOD = (Role)61;
        public const Role WARLOCK = (Role)62;

        public const Role ROLECOUNT = (Role)63;

        // Buckets
        public const Role TRUEANY = Role.RANDOM_TOWN;
        public const Role ANY = Role.TOWN_INVESTIGATIVE;
        public const Role RANDOMTOWN = Role.TOWN_PROTECTIVE;
        public const Role COMMONTOWN = Role.TOWN_KILLING;
        public const Role TOWNINVESTIGATIVE = Role.TOWN_SUPPORT;
        public const Role TOWNPOWER = Role.TOWN_POWER;
        public const Role TOWNPROTECTIVE = Role.RANDOM_COVEN;
        public const Role TOWNKILLING = Role.COVEN_KILLING;
        public const Role TOWNSUPPORT = Role.COVEN_UTILITY;
        public const Role RANDOMCOVEN = Role.COVEN_DECEPTION;
        public const Role COMMONCOVEN = Role.COVEN_POWER;
        public const Role COVENDECEPTION = Role.RANDOM_NEUTRAL;
        public const Role COVENKILLING = Role.NEUTRAL_KILLING;
        public const Role COVENPOWER = Role.NEUTRAL_EVIL;
        public const Role COVENUTILITY = Role.NEUTRAL_APOCALYPSE;
        public const Role RANDOMAPOCALYPSE = Role.ANY;
        public const Role RANDOMNEUTRAL = (Role)116;
        public const Role NEUTRALEVIL = (Role)117;
        public const Role NEUTRALKILLING = (Role)118;
        public const Role NEUTRALPARIAH = (Role)119;
        public const Role NEUTRALSPECIAL = (Role)120;
        public const Role COMMONNEUTRAL = (Role)121;

        // Modifiers
        public const Role TOWN_VIP = Role.VIP;
        public const Role COVEN_TOWN_TRAITOR = Role.TOWN_TRAITOR;
        public const Role GHOST_TOWN = Role.GHOST_TOWN;
        public const Role NO_TOWN_HANGED = Role.NO_TOWN_HANGED;
        public const Role SLOW_MODE = Role.SLOW_MODE;
        public const Role FAST_MODE = Role.FAST_MODE;
        public const Role ANONYMOUS_VOTES = Role.ANONYMOUS_VOTES;
        public const Role KILLER_ROLES_HIDDEN = Role.KILLER_ROLES_HIDDEN;
        public const Role ROLES_ON_DEATH_HIDDEN = Role.ROLES_ON_DEATH_HIDDEN;
        public const Role ONE_TRIAL_PER_DAY = Role.ONE_TRIAL_PER_DAY;
        public const Role APOC_TOWN_TRAITOR = (Role)211;
        public const Role NECRO_PASSING = (Role)212;
        public const Role TEAMS = (Role)213;
        public const Role ANON_PLAYERS = (Role)214;
        public const Role WALKING_DEAD = (Role)215;
        public const Role EGOTIST_TOWNIE = (Role)216;
        public const Role SPEAKING_SPIRITS = (Role)217;
        public const Role SECRET_OBJECTIVES = (Role)218;
        public const Role NO_LAST_WILLS = (Role)219;
        public const Role IMMOVABLE = (Role)220;
        public const Role COMPLIANT_KILLERS = (Role)221;
        public const Role PANDORAS_BOX = (Role)222;
        public const Role BALLOT_VOTING = (Role)223;
        public const Role INDIVIDUALITY = (Role)224;
        public const Role SNITCH = (Role)225;
        public const Role COVEN_VIP = (Role)226;
        public const Role SECRET_WHISPERS = (Role)227;
        public const Role LOVERS = (Role)228;
        public const Role VC_LOBBY = (Role)229;
        public const Role FEELIN_LUCKY = (Role)230;

        // Special part 2
        public const Role HANGMAN = Role.HANGMAN;
        public const Role HIDDEN = Role.HIDDEN;
        public const Role FAMINE = Role.FAMINE;
        public const Role WAR = Role.WAR;
        public const Role PESTILENCE = Role.PESTILENCE;
        public const Role DEATH = Role.DEATH;
        public const Role STONED = Role.STONED;

        public const Role UNKNOWN = Role.UNKNOWN;
    }

    public static class FactionTypeBTOS
    {
        public const FactionType NONE = FactionType.NONE;
        public const FactionType TOWN = FactionType.TOWN;
        public const FactionType COVEN = FactionType.COVEN;
        public const FactionType SERIALKILLER = FactionType.SERIALKILLER;
        public const FactionType ARSONIST = FactionType.ARSONIST;
        public const FactionType WEREWOLF = FactionType.WEREWOLF;
        public const FactionType SHROUD = FactionType.SHROUD;
        public const FactionType APOCALYPSE = FactionType.APOCALYPSE;
        public const FactionType EXECUTIONER = FactionType.EXECUTIONER;
        public const FactionType JESTER = FactionType.JESTER;
        public const FactionType PIRATE = FactionType.PIRATE;
        public const FactionType DOOMSAYER = FactionType.DOOMSAYER;
        public const FactionType VAMPIRE = FactionType.VAMPIRE;
        public const FactionType CURSED_SOUL = FactionType.CURSED_SOUL;
        public const FactionType UNKNOWN = FactionType.UNKNOWN;
        public const FactionType JACKAL = (FactionType)33;
        public const FactionType FROGS = (FactionType)34;
        public const FactionType LIONS = (FactionType)35;
        public const FactionType HAWKS = (FactionType)36;
        public const FactionType CANNIBAL = (FactionType)37;
        public const FactionType JUDGE = (FactionType)38;
        public const FactionType AUDITOR = (FactionType)39;
        public const FactionType INQUISITOR = (FactionType)40;
        public const FactionType STARSPAWN = (FactionType)41;
        public const FactionType EGOTIST = (FactionType)42;
        public const FactionType PANDORA = (FactionType)43;
        public const FactionType COMPLIANCE = (FactionType)44;
        public const FactionType LOVERS = (FactionType)250;
    }
}
