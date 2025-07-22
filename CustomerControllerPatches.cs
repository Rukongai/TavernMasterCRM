using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using static TavernData;
namespace RukonMods.TavCRM
{
    internal class CustomerControllerPatches
    {
        public static class ModData
        {
            public static CustomerSpawner Spawner;
        }

        [HarmonyPatch(typeof(CustomerSpawner), "Init")]
        public static class CustomerSpawner_Init_Patch
        {
            static void Postfix(CustomerSpawner __instance)
            {
                ModData.Spawner = __instance;
            }
        }
        [HarmonyPatch(typeof(CustomerSpawner), "ActivateCustomer")]
        public static class CustomerSpawner_ActivateCustomer_Patch
        {
            static void Postfix(CustomerSpawner __instance, CustomerController customer, bool __result)
            {
                if (__result)
                {
                    Stats.CustomerStatsCache.Total.Add(customer);
                    Stats.CustomerStatsCache.Increment(customer.CustType);
                    Stats.CustomerStatsCache.customerCounter = __instance.customerCounter;
                    Stats.CustomerStatsCache.Seated = __instance.CustomersWaitingToBeSeated.Count;
                    if (customer.IsGoingToHotel)
                    {
                        Stats.CustomerStatsCache.HotelCustomer++;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(StatsModel), "OnOrderCompleted")]
        public static class StatsModel_OnOrderCompleted_Patch
        {
            static void Postfix(StatsModel __instance, OrdersModel.Order order)
            {
                if (order.OrderType == OrdersModel.OrderType.Serve)
                {
                    if (order.DrinkType != DrinksModel.DrinkType.None)
                    {
                        Stats.CustomerStatsCache.DrinksServed++;
                    }
                }
                if (order.DrinkType != DrinksModel.DrinkType.None)
                {
                    Stats.CustomerStatsCache.DrinksOrdered++;
                }
                if (order.OrderType == OrdersModel.OrderType.ServeFood)
                {                     
                    Stats.CustomerStatsCache.FoodServed++;
                }
                if (order.OrderType == OrdersModel.OrderType.MakeFood)
                {
                    Stats.CustomerStatsCache.FoodPrepared++;
                }
                if (order.OrderType == OrdersModel.OrderType.GetFoodOrder)
                {
                    Stats.CustomerStatsCache.FoodOrdered++;
                }
                if (order.OrderType == OrdersModel.OrderType.FillDrink)
                {
                    Stats.CustomerStatsCache.DrinksFilled++;
                }
            }
        }
    }
}
