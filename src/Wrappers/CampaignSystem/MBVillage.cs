using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBVillage : MBWrapperBase<MBVillage, Village>
  {
    public static implicit operator Village(MBVillage wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBVillage(Village obj) => GetWrapper(obj);
  }
}
