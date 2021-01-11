using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCharacterTraits : MBWrapperBase<MBCharacterTraits, CharacterTraits>
  {
    public static implicit operator CharacterTraits(MBCharacterTraits wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterTraits(CharacterTraits obj) => MBCharacterTraits.GetWrapper(obj);
  }
}
