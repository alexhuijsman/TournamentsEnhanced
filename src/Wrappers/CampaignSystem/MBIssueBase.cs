using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBIssueBase : MBWrapperBase<MBIssueBase, IssueBase>
  {
    public static implicit operator IssueBase(MBIssueBase wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBIssueBase(IssueBase obj) => MBIssueBase.GetWrapperFor(obj);
  }

  public class MBIssueBaseList : MBListBase<MBIssueBase, MBIssueBaseList>
  {
    public MBIssueBaseList(params MBIssueBase[] wrappers) : this((IEnumerable<MBIssueBase>)wrappers) { }
    public MBIssueBaseList(IEnumerable<MBIssueBase> wrappers) => AddRange(wrappers);
    public MBIssueBaseList(MBIssueBase wrapper) => Add(wrapper);
    public MBIssueBaseList() { }

    public static implicit operator List<IssueBase>(MBIssueBaseList wrapperList) => wrapperList.Unwrap<MBIssueBase, IssueBase>();
    public static implicit operator MBIssueBaseList(List<IssueBase> objectList) => (MBIssueBaseList)objectList.Wrap<MBIssueBase, IssueBase>();
    public static implicit operator MBIssueBase[](MBIssueBaseList wrapperList) => wrapperList.ToArray();
  }
}
