using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBIssueBase : MBWrapperBase<MBIssueBase, IssueBase>
  {
    public static implicit operator IssueBase(MBIssueBase wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBIssueBase(IssueBase obj) => GetWrapper(obj);
  }
}
