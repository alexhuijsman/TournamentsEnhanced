using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBodyProperties : MBWrapperBase<MBBodyProperties, BodyProperties>
  {
    public static implicit operator BodyProperties(MBBodyProperties wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBodyProperties(BodyProperties obj) => MBBodyProperties.GetWrapper(obj);
  }
}
