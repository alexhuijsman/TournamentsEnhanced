using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  public static class HostTownFinder
  {


    public static Town FindByName(string name)
    {
      return MBTown.AllTowns.Find((town) => town.Name.Equals(name));
    }

    public static FindHostTownResult Find(FindHostTownOptions options)
    {
      return FindInSettlements(options);
    }

    public static FindHostTownResult FindInSettlements(FindHostTownOptions options)
    {
      var settlements = options.Settlements.ToList().Shuffle();
      var sortedSettlements = settlements.Sort(new HostTownComparer());

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
