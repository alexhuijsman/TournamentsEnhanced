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

  public class MBSkillObjectList : MBListBase<MBSkillObject, MBSkillObjectList>
  {
    public MBSkillObjectList(params MBSkillObject[] wrappers) : this((IEnumerable<MBSkillObject>)wrappers) { }
    public MBSkillObjectList(IEnumerable<MBSkillObject> wrappers) => AddRange(wrappers);
    public MBSkillObjectList(MBSkillObject wrapper) => Add(wrapper);
    public MBSkillObjectList() { }

    public static implicit operator List<SkillObject>(MBSkillObjectList wrapperList) => wrapperList.Unwrap<MBSkillObject, SkillObject>();
    public static implicit operator MBSkillObjectList(List<SkillObject> objectList) => (MBSkillObjectList)objectList.Wrap<MBSkillObject, SkillObject>();
    public static implicit operator MBSkillObject[](MBSkillObjectList wrapperList) => wrapperList.ToArray();
  }
}
