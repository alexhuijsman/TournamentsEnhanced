using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCharacterObject : MBObjectBaseWrapper<MBCharacterObject, CharacterObject>
  {
    public static MBCharacterObject Instance { get; } = new MBCharacterObject();
    public virtual MBHero HeroObject => UnwrappedObject.HeroObject;
    public MBCharacterObject Find(string stringId) => CharacterObject.Find(stringId);

    public static implicit operator CharacterObject(MBCharacterObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterObject(CharacterObject obj) => GetWrapper(obj);
  }
}
