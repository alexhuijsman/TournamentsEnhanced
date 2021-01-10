using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBFaction : MBWrapperBase<MBFaction, Faction>
  {
    public static implicit operator Faction(MBFaction wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBFaction(Faction obj) => MBFaction.GetWrapperFor(obj);
  }

  public class MBFactionList : MBListBase<MBFaction, MBFactionList>
  {
    public MBFactionList(params MBFaction[] wrappers) : this((IEnumerable<MBFaction>)wrappers) { }
    public MBFactionList(IEnumerable<MBFaction> wrappers) => AddRange(wrappers);
    public MBFactionList(MBFaction wrapper) => Add(wrapper);
    public MBFactionList() { }

    public static implicit operator List<Faction>(MBFactionList wrapperList) => wrapperList.Unwrap<MBFaction, Faction>();
    public static implicit operator MBFactionList(List<Faction> objectList) => (MBFactionList)objectList.Wrap<MBFaction, Faction>();
    public static implicit operator MBFaction[](MBFactionList wrapperList) => wrapperList.ToArray();
  }
}
