using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCharacterObject : CachedWrapperBase<MBCharacterObject, CharacterObject>
  {
    public static implicit operator CharacterObject(MBCharacterObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterObject(CharacterObject obj) => MBCharacterObject.GetWrapperFor(obj);
  }
}
