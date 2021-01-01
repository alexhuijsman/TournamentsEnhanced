using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCharacterObject : CachedWrapperBase<MBCharacterObject, CharacterObject>
  {
    public static implicit operator CharacterObject(MBCharacterObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterObject(CharacterObject obj) => MBCharacterObject.GetWrapperFor(obj);
  }

  public class MBCharacterObjectList : List<MBCharacterObject>
  {
    public static implicit operator List<CharacterObject>(MBCharacterObjectList wrapperList) => wrapperList.Unwrap<MBCharacterObject, CharacterObject>();
    public static implicit operator MBCharacterObjectList(List<CharacterObject> objectList) => (MBCharacterObjectList)objectList.Wrap<MBCharacterObject, CharacterObject>();
  }
}
