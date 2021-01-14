using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBanner : MBWrapperBase<MBBanner, Banner>
  {
    public static implicit operator Banner(MBBanner wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBanner(Banner obj) => GetWrapper(obj);
  }
}
