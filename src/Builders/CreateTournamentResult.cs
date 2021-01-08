using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class CreateTournamentResult : CreateResultBase<CreateTournamentResult>
  {
    public Payor Payor { get; protected set; }
    public MBSettlement HostSettlement { get; protected set; }
    public bool HadExistingTournament { get; protected set; }
    public new static CreateTournamentResult Success(MBSettlement settlement,
                                                     Payor payor,
                                                     bool hadExistingTournament)
    {
      return new CreateTournamentResult()
      {
        Payor = payor,
        HadExistingTournament = hadExistingTournament,
        Status = ResultStatus.Success
      };
    }
  }
}
