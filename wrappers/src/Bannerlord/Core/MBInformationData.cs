using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBInformationData : MBWrapperBase<MBInformationData, InformationData>
  {
    public static implicit operator InformationData(MBInformationData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBInformationData(InformationData obj) => GetWrapper(obj);
  }
}
