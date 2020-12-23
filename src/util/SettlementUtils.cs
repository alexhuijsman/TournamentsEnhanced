using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public class SettlementUtils
    {
        public static IList<Settlement> AllSettlements => new List<Settlement>(AllSettlementsReadOnly);
        public static IList<Settlement> AllSettlementsShuffled => AllSettlements.Shuffle();
        public static IReadOnlyList<Settlement> AllSettlementsReadOnly => Settlement.All;
    }
}
