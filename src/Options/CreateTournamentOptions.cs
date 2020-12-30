using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public struct CreateTournamentOptions
  {
    public readonly Town town;
    public readonly TournamentType type;
    public readonly Payor payor;

    public CreateTournamentOptions(FindHostTownResult result, TournamentType type, Payor payor)
    {
      this.town = result.Town;
      this.type = type;
      this.payor = payor;
    }
  }
}
