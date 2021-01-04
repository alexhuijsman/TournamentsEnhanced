using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBStatExplainer : MBWrapperBase<MBStatExplainer, StatExplainer>
  {
    public static implicit operator StatExplainer(MBStatExplainer wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBStatExplainer(StatExplainer obj) => MBStatExplainer.GetWrapperFor(obj);
  }

  public class MBStatExplainerList : MBListBase<MBStatExplainer, MBStatExplainerList>
  {
    public static implicit operator List<StatExplainer>(MBStatExplainerList wrapperList) => wrapperList.Unwrap<MBStatExplainer, StatExplainer>();
    public static implicit operator MBStatExplainerList(List<StatExplainer> objectList) => (MBStatExplainerList)objectList.Wrap<MBStatExplainer, StatExplainer>();
  }
}
