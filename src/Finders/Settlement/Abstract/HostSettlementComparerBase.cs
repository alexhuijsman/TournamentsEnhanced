using TournamentsEnhanced.Models.Serializable;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {

    public Payor Payor { get; private set; }

    public HostSettlementComparerBase(Payor payor) => Payor = payor;
  }
}