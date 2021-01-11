using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBPartyBase : MBWrapperBase<MBPartyBase, PartyBase>
  {
    public static implicit operator PartyBase(MBPartyBase wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBPartyBase(PartyBase obj) => MBPartyBase.GetWrapper(obj);
  }
}
