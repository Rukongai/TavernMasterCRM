using System.Collections.Generic;

namespace RukonMods.TavCRM
{
    internal class Stats
    {
        public static class CustomerStatsCache
        {
            public static List<CustomerController> Total = new List<CustomerController>();
            public static int customerCounter = 0;
            public static int Common = 0;
            public static int Rare = 0;
            public static int Gold = 0;
            public static int Royal = 0;
            public static int HotelCustomer = 0;
            public static int FoodOrdered = 0;
            public static int FoodPrepared = 0;
            public static int FoodServed = 0;
            public static int DrinksOrdered = 0;
            public static int DrinksFilled = 0;
            public static int DrinksServed = 0;
            public static int Seated = 0;



            public static void Increment(CustomerController.CustomerType custType)
            {
                if (custType.IsCommon())
                {
                    Common++;
                }
                else if (custType.IsRare())
                {
                    Rare++;
                }
                else if (custType.IsGold())
                {
                    Gold++;
                }
                else if (custType.IsRoyal())
                {
                    Royal++;
                }

            }

            public static void Reset()
            {
                customerCounter = FoodOrdered = DrinksOrdered = FoodServed = DrinksFilled = DrinksServed = Common = Rare = Gold = Royal = HotelCustomer = FoodPrepared = 0;
            }
        }
    }
}
