public class RoleConverter
{
    public static byte ConvertBTOSToBase(byte role)
    {
        switch (role)
        {
            //marshal
            case 56:
                return 55;

            //oracle -> town protective
            case 61:
                return 102;

            //common neutral -> neutral evil
            case 121:
            //neutral pariah -> neutral evil
            case 119:
            //starspawn -> neutral evil
            case 60:
            //auditor -> neutral evil
            case 58:
            //judge -> neutral evil
            case 57:
            //inquisitor -> neutral evil
            case 59:
                return 113;

            //neutral special -> random neutral
            case 120:
            //jackal -> random neutral
            case 55:
                return 111;

            //banshee -> coven deception
            case 54:
                return 109;

            //any -> any
            case 101:
            case 100:
                return 115;

            //random town
            case 102:
                return 100;
            //town investigative
            case 104:
                return 101;
            //town protective
            case 106:
                return 102;
            //town killing
            case 107:
                return 103;
            //town support
            case 108:
                return 104;
            //town power matches up
            //random coven
            case 109:
                return 106;
            //coven killing
            case 112:
                return 107;
            //coven utility
            case 114:
                return 108;
            //coven deception
            case 111:
                return 109;
            //coven power
            case 113:
                return 110;
            //random neutral
            case 116:
                return 111;
            //neutral killing
            case 118:
                return 112;
            //neutral evil
            case 117:
                return 113;
            //neutral apoc
            case 115:
                return 114;
            //common town
            case 103:
                return 116;
            //common coven
            case 110:
                return 117;

            //apoc town traitor -> void
            case 211:
            //necro passing -> void
            case 212:
            //teams -> void
            case 213:
            //anon players -> void
            case 214:
            //walking dead -> void
            case 215:
            //egotist townie -> void
            case 216:
            //sneaking spirits -> void
            case 217:
            //secret objectives -> void
            case 218:
            //no last wills -> void
            case 219:
            //immovable -> void
            case 220:
            //compliant killers -> void
            case 221:
            //pandoras box -> void
            case 222:
            //ballot voting -> void
            case 223:
            //individuality -> void
            case 224:
            //snitch -> void
            case 225:
            //coven vip -> void
            case 226:
            //secret whispers -> void
            case 227:
                return 0;
            default:
                return role;
        }
    }

    public static byte ConvertBaseToBTOS(byte role)
    {
        switch (role)
        {
            //socialite -> town support
            case 54:
                return 108;
            //marshal
            case 55:
                return 56;
            //any
            //maybe change to True Any bc pirate?
            //cursed soul also doesnt show up here tho
            case 115:
                return 101; 
            //random town
            case 100:
                return 102;
            //town investigative
            case 101:
                return 104;
            //town protective
            case 102:
                return 106;
            //town killing
            case 103:
                return 107;
            //town support
            case 104:
                return 108;
            //town power matches up
            //random coven
            case 106:
                return 109;
            //coven killing
            case 107:
                return 112;
            //coven utility
            case 108:
                return 114;
            //coven deception
            case 109:
                return 111;
            //coven power
            case 110:
                return 113;
            //random neutral
            case 111:
                return 116;
            //neutral killing
            case 112:
                return 118;
            //neutral evil
            case 113:
                return 117;
            //neutral apoc
            case 114:
                return 115;
            //common town
            case 116:
                return 103;
            //common coven
            case 117:
                return 110;
                
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
            default:
                return role;
        }
    }
}
