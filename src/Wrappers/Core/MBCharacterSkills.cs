using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBCharacterSkills : MBWrapperBase<MBCharacterSkills, CharacterSkills>
  {
    public static implicit operator CharacterSkills(MBCharacterSkills wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterSkills(CharacterSkills obj) => MBCharacterSkills.GetWrapperFor(obj);
  }

  public class MBCharacterSkillsList : MBListBase<MBCharacterSkills, MBCharacterSkillsList>
  {
    public static implicit operator List<CharacterSkills>(MBCharacterSkillsList wrapperList) => wrapperList.Unwrap<MBCharacterSkills, CharacterSkills>();
    public static implicit operator MBCharacterSkillsList(List<CharacterSkills> objectList) => (MBCharacterSkillsList)objectList.Wrap<MBCharacterSkills, CharacterSkills>();
  }
}
