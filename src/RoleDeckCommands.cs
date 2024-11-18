using SML;
using System;
using CommandLib.API;
using System.Collections.Generic;
using Services;
using Server.Shared.State;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
namespace RoleDeckCommands;

// TODO: add some sort of thing on the start of the string which controls whether the list is in base or btos2
// then make it convert between roles [and drop unavailable ones] if theyre used interchangably

[Mod.SalemMod]
public class RoleDeckCommands
{
    public static void Start()
    {
        CommandRegistry.AddCommand(new ImportCommand("rolelist", ["rl"]));
        CommandRegistry.AddCommand(new ExportCommand("exportlist", ["el"]));
    }
    public class ExportCommand : Command, IHelpMessage
    {
        public ExportCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
        {
        }
        public override Tuple<bool, string> Execute(string[] args)
        {
            string roleListData = "";

            RoleDeckBuilder RoleDeck = Service.Game.Sim.simulation.roleDeckBuilder;
            //make string to copy in the proper format
            //convert the Role enums to Role IDs and then to strings :)
            string[] roles = RoleDeck.roles.Select(i => ((byte)i).ToString()).ToArray();
            string[] bans = RoleDeck.bannedRoles.Select(i => ((byte)i).ToString()).ToArray();
            string[] modifiers = RoleDeck.modifierCards.Select(i => ((byte)i).ToString()).ToArray();
            roleListData += string.Join(",", roles) + ";";
            roleListData += string.Join(",", bans) + ";";
            roleListData += string.Join(",", modifiers);

            //if playing in BTOS2 add a B to the start of the string
            if (ModStates.IsLoaded("curtis.tuba.better.tos2"))
            {
                Debug.Log("BTOS LOADED IN /EXPORTLIST");
                if (BTOSChecker.CheckBTOSIsModded())
                {
                    roleListData = "B" + roleListData;
                }
            }
            //copy to clipboard
            GUIUtility.systemCopyBuffer = roleListData;
            return new Tuple<bool, string>(true, null);
        }

        public string GetHelpMessage()
        {
            return "Exports the current role list to your clipboard!";
        }
    }
    public class ImportCommand : Command, IHelpMessage
    {
        public ImportCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
        {
        }

        private static void AddRolesToList(string rolestr, List<Role> list, bool isModded, bool isRoleListModded)
        {
            List<string> Roles = new(rolestr.Split(','));
            foreach (string role in Roles)
            {
                //allow using role tags instead, by removing all non-digit characters from the roles
                string rolenum = Regex.Replace(role, "[^0-9]", "");
                if (byte.TryParse(rolenum, out byte roleid))
                {
                    if (isModded != isRoleListModded)
                    {
                        if (isModded)
                        {
                            roleid = RoleConverter.ConvertBaseToBTOS(roleid);
                        } else
                        {
                            roleid = RoleConverter.ConvertBTOSToBase(roleid);
                        }
                    }
                    //RoleConverter returns 0 when the role doesnt exist in the specified environment.
                    if (roleid != 0)
                    {
                        list.Add((Role)roleid);
                    }
                }
            }
        }
        public override Tuple<bool, string> Execute(string[] args)
        {
            if (args.Length < 1) return new Tuple<bool, string>(false, "Usage example: /rolelist [role list exported with /exportlist]");

            //join them
            string RoleListArg = string.Join("", args);
            List<string> RoleLists = new(RoleListArg.Split(";"));

            //RoleLists[0] is roles
            //RoleLists[1] is bans
            //RoleLists[3] is modifiers
            List<Role> Roles = new();
            List<Role> Bans = new();
            List<Role> Modifiers = new();

            bool isModded = false;
            bool isRoleListModded = RoleListArg.StartsWith("B");

            if (ModStates.IsLoaded("curtis.tuba.better.tos2"))
            {
                Debug.Log("BTOS LOADED IN /ROLELIST");
                isModded = BTOSChecker.CheckBTOSIsModded();
            }
            
            for (int i = 0; i < RoleLists.Count; i++)
            {
                List<Role> RoleList;
                switch (i)
                {
                    case 0:
                        RoleList = Roles;
                        break;
                    case 1:
                        RoleList = Bans;
                        break;
                    case 2:
                        RoleList = Modifiers;
                        break;
                    default:
                        RoleList = Modifiers;
                        break;
                }
                AddRolesToList(RoleLists[i], RoleList, isModded, isRoleListModded);
            }
            Service.Game.Sim.simulation.ClearRoleDeck();
            // There are issues with this approach, until they are fixed we need to send them individually
            // Doing it that way avoids an issue that WILL GET YOUR ACCOUNT SUSPENDED IF YOU ABUSE IT.
            // Service.Game.Sim.simulation.SendFullRoleDeck(Roles, Bans, Modifiers);

            // Hacky quick fix instead bc im BUSY
            // I checked and this avoids the issue in both base game and btos2 :)
            foreach (Role role in Roles)
            {
                Service.Game.Sim.simulation.AddRoleToRoleDeck(role);
            }
            foreach (Role role in Bans)
            {
                Service.Game.Sim.simulation.AddBannedRoleToRoleDeck(role);
            }
            foreach (Role role in Modifiers)
            {
                Service.Game.Sim.simulation.AddRoleToRoleDeck(role);
            }
            return new Tuple<bool, string>(true, null);
        }

        public string GetHelpMessage()
        {
            return "Import a custom role list! the format as follows [list of role tags separated by commas];[list of role tags to ban separated by commas];[list of modifier ids separated by commas]";
        }
    }
}
