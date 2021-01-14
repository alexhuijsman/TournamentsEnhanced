using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBStatExplainer : MBWrapperBase<MBStatExplainer, StatExplainer>
  {
    public static implicit operator StatExplainer(MBStatExplainer wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBStatExplainer(StatExplainer obj) => GetWrapper(obj);
  }
}
