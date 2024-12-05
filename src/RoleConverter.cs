using Server.Shared.State;
using BetterTOS2;
public class RoleConverter
{
    public static byte ConvertBTOSToBase(byte role) => role switch
    {
        //I can't reference RolePlus here because this code can be run in a non-btos2 state

        // these don't exist in base so they need to be given a bucket instead [except warlock->sc]
        // oracle -> tp
        61 => (byte)Role.TOWN_PROTECTIVE,

        // warlock -> sc
        62 => (byte)Role.SOULCOLLECTOR,

        // common neutral -> neutral evil
        121 => (byte)Role.NEUTRAL_EVIL,

        // neutral pariah -> neutral evil
        119 => (byte)Role.NEUTRAL_EVIL,

        // starspawn -> neutral evil
        60 => (byte)Role.NEUTRAL_EVIL,

        // auditor -> neutral evil
        58 => (byte)Role.NEUTRAL_EVIL,

        // judge -> neutral evil
        57 => (byte)Role.NEUTRAL_EVIL,

        // inquisitor -> neutral evil
        59 => (byte)Role.NEUTRAL_EVIL,
        
        // neutral special -> random neutral
        120 => (byte)Role.RANDOM_NEUTRAL,

        // jackal -> random neutral
        55 => (byte)Role.RANDOM_NEUTRAL,

        // banshee -> coven deception
        54 => (byte)Role.COVEN_DECEPTION,

        // now the stuff that actually exists and just needs to be converted

        //marshal
        56 => (byte)Role.MARSHAL,

        // any/true any -> any
        101 => (byte)Role.ANY,
        100 => (byte)Role.ANY,

        // random town
        102 => (byte)Role.RANDOM_TOWN,

        // town investigative
        104 => (byte)Role.TOWN_INVESTIGATIVE,

        // town protective
        106 => (byte)Role.TOWN_PROTECTIVE,

        // town killing
        107 => (byte)Role.TOWN_KILLING,

        // town support
        108 => (byte)Role.TOWN_SUPPORT,

        // town power matches up

        // random coven
        109 => (byte)Role.RANDOM_COVEN,

        // coven killing
        112 => (byte)Role.COVEN_KILLING,

        // coven utility
        114 => (byte)Role.COVEN_UTILITY,

        // coven deception
        111 => (byte)Role.COVEN_DECEPTION,

        // coven power
        113 => (byte)Role.COVEN_POWER,

        // random neutral
        116 => (byte)Role.RANDOM_NEUTRAL,

        // neutral killing
        118 => (byte)Role.NEUTRAL_KILLING,

        // neutral evil
        117 => (byte)Role.NEUTRAL_EVIL,

        // neutral apoc
        115 => (byte)Role.NEUTRAL_APOCALYPSE,

        // common town
        103 => (byte)Role.COMMON_TOWN,

        // common coven
        110 => (byte)Role.COMMON_COVEN,

        //apoc town traitor -> void
        211 => 0,
        //necro passing -> void
        212 => 0,
        //teams -> void
        213 => 0,
        //anon players -> void
        214 => 0,
        //walking dead -> void
        215 => 0,
        //egotist townie -> void
        216 => 0,
        //sneaking spirits -> void
        217 => 0,
        //secret objectives -> void
        218 => 0,
        //no last wills -> void
        219 => 0,
        //immovable -> void
        220 => 0,
        //compliant killers -> void
        221 => 0,
        //pandoras box -> void
        222 => 0,
        //ballot voting -> void
        223 => 0,
        //individuality -> void
        224 => 0,
        //snitch -> void
        225 => 0,
        //coven vip -> void
        226 => 0,
        //secret whispers -> void
        227 => 0,
        
        // default case, just return the same role id
        _ => role,
    };

    public static byte ConvertBaseToBTOS(byte role) => role switch
    {
        //I CAN reference roleplus here as this code is only ever run in a btos2 state :D

        //socialite -> town support
        (byte)Role.SOCIALITE => (byte)RolePlus.TOWN_SUPPORT,

        //marshal
        (byte)Role.MARSHAL => (byte)RolePlus.MARSHAL,

        //any
        //maybe change to True Any bc pirate?
        //cursed soul also doesnt show up here tho
        (byte)Role.ANY => (byte)RolePlus.ANY,

        //random town
        (byte)Role.RANDOM_TOWN => (byte)RolePlus.RANDOM_TOWN,

        //town investigative
        (byte)Role.TOWN_INVESTIGATIVE => (byte)RolePlus.TOWN_INVESTIGATIVE,

        //town protective
        (byte)Role.TOWN_PROTECTIVE => (byte)RolePlus.TOWN_PROTECTIVE,

        //town killing
        (byte)Role.TOWN_KILLING => (byte)RolePlus.TOWN_KILLING,

        //town support
        (byte)Role.TOWN_SUPPORT => (byte)RolePlus.TOWN_SUPPORT,

        //town power matches
    
        //random coven
        (byte)Role.RANDOM_COVEN => (byte)RolePlus.RANDOM_COVEN,

        //coven killing
        (byte)Role.COVEN_KILLING => (byte)RolePlus.COVEN_KILLING,

        //coven utility
        (byte)Role.COVEN_UTILITY => (byte)RolePlus.COVEN_UTILITY,
        
        //coven deception
        (byte)Role.COVEN_DECEPTION => (byte)RolePlus.COVEN_DECEPTION,

        //coven power
        (byte)Role.COVEN_POWER => (byte)RolePlus.COVEN_POWER,
        //random neutral
        (byte)Role.RANDOM_NEUTRAL => (byte)RolePlus.RANDOM_NEUTRAL,

        //neutral killing
        (byte)Role.NEUTRAL_KILLING => (byte)RolePlus.NEUTRAL_KILLING,

        //neutral evil
        (byte)Role.NEUTRAL_EVIL => (byte)RolePlus.NEUTRAL_EVIL,

        //neutral apoc
        (byte)Role.NEUTRAL_APOCALYPSE => (byte)RolePlus.RANDOM_APOCALYPSE,

        //common town
        (byte)Role.COMMON_TOWN => (byte)RolePlus.REGULAR_TOWN,

        //common coven
        (byte)Role.COMMON_COVEN => (byte)RolePlus.REGULAR_COVEN,
                
        //vip matches
        //town traitor matches
        //ghost town matches
        //perfect town matches
        //slow mode matches
        //fast mode matches
        //anon voting matches
        //secret killers matches
        //hidden roles matches
        //one trial per day matches

        // default case, just return the same role id
        _ => role,
  };
}
