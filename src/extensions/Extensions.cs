using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace TournamentsEnhanced
{
    static class Extensions
    {

        public static bool IsLedBy(this Settlement settlement, TextObject leaderName)
        {
            return settlement.OwnerClan.Leader.Name.Equals(leaderName);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
