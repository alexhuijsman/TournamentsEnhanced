using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCharacterTraits : MBWrapperBase<MBCharacterTraits, CharacterTraits>
  {
    public static implicit operator CharacterTraits(MBCharacterTraits wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterTraits(CharacterTraits obj) => MBCharacterTraits.GetWrapperFor(obj);
  }

  public class MBCharacterTraitsList : MBListBase<MBCharacterTraits, MBCharacterTraitsList>
  {
    public MBCharacterTraitsList(params MBCharacterTraits[] wrappers) : this((IEnumerable<MBCharacterTraits>)wrappers) { }
    public MBCharacterTraitsList(IEnumerable<MBCharacterTraits> wrappers) => AddRange(wrappers);
    public MBCharacterTraitsList(MBCharacterTraits wrapper) => Add(wrapper);
    public MBCharacterTraitsList() { }

    public static implicit operator List<CharacterTraits>(MBCharacterTraitsList wrapperList) => wrapperList.Unwrap<MBCharacterTraits, CharacterTraits>();
    public static implicit operator MBCharacterTraitsList(List<CharacterTraits> objectList) => (MBCharacterTraitsList)objectList.Wrap<MBCharacterTraits, CharacterTraits>();
  }
}
