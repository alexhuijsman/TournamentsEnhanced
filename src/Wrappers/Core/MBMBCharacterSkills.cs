using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBMBCharacterSkills : MBWrapperBase<MBMBCharacterSkills, TaleWorlds.Core.MBCharacterSkills>
  {
    public static implicit operator TaleWorlds.Core.MBCharacterSkills(MBMBCharacterSkills wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMBCharacterSkills(TaleWorlds.Core.MBCharacterSkills obj) => GetWrapper(obj);
  }
}
