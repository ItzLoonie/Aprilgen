using System;
using Home.Common;
using Mentions.UI;
using Server.Shared.Messages;
using Server.Shared.State;
using Server.Shared.State.Chat;
using Services;
using SML;

namespace Aprilgen
{
    public static class Utils
    {
        public static bool BTOS2Exists()
        {
            return ModStates.IsEnabled("curtis.tuba.better.tos2");
        }
        public static bool IsBTOS2()
        {
            bool result;
            try
            {
                result = Utils.IsBTOS2Bypass();
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private static bool IsBTOS2Bypass()
        {
            return BTOS2Exists() && BetterTOS2.BTOSInfo.IS_MODDED;
        }

        public static void AddMessage(string message, string style = "", bool playSound = true, bool stayInChatlogs = true, bool showInChat = true)
        {
            var mentionPanel = UnityEngine.Object.FindObjectOfType<MentionPanel>();
            if (mentionPanel != null)
            {
                var decodedMessage = mentionPanel.mentionsProvider.DecodeText(message);
                var chatLogEntry = new ChatLogCustomTextEntry(decodedMessage, style)
                {
                    showInChatLog = stayInChatlogs,
                    showInChat = showInChat
                };

                var chatLogMessage = new ChatLogMessage(chatLogEntry);
                Service.Game.Sim.simulation.HandleChatLog(chatLogMessage);

                if (playSound)
                {
                    PlayUISound("Audio/UI/Error");
                }
            }
        }

        public static void AddFeedbackMsg(string message, string feedbackMessageType = "normal", bool playSound = true)
        {
            try
            {
                var mentionPanel = UnityEngine.Object.FindObjectOfType<MentionPanel>();
                if (mentionPanel != null)
                {
                    var decodedMessage = mentionPanel.mentionsProvider.DecodeText(message);
                    var feedbackType = StringToFeedbackType(feedbackMessageType);
                    var feedbackEntry = new ChatLogClientFeedbackEntry(feedbackType, decodedMessage);

                    var chatLogMessage = new ChatLogMessage { chatLogEntry = feedbackEntry };
                    Service.Game.Sim.simulation.incomingChatMessage.ForceSet(chatLogMessage);

                    if (playSound)
                    {
                        PlayUISound("Audio/UI/Error");
                    }
                }
            }
            catch
            {
            }
        }

        private static void PlayUISound(string soundPath)
        {
            var uiController = UnityEngine.Object.FindObjectOfType<UIController>();
            uiController?.PlaySound(soundPath, false);
        }

        public static ClientFeedbackType StringToFeedbackType(string str)
        {
            ClientFeedbackType result;
            if (!(str == "normal"))
            {
                if (!(str == "info"))
                {
                    if (!(str == "warning"))
                    {
                        if (!(str == "critical"))
                        {
                            if (!(str == "success"))
                            {
                                result = ClientFeedbackType.Normal;
                            }
                            else
                            {
                                result = ClientFeedbackType.Success;
                            }
                        }
                        else
                        {
                            result = ClientFeedbackType.Critical;
                        }
                    }
                    else
                    {
                        result = ClientFeedbackType.Warning;
                    }
                }
                else
                {
                    result = ClientFeedbackType.Info;
                }
            }
            else
            {
                result = ClientFeedbackType.Normal;
            }
            return result;
        }

        public static bool RoleExists(Role role) => Service.Game.Sim.simulation.roleDeckBuilder.Data.roles.Contains(role);
        public static bool BanExists(Role role) => Service.Game.Sim.simulation.roleDeckBuilder.Data.bannedRoles.Contains(role);
        public static bool ModifierExists(Role role) => Service.Game.Sim.simulation.roleDeckBuilder.Data.modifierCards.Contains(role);
    }
}