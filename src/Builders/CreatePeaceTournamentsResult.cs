using TournamentsEnhanced.Builders.Abstract;

namespace TournamentsEnhanced.Builders
{
  public class CreatePeaceTournamentsResult : CreateResultBase<CreatePeaceTournamentsResult>
  {
    public string HostSettlementNames { get; protected set; }
    public new static CreatePeaceTournamentsResult Success(CreateTournamentResult factionAResult,
                                                           CreateTournamentResult factionBResult)
    {
      string hostSettlementNames;
      if (factionAResult.Succeeded && factionBResult.Succeeded)
      {
        hostSettlementNames = $"{factionAResult.HostSettlement.Name} and {factionBResult.HostSettlement.Name}";
      }
      else
      {
        hostSettlementNames = factionAResult.Succeeded ? $"{factionAResult.HostSettlement.Name}" : $"{factionBResult.HostSettlement.Name}";
      }

      return new CreatePeaceTournamentsResult()
      {
        Status = ResultStatus.Success,
        HostSettlementNames = hostSettlementNames
      };
    }
  }
}
