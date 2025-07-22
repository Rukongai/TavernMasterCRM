using HarmonyLib;
using UnityEngine;

namespace RukonMods.TavCRM
{
    internal class TavernControllerPatches
    {
        [HarmonyPatch(typeof(TavernController), "Init")]
        public static class TavernController_Init_Patch
        {
            static void Postfix()
            {
                if (GameObject.Find("CustomerStatsOverlay") == null) // prevent duplicates
                {
                    GameObject obj = new GameObject("CustomerStatsOverlay");
                    Object.DontDestroyOnLoad(obj);
                    obj.AddComponent<CustomerStatsOverlay>();
                }
            }
        }

        [HarmonyPatch(typeof(TavernController), "InitiateNewDayStart")]
        public static class TavernController_InitiateNewDayStart_Patch
        {
            static void Postfix(TavernController __instance)
            {
                // Reset customer stats at the start of a new day
                Stats.CustomerStatsCache.Reset();
            }
        }
        [HarmonyPatch(typeof(TavernController), "OnApplicationQuit")]
        public static class TavernController_OnApplicationQuit_Patch
        {
            static void Postfix()
            {
                var overlay = GameObject.Find("CustomerStatsOverlay");
                if (overlay != null)
                    Object.Destroy(overlay);
                Stats.CustomerStatsCache.Reset();
            }
        }
    }
}
