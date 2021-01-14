using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public interface IMBBodyProperties : IStructWrapperBase<BodyProperties>
  {
    float Age { get; }
    ulong KeyPart7 { get; }
    ulong KeyPart6 { get; }
    ulong KeyPart5 { get; }
    ulong KeyPart4 { get; }
    ulong KeyPart3 { get; }
    ulong KeyPart2 { get; }
    ulong KeyPart1 { get; }
    float Build { get; }
    float Weight { get; }
    MBDynamicBodyProperties DynamicProperties { get; }
    ulong KeyPart8 { get; }
    MBStaticBodyProperties StaticProperties { get; }
    MBBodyProperties ClampForMultiplayer();
  }
}