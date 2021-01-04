using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public static class SettlementFinder
  {
    public static FindHostTownResult FindHostSettlement(FindHostSettlementOptions options)
    {
      var settlements = options.Settlements.Shuffle();
      var sortedSettlements = settlements.Sort(new TownComparer());

      Town townWithExistingTournament = null;
      Town hostTown = null;

      foreach (var settlement in settlements)
      {
        if (settlement.IsTown && settlement.Town.HasTournament && townWithExistingTournament == null)
        {
          townWithExistingTournament = settlement.Town;
        }

        if (!settlement.IsTown || settlement.Town.HasTournament)
        {
          continue;
        }

        hostTown = settlement.Town;
        break;
      }

      if (hostTown == null && townWithExistingTournament != null && options.CanUseExistingTournamentAsLastResort)
      {
        hostTown = townWithExistingTournament;
      }

      return hostTown == null ? FindHostTownResult.Failure() : FindHostTownResult.Success(hostTown);
    }
  }
}
