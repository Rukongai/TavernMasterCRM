using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace RukonMods.TavCRM
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class TavCRM : BaseUnityPlugin
    {
        private const string MyGuid = "com.rukonmods.tavcrm";
        private const string PluginName = "TavCRM";
        private const string VersionString = "1.0.3";

        private static readonly Harmony Harmony = new Harmony(MyGuid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
            Log = Logger;
        }
    }
}
