using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBHeroDeveloper : MBWrapperBase<MBHeroDeveloper, HeroDeveloper>
  {
    public static implicit operator HeroDeveloper(MBHeroDeveloper wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBHeroDeveloper(HeroDeveloper obj) => GetWrapper(obj);
  }
}
