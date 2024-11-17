using SML;
using System;
using CommandLib.API;
using CommandLib;
using System.Collections.Generic;
using Services;
using UnityEngine;
using Server.Shared.Extensions;
using Server.Shared.State;
using Server.Shared.Info;
using Game.Simulation;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Data;

namespace RoleDeckCommands;

[Mod.SalemMod]
public class SlashRename
{
    public static void Start()
    {
        CommandRegistry.AddCommand(new ImportCommand("rolelist", ["rl"]));
    }

    public class ImportCommand : Command, IHelpMessage
    {
        public ImportCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
        {
        }

        private static void AddRolesToList(string rolestr, List<Role> list)
        {
            List<string> Roles = new(rolestr.Split(','));
            foreach (string role in Roles)
            {
                string rolenum = Regex.Replace(role, "[^0-9]", "");
                if (byte.TryParse(rolenum, out byte roleid))
                {
                    list.Add((Role)roleid);
                }
            }
        }
        public override Tuple<bool, string> Execute(string[] args)
        {
            if (args.Length < 1) return new Tuple<bool, string>(false, "Usage example: /rolelist (role tags separated by commas);(bans separated by commas);(modifiers separated by commas)");
           
            //join them
            string RoleListArg = string.Join("", args);
            List<string> RoleLists = new(RoleListArg.Split(";"));

            //RoleLists[0] is roles
            //RoleLists[1] is bans
            //RoleLists[3] is modifiers
            List<Role> Roles = new();
            List<Role> Bans = new();
            List<Role> Modifiers = new();
            
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
                AddRolesToList(RoleLists[i], RoleList);
            }
            Service.Game.Sim.simulation.ClearRoleDeck();
            //There are issues with this approach, until they are fixed we need to send them individually
            //Doing it that way avoids an issue that WILL GET YOUR ACCOUNT SUSPENDED IF YOU ABUSE IT.
            //Service.Game.Sim.simulation.SendFullRoleDeck(Roles, Bans, Modifiers);
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
