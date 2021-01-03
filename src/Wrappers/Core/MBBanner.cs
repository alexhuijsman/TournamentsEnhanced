using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBanner : CachedWrapperBase<MBBanner, Banner>
  {
    public static implicit operator Banner(MBBanner wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBanner(Banner obj) => MBBanner.GetWrapperFor(obj);
  }
  public class MBBannerList : List<MBBanner>
  {
    public static implicit operator List<Banner>(MBBannerList wrapperList) => wrapperList.Unwrap<MBBanner, Banner>();
    public static implicit operator MBBannerList(List<Banner> objectList) => (MBBannerList)objectList.Wrap<MBBanner, Banner>();
  }
}
