using HarmonyLib;

namespace RukonMods.TavCRM
{

    [HarmonyPatch(typeof(StatsModel), "AddLog")]
    public static class StatsModel_AddLog_Patch
    {
        static void Postfix(int customerId, LogReason reason)
        {
            if (reason == LogReason.OrderedDrink)
                Stats.CustomerStatsCache.DrinksOrdered++;
        }
    }

    [HarmonyPatch(typeof(SummaryPopup), "Refresh")]
    public static class SummaryPopup_Refresh_Patch
    {
        static void Postfix(SummaryPopup __instance)
        {
            //__instance.commonCustomersLabel.text = $"Total - {__instance.selectedData.CustomersServedCommon} of {CustomerStatsCache.Common}";
            __instance.commonCustomersLabel.text = $"<sprite index=1> - {__instance.selectedData.CustomersServedCommon} of {Stats.CustomerStatsCache.Common}";
            __instance.rareCustomersLabel.text = $"<sprite index=5> - {__instance.selectedData.CustomersServedRare} of {Stats.CustomerStatsCache.Rare}";
            __instance.goldCustomersLabel.text = $"<sprite index=2> - {__instance.selectedData.CustomersServedGold} of {Stats.CustomerStatsCache.Gold}";
            __instance.royalCustomersLabel.text = $"<sprite index=3> - {__instance.selectedData.CustomersServedRoyal} of {Stats.CustomerStatsCache.Royal}";
            //__instance.royalCustomersLabel.text = $"Waiting for Food - {Stats.CustomerStatsCache.OrderedFood} of {Stats.CustomerStatsCache.GotFood}";
            //__instance.royalCustomersLabel.text = $"Waiting for Drink - {Stats.CustomerStatsCache.OrderedDrink} of {Stats.CustomerStatsCache.GotDrink}";
        }
    }
}
