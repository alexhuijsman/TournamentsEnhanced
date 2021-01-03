using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced.Finder
{
  public static class HostTownFinder
  {
    public static FindHostTownResult FindInSettlements(FindHostTownOptions options)
    {
      var settlements = options.Settlements.ToList().Shuffle();
      var sortedSettlements = settlements.Sort(new TownComparer());

      )
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
