using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCharacterObject : CachedWrapperBase<MBCharacterObject, CharacterObject>
  {
    public static implicit operator CharacterObject(MBCharacterObject wrapper) => wrapper.Unwrap();
    public static implicit operator MBCharacterObject(CharacterObject obj) => MBCharacterObject.GetWrapperFor(obj);
  }
}
