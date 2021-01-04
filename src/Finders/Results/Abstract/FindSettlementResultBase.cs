using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Results.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public interface ISettlementResultBase : IResultBase
  {
    MBSettlement Settlement { get; }
  }

  public abstract class FindSettlementResultBase : ResultBase, ISettlementResultBase
  {
    public MBSettlement Settlement { get; private set; }

    protected FindSettlementResultBase(ResultStatus status) : base(status) { }
    protected FindSettlementResultBase(ResultStatus status, Settlement settlement) : base(status)
    {
      Settlement = settlement;
    }
  }
}
