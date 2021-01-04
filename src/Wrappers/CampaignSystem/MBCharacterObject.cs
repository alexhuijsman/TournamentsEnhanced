using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCharacterObject : MBObjectBaseWrapper<MBCharacterObject, CharacterObject>
  {
    public MBHero HeroObject => UnwrappedObject.HeroObject;
    internal static MBCharacterObject Find(string stringId) => CharacterObject.Find(stringId);

    public static implicit operator CharacterObject(MBCharacterObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterObject(CharacterObject obj) => MBCharacterObject.GetWrapperFor(obj);
  }

  public class MBCharacterObjectList : MBListBase<MBCharacterObject, MBCharacterObjectList>
  {
    public static implicit operator List<CharacterObject>(MBCharacterObjectList wrapperList) => wrapperList.Unwrap<MBCharacterObject, CharacterObject>();
    public static implicit operator MBCharacterObjectList(List<CharacterObject> objectList) => (MBCharacterObjectList)objectList.Wrap<MBCharacterObject, CharacterObject>();
  }
}
