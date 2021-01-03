using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBSkillObject : CachedWrapperBase<MBSkillObject, SkillObject>
  {
    public static implicit operator SkillObject(MBSkillObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSkillObject(SkillObject obj) => MBSkillObject.GetWrapperFor(obj);
  }

  public class MBSkillObjectList : List<MBSkillObject>
  {
    public static implicit operator List<SkillObject>(MBSkillObjectList wrapperList) => wrapperList.Unwrap<MBSkillObject, SkillObject>();
    public static implicit operator MBSkillObjectList(List<SkillObject> objectList) => (MBSkillObjectList)objectList.Wrap<MBSkillObject, SkillObject>();
  }
}
