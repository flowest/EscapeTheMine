using System;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Main
    {
        public static XmlDocument configXML;
        private static MinerConfigData minerConfigData;

        private static int killedBats = 0;

        public static void loadConfigXML()
        {
            configXML = new XmlDocument();
            configXML.Load("Assets/Config/config.xml");
            XmlNode minerSettings = configXML.SelectNodes("Settings/Miner")[0];

            if (minerSettings != null)
            {
                int minerHealth = Convert.ToInt32(minerSettings.SelectSingleNode("Health").InnerText);
                int minerWalkSpeed = Convert.ToInt32(minerSettings.SelectSingleNode("WalkSpeed").InnerText);
                int minerMaxStamina = Convert.ToInt32(minerSettings.SelectSingleNode("MaxStamina").InnerText);
                int minerThrowForce = Convert.ToInt32(minerSettings.SelectSingleNode("ThrowForce").InnerText);

                minerConfigData = new MinerConfigData(minerHealth,minerWalkSpeed,minerMaxStamina,minerThrowForce);
            }
        }

        public static MinerConfigData getConfigData()
        {
            return minerConfigData;
        }

        public static void killedBat()
        {
            killedBats++;
        }

        public static int getKilledBats()
        {
            return killedBats;
        }
    }
}
