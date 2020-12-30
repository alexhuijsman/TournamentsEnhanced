using System.Collections;

namespace TournamentsEnhanced
{
  public struct FindHostTownOptions
  {
    private static readonly bool UseExisting = true;
    private static readonly bool RejectExisting = false;

    public readonly bool CanUseExistingTournamentAsLastResort;
    public readonly IComparer TownComparer;

    private FindHostTownOptions(bool canUseExistingTournamentAsLastResort, IComparer townComparer = null)
    {
      CanUseExistingTournamentAsLastResort = canUseExistingTournamentAsLastResort;
      TownComparer = townComparer;
    }
  }
}
