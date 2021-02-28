using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public interface IMBDynamicBodyProperties : IStructWrapperBase<DynamicBodyProperties>
  {
    float Age { get; }
    float Weight { get; }
    float Build { get; }
  }
}