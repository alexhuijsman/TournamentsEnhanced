using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBCharacterSkills : MBWrapperBase<MBCharacterSkills, CharacterSkills>
  {
    public static implicit operator CharacterSkills(MBCharacterSkills wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCharacterSkills(CharacterSkills obj) => MBCharacterSkills.GetWrapper(obj);
  }
}
