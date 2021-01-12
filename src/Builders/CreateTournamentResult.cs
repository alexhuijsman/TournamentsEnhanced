using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public class CreateTournamentResult : CreateResultBase<CreateTournamentResult>
  {
    public static ModState ModState { protected get; set; } = ModState.Instance;

    public MBHero Payor { get; protected set; }
    public bool HasPayor => Payor == MBHero.Null;
    public MBSettlement HostSettlement { get; protected set; }
    public bool HadExistingTournament { get; protected set; }
    public new static CreateTournamentResult Success(MBSettlement settlement,
                                                     bool hadExistingTournament)
    {
      var isInitialTournament = ModState.TournamentRecords[settlement].tournamentType == TournamentType.Initial;

      return new CreateTournamentResult()
      {
        HadExistingTournament = hadExistingTournament,
        Status = ResultStatus.Success,
        Payor = isInitialTournament ? MBHero.Null : settlement.OwnerClan.Leader
      };
    }
  }
}
