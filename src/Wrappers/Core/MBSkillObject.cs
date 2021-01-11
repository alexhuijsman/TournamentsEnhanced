using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBSkillObject : MBObjectBaseWrapper<MBSkillObject, SkillObject>
  {
    public static implicit operator SkillObject(MBSkillObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSkillObject(SkillObject obj) => MBSkillObject.GetWrapper(obj);
  }
}
