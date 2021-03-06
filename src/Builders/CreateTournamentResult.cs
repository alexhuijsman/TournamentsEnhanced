using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public class CreateTournamentResult : CreateResultBase<CreateTournamentResult>
  {
    public virtual MBHero Payor { get; protected set; }
    public virtual bool HasPayor => Payor != MBHero.Null;
    public virtual MBSettlement HostSettlement { get; protected set; }
    public virtual bool HadExistingTournament { get; protected set; }
    public new static CreateTournamentResult Success(TournamentType tournamentType, bool hadExistingTournament, MBSettlement settlement)
    {
      var isInitialTournament = tournamentType == TournamentType.Initial;

      return new CreateTournamentResult()
      {
        HadExistingTournament = hadExistingTournament,
        Status = ResultStatus.Success,
        Payor = isInitialTournament ? MBHero.Null : settlement.OwnerClan.Leader,
        HostSettlement = settlement
      };
    }
  }
}
