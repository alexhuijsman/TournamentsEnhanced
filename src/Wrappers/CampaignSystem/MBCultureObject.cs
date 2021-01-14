using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCultureObject : MBObjectBaseWrapper<MBCultureObject, CultureObject>
  {
    public static implicit operator CultureObject(MBCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCultureObject(CultureObject obj) => GetWrapper(obj);
  }
}
