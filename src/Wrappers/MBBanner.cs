using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBBanner : CachedWrapperBase<MBBanner, Banner>
  {
    public static implicit operator Banner(MBBanner wrapper) => wrapper.Unwrap();
    public static implicit operator MBBanner(Banner obj) => MBBanner.GetWrapperFor(obj);
  }
}
