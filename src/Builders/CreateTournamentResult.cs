using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class CreateTournamentResult : CreateResultBase<CreateTournamentResult>
  {
    public MBHero Payor { get; protected set; }
    public bool HasPayor => Payor == null;
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
        Payor = isInitialTournament ? MBHero.Null : settlement.ClanLeader
      };
    }
  }
}
