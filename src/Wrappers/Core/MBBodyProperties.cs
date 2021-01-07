using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBodyProperties : MBWrapperBase<MBBodyProperties, BodyProperties>
  {
    public static implicit operator BodyProperties(MBBodyProperties wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBodyProperties(BodyProperties obj) => MBBodyProperties.GetWrapperFor(obj);
  }

  public class MBBodyPropertiesList : MBListBase<MBBodyProperties, MBBodyPropertiesList>
  {
    public MBBodyPropertiesList(params MBBodyProperties[] wrappers) : this((IEnumerable<MBBodyProperties>)wrappers) { }
    public MBBodyPropertiesList(IEnumerable<MBBodyProperties> wrappers) => AddRange(wrappers);
    public MBBodyPropertiesList(MBBodyProperties wrapper) => Add(wrapper);
    public MBBodyPropertiesList() { }

    public static implicit operator List<BodyProperties>(MBBodyPropertiesList wrapperList) => wrapperList.Unwrap<MBBodyProperties, BodyProperties>();
    public static implicit operator MBBodyPropertiesList(List<BodyProperties> objectList) => (MBBodyPropertiesList)objectList.Wrap<MBBodyProperties, BodyProperties>();
    public static implicit operator MBBodyProperties[](MBBodyPropertiesList wrapperList) => wrapperList.ToArray();
  }
}
