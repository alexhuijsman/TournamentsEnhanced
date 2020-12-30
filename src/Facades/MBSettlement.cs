using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class MBSettlement
  {
    public static IReadOnlyList<Settlement> AllSettlementsReadOnly => Settlement.All;
    public static List<Settlement> AllSettlements => new List<Settlement>(AllSettlementsReadOnly);
    public static List<Settlement> AllSettlementsShuffled => AllSettlements.Shuffle();
  }
}
