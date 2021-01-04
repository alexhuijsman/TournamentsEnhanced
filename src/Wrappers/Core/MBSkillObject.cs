using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBSkillObject : MBObjectBaseWrapper<MBSkillObject, SkillObject>
  {
    public static implicit operator SkillObject(MBSkillObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSkillObject(SkillObject obj) => MBSkillObject.GetWrapperFor(obj);
  }

  public class MBSkillObjectList : MBListBase<MBSkillObject, MBSkillObjectList>
  {
    public static implicit operator List<SkillObject>(MBSkillObjectList wrapperList) => wrapperList.Unwrap<MBSkillObject, SkillObject>();
    public static implicit operator MBSkillObjectList(List<SkillObject> objectList) => (MBSkillObjectList)objectList.Wrap<MBSkillObject, SkillObject>();
  }
}
