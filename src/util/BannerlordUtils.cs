using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TournamentsEnhanced
{
  public class BannerlordUtils
  {
    public static void WeddingSettlementStatChange(Settlement settlement)
    {
      settlement.Prosperity += TournamentsEnhancedSettings.Instance.ProsperityIncrease;
      settlement.Town.Loyalty += TournamentsEnhancedSettings.Instance.LoyaltyIncrease;
      settlement.Town.Security += TournamentsEnhancedSettings.Instance.SecurityIncrease;
      settlement.Town.FoodStocks -= TournamentsEnhancedSettings.Instance.FoodStocksDecrease;

      if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter && TournamentsEnhancedSettings.Instance.SettlementStatNotification)
      {
        NotificationUtils.DisplayMessage(settlement.Town.Name + "'s prosperity, loyalty and security have increased and food stocks have decreased");
      }
    }

    public static void HostedSettlementStatChange(Settlement settlement)
    {
      WeddingSettlementStatChange(settlement);
    }

    public static void LocalRelationStatChange(Settlement settlement)
    {
      MBReadOnlyList<Hero> notableList = settlement.Notables;
      List<Hero>.Enumerator enumerator = notableList.GetEnumerator();

      while (enumerator.MoveNext())
      {
        if (!enumerator.Current.Name.Equals(settlement.OwnerClan.Leader.Name))
        {
          enumerator.Current.SetPersonalRelation(settlement.OwnerClan.Leader, enumerator.Current.GetRelation(settlement.OwnerClan.Leader) + RELATIONSHIP_MODIFIER);
        }
      }

      if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter)
      {
        NotificationUtils.DisplayBannerMessage("Your relationship with local notables at " + settlement.Town.Name + " has improved");
      }
    }

    private const int RELATIONSHIP_MODIFIER = 3;
    public static CampaignOptions.Difficulty difficulty;
  }
}
