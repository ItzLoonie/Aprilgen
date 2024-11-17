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

namespace RoleDeckCommands;

[Mod.SalemMod]
public class SlashRename
{
    public static void Start()
    {
        CommandRegistry.AddCommand(new RenameCommand("rolelist", ["rl"]));
    }

    public class RenameCommand : Command, IHelpMessage
    {
        public RenameCommand(string name, string[] aliases = null, string harmonyId = null) : base(name, aliases, harmonyId)
        {
        }

        public override Tuple<bool, string> Execute(string[] args)
        {
            if (args.Length < 1) return new Tuple<bool, string>(false, "Usage example: /rolelist (role ids separated by commas) (banned role ids separated by commas) (");
            List<Role> Roles = new();
            List<Role> Bans = new();
            List<Role> HostOptions = new();
            foreach (string role in args[0].Split(','))
            {
                if (byte.TryParse(role, out byte RoleID))
                {
                    Roles.Add((Role)RoleID);
                }
            }

            if (args.Length > 1)
            {
                foreach (string role in args[1].Split(','))
                {
                    if (byte.TryParse(role, out byte RoleID))
                    {
                        Bans.Add((Role)RoleID);
                    }
                }
            }

            if (args.Length > 2)
            {
                foreach (string role in args[2].Split(','))
                {
                    if (byte.TryParse(role, out byte RoleID))
                    {
                        HostOptions.Add((Role)RoleID);
                    }
                }
            }

            Service.Game.Sim.simulation.SendFullRoleDeck(Roles, Bans, HostOptions);
            return new Tuple<bool, string>(true, null);
        }

        public string GetHelpMessage()
        {
            return "Control the role deck list for a Custom game. Usage example: /rolelist (role ids separated by spaces)";
        }
    }
}
