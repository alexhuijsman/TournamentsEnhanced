using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBStatExplainer : CachedWrapperBase<MBStatExplainer, StatExplainer>
  {
    public static implicit operator StatExplainer(MBStatExplainer wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBStatExplainer(StatExplainer obj) => MBStatExplainer.GetWrapperFor(obj);
  }

  public class MBStatExplainerList : List<MBStatExplainer>
  {
    public static implicit operator List<StatExplainer>(MBStatExplainerList wrapperList) => wrapperList.Unwrap<MBStatExplainer, StatExplainer>();
    public static implicit operator MBStatExplainerList(List<StatExplainer> objectList) => (MBStatExplainerList)objectList.Wrap<MBStatExplainer, StatExplainer>();
  }
}
