using RestSharp.Extensions;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Core.Steam;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace NelPluginsInterface
{
    public class Main : RocketPlugin<NelInterfaceConfig>
    {
        private static DateTime lastUpdated;
        protected override void Load()
        {
            Main.lastUpdated = DateTime.Now;
            Logger.Log("Nel Plugins", ConsoleColor.Red);
      Logger.Log("░░░░░░░░░░░░░░░░░░░░░▄▀░░▌");
      Logger.Log("░░░░░░░░░░░░░░░░░░░▄▀▐░░░▌");
      Logger.Log("░░░░░░░░░░░░░░░░▄▀▀▒▐▒░░░▌");
      Logger.Log("░░░░░▄▀▀▄░░░▄▄▀▀▒▒▒▒▌▒▒░░▌");
      Logger.Log("░░░░▐▒░░░▀▄▀▒▒▒▒▒▒▒▒▒▒▒▒▒█");
      Logger.Log("░░░░▌▒░░░░▒▀▄▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄");
      Logger.Log("░░░░▐▒░░░░░▒▒▒▒▒▒▒▒▒▌▒▐▒▒▒▒▒▀▄");
      Logger.Log("░░░░▌▀▄░░▒▒▒▒▒▒▒▒▐▒▒▒▌▒▌▒▄▄▒▒▐");
      Logger.Log("░░░▌▌▒▒▀▒▒▒▒▒▒▒▒▒▒▐▒▒▒▒▒█▄█▌▒▒▌");
      Logger.Log("░▄▀▒▐▒▒▒▒▒▒▒▒▒▒▒▄▀█▌▒▒▒▒▒▀▀▒▒▐░░░▄");
      Logger.Log("▀▒▒▒▒▌▒▒▒▒▒▒▒▄▒▐███▌▄▒▒▒▒▒▒▒▄▀▀▀▀");
      Logger.Log("▒▒▒▒▒▐▒▒▒▒▒▄▀▒▒▒▀▀▀▒▒▒▒▄█▀░░▒▌▀▀▄▄");
      Logger.Log("▒▒▒▒▒▒█▒▄▄▀▒▒▒▒▒▒▒▒▒▒▒░░▐▒▀▄▀▄░░░░▀");
      Logger.Log("▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▄▒▒▒▒▄▀▒▒▒▌░░▀▄");
      Logger.Log("▒▒▒▒▒▒▒▒▀▄▒▒▒▒▒▒▒▒▀▀▀▀▒▒▒▄▀");
            Logger.Log("Support Server https://discord.gg/2D9VNwcQEB%22");
            Logger.Log("Interface Plugin Loaded", ConsoleColor.Green);
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateHealth += UnturnedPlayerEvents_OnPlayerUpdateHealth;
            UnturnedPlayerEvents.OnPlayerUpdateFood += UnturnedPlayerEvents_OnPlayerUpdateFood;
            UnturnedPlayerEvents.OnPlayerUpdateWater += UnturnedPlayerEvents_OnPlayerUpdateWater;
            UnturnedPlayerEvents.OnPlayerUpdateStamina += UnturnedPlayerEvents_OnPlayerUpdateStamina;
            UnturnedPlayerEvents.OnPlayerUpdateExperience += UnturnedPlayerEvents_OnPlayerUpdateExperience;
            EffectManager.onEffectButtonClicked += OnButtonClicked;
            UnturnedPlayerEvents.OnPlayerUpdatePosition += UnturnedPlayerEvents_OnPlayerUpdatePosition;
        }

        private void UnturnedPlayerEvents_OnPlayerUpdatePosition(UnturnedPlayer player, Vector3 position)
        {
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "NelClockText", DateTime.Now.ToString("HH:mm"));
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "HealthText", "%" + player.Health.ToString());
        }
        private void UnturnedPlayerEvents_OnPlayerUpdateExperience(UnturnedPlayer player, uint experience)
        {
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "NelCashText", experience.ToString());
        }

        public void OnButtonClicked(Player caller, string buttonName)
        {
            UnturnedPlayer NelPlayer = (UnturnedPlayer.FromPlayer(caller));

            if (buttonName == "DiscordButton") // Vote Button
            {
                NelPlayer.Player.sendBrowserRequest("Discord Server", Configuration.Instance.DiscordUrl);
            }

            if (buttonName == "SteamButton")
            {
                NelPlayer.Player.sendBrowserRequest("Steam Link", Configuration.Instance.SteamUrl);
            }

            if(buttonName == "VoteButton")
            {
                NelPlayer.Player.sendBrowserRequest("Vote Link", Configuration.Instance.VoteUrl);
            }
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateStamina(UnturnedPlayer player, byte stamina)
        {
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "StaminaText", "%" + player.Stamina.ToString());
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateWater(UnturnedPlayer player, byte water)
        {
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "WaterText", "%" + player.Thirst.ToString());
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateFood(UnturnedPlayer player, byte food)
        {
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "FoodText", "%" + player.Hunger.ToString());
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateHealth(UnturnedPlayer player, byte health)
        {
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "HealthText",  "%" + player.Health.ToString());
        }

        private void Events_OnPlayerDisconnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(base.Configuration.Instance.EffectId, 9095, true, Provider.clients.Count<SteamPlayer>() - 1 + " / " + Provider.maxPlayers.ToString());
            EffectManager.askEffectClearByID(base.Configuration.Instance.EffectId, player.CSteamID);
        }

        private void Events_OnPlayerConnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(9095, 9095, player.SteamPlayer().transportConnection, false);
            EffectManager.sendUIEffect(base.Configuration.Instance.EffectId, 9095, true, Provider.clients.Count<SteamPlayer>() + " / " + Provider.maxPlayers.ToString());
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "NelCountText", Provider.clients.Count<SteamPlayer>() + " / " + Provider.maxPlayers.ToString());
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowHealth);
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowFood);
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowWater);
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowStamina);
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowVirus);
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowOxygen);
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "HealthText", "%" + player.Health.ToString());
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "FoodText", "%" + player.Hunger.ToString());
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "WaterText", "%" + player.Thirst.ToString());
            EffectManager.sendUIEffectText(9095, player.Player.channel.owner.transportConnection, true, "StaminaText", "%" + player.Stamina.ToString());
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateHealth -= UnturnedPlayerEvents_OnPlayerUpdateHealth;
            UnturnedPlayerEvents.OnPlayerUpdateFood -= UnturnedPlayerEvents_OnPlayerUpdateFood;
            UnturnedPlayerEvents.OnPlayerUpdateWater -= UnturnedPlayerEvents_OnPlayerUpdateWater;
            UnturnedPlayerEvents.OnPlayerUpdateStamina -= UnturnedPlayerEvents_OnPlayerUpdateStamina;
            UnturnedPlayerEvents.OnPlayerUpdateExperience -= UnturnedPlayerEvents_OnPlayerUpdateExperience;
            EffectManager.onEffectButtonClicked -= OnButtonClicked;
            UnturnedPlayerEvents.OnPlayerUpdatePosition -= UnturnedPlayerEvents_OnPlayerUpdatePosition;
        }
    }
}
