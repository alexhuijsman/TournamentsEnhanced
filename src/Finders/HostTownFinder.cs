using System.Collections;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class HostTownFinder
  {

    public static FindHostTownResult FindForFaction(IFaction faction, FindHostTownOptions options)
    {
      var settlements = faction.Settlements.ToList().Shuffle();

      return FindInSettlements(settlements, options);
    }

    public static FindHostTownResult FindInSettlements(IList<Settlement> settlements, FindHostTownOptions options)
    {
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
