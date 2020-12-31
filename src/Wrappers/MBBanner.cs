using TaleWorlds.Core;

namespace TournamentsEnhanced.Wrappers
{
  public class MBBanner : CachedWrapper<MBBanner, Banner>
  {
    public static implicit operator Banner(MBBanner wrapper) => wrapper.Unwrap();
    public static implicit operator MBBanner(Banner obj) => MBBanner.GetWrapperFor(obj);
  }
}
