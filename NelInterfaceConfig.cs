using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NelPluginsInterface
{
    public class NelInterfaceConfig : IRocketPluginConfiguration
    {
        public ushort EffectId = 9095;
        public string DiscordUrl;
        public string SteamUrl;
        public string VoteUrl;
        public string NelPluginsDiscord;
        public void LoadDefaults()
        {
            this.EffectId = 9095;
            DiscordUrl = "YourDiscordServerURL";
            SteamUrl = "URL";
            VoteUrl = "VoteServerURL";
            NelPluginsDiscord = "https://discord.gg/BfwKmCKCKZ";
        }
    }
}
